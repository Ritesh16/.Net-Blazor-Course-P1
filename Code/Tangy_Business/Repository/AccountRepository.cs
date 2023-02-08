using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.VisualBasic;
using System;
using Tangy_Business.Repository.Interfaces;
using Tangy_Data.Entities;
using Tangy_Models.Dtos;
using Tangy_Common;

namespace Tangy_Business.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountRepository(UserManager<ApplicationUser> userManager,
                        SignInManager<ApplicationUser> signInManager, 
                        RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager; 
        }

        public async Task<IList<string>> GetUserRoles(string email)
        {
            var roles = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(email));
            return roles;
        }

        public async Task<LoginResultDto> Login(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            var loginResultDto = new LoginResultDto();
            if (user == null)
            {
                loginResultDto.Error = "User not found!";
                return loginResultDto;
            }

            if (await _signInManager.CanSignInAsync(user))
            {
                var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, true);
                if (result.Succeeded)
                {
                    loginResultDto.Successful = true;
                    loginResultDto.User.Name = user.FirstName + " " + user.LastName;
                    loginResultDto.User.Email = user.Email;
                }
                else
                {
                    loginResultDto.Error = "Login failed. Check your username and password.";
                }
            }
            else
            {
                loginResultDto.Error = "Your account is blocked";
            }

            return loginResultDto;
        }

        public async Task<OutputDto> Register(RegisterDto model)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailConfirmed = true,
                PhoneNumber = model.PhoneNumber
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(x => x.Description);

                return new OutputDto { Successful = false, Errors = errors };

            }

            await _userManager.AddToRoleAsync(user, Tangy_Common.Constants.Role_Customer);

            return new OutputDto { Successful = true, Message = "Your account is created successfully." };
        }
    }
}
