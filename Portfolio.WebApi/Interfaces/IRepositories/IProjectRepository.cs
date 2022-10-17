using Portfolio.WebApi.Models;
using System.Linq.Expressions;

namespace Portfolio.WebApi.Interfaces.IRepositories
{
    public interface IProjectRepository
    {
        Task<Project> CreateAsync(Project project);
        Task UpdateAsync(Project project);
        Task DeleteAsync(Project project);
        Task<Project?> GetAsync(Expression<Func<Project, bool>> expression);
        IQueryable<Project> GetAll(Expression<Func<Project, bool>>? expression = null, bool isTracking = true);
    }
}
