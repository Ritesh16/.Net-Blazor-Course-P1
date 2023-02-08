using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tangy_Business.Repository.Interfaces;
using Tangy_Data.Entities;
using Tangy_Models.Dtos;
using TangyWeb_Api.Helper;

namespace TangyWeb_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly APISettings _apiSettings;

        public AccountController(IAccountRepository accountRepository, IOptions<APISettings> options)
        {
            _accountRepository = accountRepository;
            _apiSettings = options.Value;
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

            //var user = new ApplicationUser()
            var signinCredentials = GetSigningCredentials();
            var claims = await GetClaims(output);

            var tokenOptions = new JwtSecurityToken(
                issuer: _apiSettings.ValidIssuer,
                audience: _apiSettings.ValidAudience,
                claims: claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: signinCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            output.Token = token;

            return Ok(output);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_apiSettings.SecretKey));
            return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaims(LoginResultDto loginResult)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,loginResult.User.Name),
                new Claim(ClaimTypes.Email,loginResult.User.Email)
            };

            var roles = await _accountRepository.GetUserRoles(loginResult.User.Email);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            return claims;
        }
    }
}
