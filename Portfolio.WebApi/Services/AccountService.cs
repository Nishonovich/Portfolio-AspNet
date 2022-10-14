using Portfolio.WebApi.Interfaces.Services;
using Portfolio.WebApi.ViewModels.Users;

namespace Portfolio.WebApi.Services
{
    public class AccountService : IAccountService
    {
        public Task<string> EmailVerifyAsync(EmailAddress emailAddress)
        {
            throw new NotImplementedException();
        }

        public Task<string> LoginAsync(UserLoginModel userLoginModel)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RegistrAsync(UserCreateViewModel userCreateViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
