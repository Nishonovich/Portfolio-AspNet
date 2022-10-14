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

        public async Task<Project?> GetAsync(Expression<Func<Project, bool>> expression)
            => await _dbSet.Projects.FirstOrDefaultAsync(expression);

        public Task<Project> UpdateAsync(Project project)
            => Task.FromResult(_dbSet.Update(project).Entity);

    }
}
