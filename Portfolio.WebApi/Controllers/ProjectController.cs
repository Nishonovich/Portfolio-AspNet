using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.WebApi.Commons.Utils;
using Portfolio.WebApi.Interfaces.Services;
using Portfolio.WebApi.Services;
using Portfolio.WebApi.ViewModels.Projects;
using Portfolio.WebApi.ViewModels.Users;

namespace Portfolio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpPost, Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> CreateAsync([FromForm] ProjectCreateViewModel projectCreateViewModel)
        {
            long userId = long.Parse(HttpContext.User.FindFirst("Id")?.Value ?? "0");
            return Ok(await _projectService.CreateAsync(userId, projectCreateViewModel));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            return Ok(await _projectService.GetAllAsync(expression: null, @params));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _projectService.DeleteAsync(project => project.Id == id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await _projectService.GetAsync(project => project.Id == id));
        }

        [HttpPut, Authorize(Roles = "User, Admin")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] ProjectCreateViewModel projectCreateViewModell)
        {
            long userId = long.Parse(HttpContext.User.FindFirst("Id")?.Value ?? "0");
            return Ok(await _projectService.UpdateAsync(id, projectCreateViewModell));
        }
    }
}
