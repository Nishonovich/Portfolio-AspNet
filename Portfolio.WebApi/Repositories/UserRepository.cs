using Microsoft.EntityFrameworkCore;
using Portfolio.WebApi.Data;
using Portfolio.WebApi.Interfaces.IRepositories;
using Portfolio.WebApi.Models;
using System.Linq.Expressions;

namespace Portfolio.WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbSet;

        public UserRepository(AppDbContext dbContext)
        {
            _dbSet = dbContext;

        }
        public async Task<User> CreateAsync(User user)
        {
            var entry = await _dbSet.Users.AddAsync(user);
            return entry.Entity;
        }

        public Task DeleteAsync(User user)
            => Task.FromResult(_dbSet.Users.Remove(user));

        public IQueryable<User> GetAll(Expression<Func<User, bool>>? expression = null, bool isTracking = true)
        {
            var result = expression is null ? _dbSet.Users : _dbSet.Users.Where(expression);

            return isTracking ? result : result.AsNoTracking();
        }

        public async Task<User?> GetAsync(Expression<Func<User, bool>> expression)
            => await _dbSet.Users.FirstOrDefaultAsync(expression);

        public Task<User> UpdateAsync(User user)
            => Task.FromResult(_dbSet.Users.Update(user).Entity);
        
    }
}
