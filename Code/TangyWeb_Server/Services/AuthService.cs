using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Net.Http;
using Tangy_Business.Repository.Interfaces;
using Tangy_Data.Entities;
using Tangy_Models.Dtos;
using TangyWeb_Server.Services.Interfaces;

namespace TangyWeb_Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IAccountRepository _accountRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthService(ILocalStorageService localStorage, IAccountRepository accountRepository,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, 
            AuthenticationStateProvider authenticationStateProvider)
        {
            _localStorage = localStorage;
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationStateProvider = authenticationStateProvider;
        }
        public async Task<LoginResultDto> Login(LoginDto loginDto)
        {

            var usr = await _userManager.FindByEmailAsync(loginDto.Email);
            if (usr == null)
            {
                throw new Exception("User not found!");
            }

            var loginResultDto = new LoginResultDto();

            if (await _signInManager.CanSignInAsync(usr))
            {
                var result = await _signInManager.CheckPasswordSignInAsync(usr, loginDto.Password, true);
                if (result.Succeeded)
                {
                    loginResultDto.Successful = true;
                    loginResultDto.Name = usr.FirstName + " " + usr.LastName;

                    await _localStorage.SetItemAsync("userName", loginResultDto.Name);
                    ((AppStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResultDto.Name);

                }
                else
                {
                    //Model.Error = "Login failed. Check your username and password.";
                }
            }
            else
            {
                //Model.Error = "Your account is blocked";
            }

            return loginResultDto;
        }
    }
}
