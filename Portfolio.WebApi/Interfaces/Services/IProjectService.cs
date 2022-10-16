using Portfolio.WebApi.Commons.Utils;
using Portfolio.WebApi.Models;
using Portfolio.WebApi.ViewModels.Projects;
using Portfolio.WebApi.ViewModels.Users;
using System.Linq.Expressions;

namespace Portfolio.WebApi.Interfaces.Services
{
    public interface IProjectService
    {
        Task<ProjectViewModel> CreateAsync(long userId, ProjectCreateViewModel projectCreate);
        Task<ProjectViewModel> UpdateAsync(long id, ProjectCreateViewModel createViewModel);
        Task<bool> DeleteAsync(Expression<Func<Project, bool>> expression);
        Task<IEnumerable<ProjectViewModel>> GetAllAsync(Expression<Func<Project, bool>>? expression = null,
            PaginationParams? @params = null);
        Task<ProjectViewModel?> GetAsync(Expression<Func<Project, bool>> expression);
    }
}
