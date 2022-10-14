using Portfolio.WebApi.Models;
using System.Linq.Expressions;

namespace Portfolio.WebApi.Interfaces.IRepositories
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<User?> GetAsync(Expression<Func<User, bool>> expression);
        IQueryable<User> GetAll(Expression<Func<User, bool>>? expression=null, bool isTracking = true);
    }
}
