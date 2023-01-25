using Microsoft.AspNetCore.Identity;
using System;
using Tangy_Business.Repository.Interfaces;
using Tangy_Data.Entities;
using Tangy_Models.Dtos;

namespace Tangy_Business.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<LoginResultDto> Login(LoginDto model)
        {
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            try
            {
                var ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, lockoutOnFailure: false);
                var output = new LoginResultDto();
                if (result.Succeeded)
                {
                    var appUser = _userManager.Users.FirstOrDefault(x => x.Email == model.Email);
                    output.Successful = true;
                    output.Name = appUser.FirstName + " " + appUser.LastName;
                }

                if (result.IsLockedOut)
                {
                    output.Successful = false;
                    output.Error = "Account is locked out.";
                }
                else
                {
                    output.Error = "Email/Password does not match.";
                }

                return output;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<OutputDto> Register(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return new OutputDto { Successful = false, Errors = errors };

            }

            return new OutputDto { Successful = true, Message = "Your account is created successfully." };
        }
    }
}
