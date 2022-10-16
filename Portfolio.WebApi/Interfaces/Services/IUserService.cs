using Portfolio.WebApi.Commons.Utils;
using Portfolio.WebApi.Models;
using Portfolio.WebApi.ViewModels.Users;
using System.Linq.Expressions;

namespace Portfolio.WebApi.Interfaces.Services
{
    public interface IUserService
    {
        Task<UserViewModel> UpdateAsync(long id, UserCreateViewModel userCreateViewModel);
        Task<bool> DeleteAsync(Expression<Func<User, bool>> expression);
        Task<IEnumerable<UserViewModel>> GetAllAsync(Expression<Func<User, bool>>? expression = null,
            PaginationParams? @params = null);
        Task<UserViewModel?> GetAsync(Expression<Func<User, bool>> expression);
    }
}
