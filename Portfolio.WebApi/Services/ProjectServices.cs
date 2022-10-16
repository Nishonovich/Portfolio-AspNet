using AutoMapper;
using Portfolio.WebApi.Commons.Extensions;
using Portfolio.WebApi.Commons.Utils;
using Portfolio.WebApi.Data;
using Portfolio.WebApi.Exceptions;
using Portfolio.WebApi.Interfaces.IRepositories;
using Portfolio.WebApi.Interfaces.Services;
using Portfolio.WebApi.Models;
using Portfolio.WebApi.Repositories;
using Portfolio.WebApi.Security;
using Portfolio.WebApi.ViewModels.Projects;
using Portfolio.WebApi.ViewModels.Users;
using System.Linq;
using System.Linq.Expressions;
using System.Net;

namespace Portfolio.WebApi.Services
{
    public class ProjectServices : IProjectService
    {
        private readonly AppDbContext _dbContext;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;
        private readonly IFileService _fileService;

        public ProjectServices(AppDbContext appDbContext, IMapper mapper, IFileService fileService, IProjectRepository projectRepository)
        {
            _dbContext = appDbContext;
            _projectRepository = projectRepository;
            _mapper = mapper;
            _fileService = fileService;
        }
        public async Task<ProjectViewModel> CreateAsync(long userId, ProjectCreateViewModel projectCreate)
        {
            var project = _mapper.Map<Project>(projectCreate);

            project.UserId = userId;

            project.LogoPath = await _fileService.SaveLogoAsync(projectCreate.Image);

            var projectView = await _projectRepository.CreateAsync(project);

            await _dbContext.SaveChangesAsync();

            return _mapper.Map<ProjectViewModel>(projectView);
        }

        public async Task<bool> DeleteAsync(Expression<Func<Project, bool>> expression)
        {
            var projects = await _projectRepository.GetAsync(expression);

            if (projects is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Project not found!");

            await _fileService.DeleteImageAsync(projects.LogoPath);
            await _projectRepository.DeleteAsync(projects);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ProjectViewModel>> GetAllAsync(Expression<Func<Project, bool>>? expression = null, PaginationParams? @params = null)
        {
            var projects = (expression is null ? _dbContext.Projects : _dbContext.Projects.Where(expression)).ToPagedAsEnumerable(@params);
            var projectViews = new List<ProjectViewModel>(); 
            

            foreach (var project in projects)
            {
                var item = _mapper.Map<ProjectViewModel>(project);
                item.LogoPath = project.LogoPath;
                projectViews.Add(item);
            }

            return projectViews;
        }
         
        public async Task<ProjectViewModel?> GetAsync(Expression<Func<Project, bool>> expression)
        {
            var project = await _projectRepository.GetAsync(expression);

            if (project is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Project not found!");

            var projectViewModel = _mapper.Map<ProjectViewModel>(project);
            projectViewModel.LogoPath = project.LogoPath;

            return projectViewModel;
        }

        public async Task<ProjectViewModel> UpdateAsync(long id, ProjectCreateViewModel createViewModel)
        {
            var entity = await _projectRepository.GetAsync( project => project.Id == id);

            if (entity is null)
                throw new StatusCodeException(HttpStatusCode.NotFound, "Project not found!");

            await _fileService.DeleteImageAsync(entity.LogoPath);

            Project project = _mapper.Map<Project>(createViewModel);
          
            project.Id = id;
            project.LogoPath = await _fileService.SaveImageAsync(createViewModel.Image);

            await _projectRepository.UpdateAsync(project);
            await _dbContext.SaveChangesAsync();

            var projectViewModel = _mapper.Map<ProjectViewModel>(project);
            projectViewModel.LogoPath = project.LogoPath;
            return projectViewModel;
        }
    }
}
