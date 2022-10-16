using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.OpenApi.Expressions;
using Portfolio.WebApi.Data;
using Portfolio.WebApi.Exceptions;
using Portfolio.WebApi.Interfaces.IRepositories;
using Portfolio.WebApi.Interfaces.Services;
using Portfolio.WebApi.Models;
using Portfolio.WebApi.Security;
using Portfolio.WebApi.ViewModels.Users;
using System.Net;
using System.Net.NetworkInformation;
using System.Security.Cryptography;

namespace Portfolio.WebApi.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private readonly AppDbContext _dbContext;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;
        public AccountService(IAuthService authService, IMemoryCache memoryCache, IUserRepository userRepository, AppDbContext dbContext
                ,IFileService fileService, IEmailService emailService, IMapper mapper)
        {
            _authService = authService;
            _memoryCache = memoryCache;
            _userRepository = userRepository;
            _dbContext = dbContext;
            _fileService = fileService;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<string> EmailVerifyAsync(EmailAddress emailAddress)
        {
            var user = await _userRepository.GetAsync(user => user.Email == emailAddress.Email);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found");

            if (_memoryCache.TryGetValue(emailAddress.Email, out var exceptedCode))
            {
                if (exceptedCode.Equals(emailAddress.Code))
                {
                    user.EmailConfirmed = true;
                    await _userRepository.UpdateAsync(user);
                    await _dbContext.SaveChangesAsync();

                    return _authService.GenerateToken(user);
                }
                else throw new StatusCodeException(HttpStatusCode.BadRequest, "Code is not valid");
            }
            else
                throw new Exception("Code is expired");
        }

        public async Task<string> LoginAsync(UserLoginModel userLoginModel)
        {
            var user = await _userRepository.GetAsync(user => user.Email == userLoginModel.Email);

            if(user is null)
                throw new StatusCodeException(HttpStatusCode.BadRequest, "Email is not valid");

            if (!PasswordHasher.Verify(userLoginModel.Password, user.Salt, user.PasswordHash))
                throw new StatusCodeException(HttpStatusCode.BadRequest, "Email is not valid");

            return _authService.GenerateToken(user);

        }

        public async Task<bool> RegistrAsync(UserCreateViewModel userCreateViewModel)
        {
            var checkUser = await _userRepository.GetAsync(user => user.Email == userCreateViewModel.Email);

            if (checkUser is not null)
                throw new StatusCodeException(HttpStatusCode.Conflict, "This email already exists");

            User user = _mapper.Map<User>(userCreateViewModel);

            if (userCreateViewModel.Image is null)
                user.ImagePath = $"{_fileService.ImageFolderName}/defaultImage.jpg";
            else
                user.ImagePath = await _fileService.SaveImageAsync(userCreateViewModel.Image);

            var hasherResult = PasswordHasher.Hash(userCreateViewModel.Password);
            user.PasswordHash = hasherResult.Hash;
            user.Salt = hasherResult.Salt;

            await _userRepository.CreateAsync(user);
            await _dbContext.SaveChangesAsync();

            var code = RandomNumberGenerator.GetInt32(1000, 9999).ToString();
            _memoryCache.Set(userCreateViewModel.Email, code, TimeSpan.FromMinutes(10));

            await _emailService.SendAsync(userCreateViewModel.Email, code);
            return true;
        }
    }
}
