using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Tangy_Business.Repository.Interfaces;
using Tangy_Data.Entities;
using Tangy_Models.Dtos;

namespace TangyWeb_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;

        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            var output = await _accountRepository.Register(model);
            return Ok(output);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDto model)
        {
            var output = await _accountRepository.Login(model);
            if (!output.Successful)
            {
                return Unauthorized(output);
            }

            return Ok(output);
        }
    }
}
