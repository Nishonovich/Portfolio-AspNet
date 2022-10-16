using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Portfolio.WebApi.Interfaces.Services;
using Portfolio.WebApi.ViewModels.Users;

namespace Portfolio.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService _accountServcie;

        public AccountsController(IAccountService accountService)
        {
            _accountServcie = accountService;
        }
        [HttpPost("registr")]
        public async Task<IActionResult> CreateAsync([FromForm] UserCreateViewModel user)
        {
            return Ok(await _accountServcie.RegistrAsync(user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromForm] UserLoginModel user)
        {
            return Ok(new { Token = await _accountServcie.LoginAsync(user) });
        }

        [HttpPost("emailVerify")]
        public async Task<IActionResult> EmailVerify([FromForm] EmailAddress emailAddress)
        {
            return Ok(new { Token = await _accountServcie.EmailVerifyAsync(emailAddress) });
        }

    }
}
