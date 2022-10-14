using Portfolio.WebApi.ViewModels.Users;

namespace Portfolio.WebApi.Interfaces.Services
{
    public interface IAccountService
    {
        Task<string> EmailVerifyAsync(EmailAddress emailAddress);
        Task<bool> RegistrAsync(UserCreateViewModel userCreateViewModel);
        Task<string> LoginAsync(UserLoginModel userLoginModel);
    }
}
