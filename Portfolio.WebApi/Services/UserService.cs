using AutoMapper;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Portfolio.WebApi.Commons.Extensions;
using Portfolio.WebApi.Commons.Utils;
using Portfolio.WebApi.Data;
using Portfolio.WebApi.Exceptions;
using Portfolio.WebApi.Interfaces.IRepositories;
using Portfolio.WebApi.Interfaces.Services;
using Portfolio.WebApi.Models;
using Portfolio.WebApi.Repositories;
using Portfolio.WebApi.Security;
using Portfolio.WebApi.ViewModels.Users;
using System.Linq.Expressions;
using System.Net;
using static System.Net.Mime.MediaTypeNames;

namespace Portfolio.WebApi.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public UserService(AppDbContext dbContext, IMapper mapper, IFileService fileService, IUserRepository userRepository)
        {
            _dbContext = dbContext;
            _userRepository = userRepository;
            _mapper = mapper;
            _fileService = fileService;

        }
        public async Task<bool> DeleteAsync(Expression<Func<User, bool>> expression)
        {
            var user = await _userRepository.GetAsync(expression);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");

            await _fileService.DeleteImageAsync(user.ImagePath);
            await _userRepository.DeleteAsync(user);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync(Expression<Func<User, bool>>? expression = null, PaginationParams? @params = null)
        {
            var users = (expression is null ? _dbContext.Users : _dbContext.Users.Where(expression)).ToPagedAsEnumerable(@params);
            var userViews = new List<UserViewModel>();

            foreach (var user in users)
            {
                var item = _mapper.Map<UserViewModel>(user);
                item.ImageUrl = user.ImagePath;
                userViews.Add(item);
            }

            return userViews;
        }

        public async Task<UserViewModel?> GetAsync(Expression<Func<User, bool>> expression)
        {
            var user = await _userRepository.GetAsync(expression);

            if (user is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");

            var userViewModel = _mapper.Map<UserViewModel>(user);
            userViewModel.ImageUrl = user.ImagePath;
            return userViewModel;
        }

        public async Task<UserViewModel> UpdateAsync(long id, UserCreateViewModel userCreateViewModel)
        {
            var entity = await _userRepository.GetAsync(user => user.Id == id);

            if (entity is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "User not found!");

            if (entity.ImagePath != "Images/defaultImage.jpg")
                await _fileService.DeleteImageAsync(entity.ImagePath);

            User user = _mapper.Map<User>(userCreateViewModel);
            var hasherResult = PasswordHasher.Hash(userCreateViewModel.Password);

            user.Id = id;
            user.PasswordHash = hasherResult.Hash;
            user.Salt = hasherResult.Salt;

            if (userCreateViewModel.Image is null)
                user.ImagePath = $"{_fileService.ImageFolderName}/defaultImage.jpg";
            else
                user.ImagePath = await _fileService.SaveImageAsync(userCreateViewModel.Image);

            await _userRepository.UpdateAsync(user);
            await _dbContext.SaveChangesAsync();

            var userViewModel = _mapper.Map<UserViewModel>(user);
            userViewModel.ImageUrl = user.ImagePath;
            return userViewModel;
        }
    }
}
