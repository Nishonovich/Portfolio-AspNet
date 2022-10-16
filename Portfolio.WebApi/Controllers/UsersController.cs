using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.WebApi.Commons.Utils;
using Portfolio.WebApi.Interfaces.Services;
using Portfolio.WebApi.ViewModels.Users;

namespace Portfolio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params)
        {
            return Ok(await _userService.GetAllAsync(expression: null, @params));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            return Ok(await _userService.DeleteAsync(user => user.Id == id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(long id)
        {
            return Ok(await _userService.GetAsync(user => user.Id == id));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm] UserCreateViewModel userCreateViewModel)
        {
            return Ok(await _userService.UpdateAsync(id, userCreateViewModel));
        }
    }
}
