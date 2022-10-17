using Microsoft.EntityFrameworkCore;
using Portfolio.WebApi.Data;
using Portfolio.WebApi.Interfaces.IRepositories;
using Portfolio.WebApi.Models;
using System.Linq.Expressions;

namespace Portfolio.WebApi.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        private readonly AppDbContext _dbSet;

        public ProjectRepository(AppDbContext dbContext)
        {
            _dbSet = dbContext;
        }
        public async Task<Project> CreateAsync(Project project)
        {
            var entry = await _dbSet.Projects.AddAsync(project);
            return entry.Entity;
        }

        public Task DeleteAsync(Project project)
            => Task.FromResult(_dbSet.Remove(project));

        public IQueryable<Project> GetAll(Expression<Func<Project, bool>>? expression = null, bool isTracking = true)
        {
            var sources = expression is null ? _dbSet.Projects : _dbSet.Projects.Where(expression);

            return isTracking ? sources : sources.AsNoTracking();
        }

        public Task<Project?> GetAsync(Expression<Func<Project, bool>> expression)
            => _dbSet.Projects.FirstOrDefaultAsync(expression);

        public async Task UpdateAsync(Project project)
            => _dbSet.Update(project);

    }
}
