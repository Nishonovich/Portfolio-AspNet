using Microsoft.EntityFrameworkCore;
using Portfolio.WebApi.Data;
using Portfolio.WebApi.Interfaces.IRepositories;
using Portfolio.WebApi.Models;
using System.Linq.Expressions;

namespace Portfolio.WebApi.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly DbSet<User> _dbSet;
        protected readonly AppDbContext appDbContext;


        public UserRepository(AppDbContext dbContext)
        {
            appDbContext = dbContext;
            _dbSet = dbContext.Set<User>();

        }
        public async Task<User> CreateAsync(User user)
        {
            var entry = await _dbSet.AddAsync(user);
            return entry.Entity;
        }

        public Task DeleteAsync(User user)
            => Task.FromResult(_dbSet.Remove(user));

        public IQueryable<User> GetAll(Expression<Func<User, bool>>? expression = null, bool isTracking = true)
        {
            var result = expression is null ? _dbSet : _dbSet.Where(expression);

            return isTracking ? result : result.AsNoTracking();
        }

        public async Task<User?> GetAsync(Expression<Func<User, bool>> expression)
            => await _dbSet.FirstOrDefaultAsync(expression);

        public async Task<User> UpdateAsync(User user)
            => _dbSet.Update(user).Entity;
        
    }
}
