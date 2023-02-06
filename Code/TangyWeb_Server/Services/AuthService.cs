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
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(ILocalStorageService localStorage, IAccountRepository accountRepository,
            UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager,
            AuthenticationStateProvider authenticationStateProvider,
            RoleManager<IdentityRole> roleManager)
        {
            _localStorage = localStorage;
            _accountRepository = accountRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _authenticationStateProvider = authenticationStateProvider;
            _roleManager = roleManager;
        }
        public async Task<LoginResultDto> Login(LoginDto loginDto)
        {

            var loginResult = await _accountRepository.Login(loginDto);

            if (loginResult.Successful)
            {
                await _localStorage.SetItemAsync<UserInfoDto>("userName", new UserInfoDto(loginResult.User.Name, loginDto.Email));
                ((AppStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResult.User.Name, loginDto.Email);
            }

            //var user = await _userManager.FindByEmailAsync(loginDto.Email);
            //var loginResultDto = new LoginResultDto();
            //if (user == null)
            //{
            //    loginResultDto.Error = "User not found!";
            //    return loginResultDto;
            //}

            //if (await _signInManager.CanSignInAsync(user))
            //{
            //    var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, true);
            //    if (result.Succeeded)
            //    {
            //        loginResultDto.Successful = true;
            //        loginResultDto.Name = user.FirstName + " " + user.LastName;

            //        await _localStorage.SetItemAsync<UserInfoDto>("userName", new UserInfoDto(loginResultDto.Name, loginDto.Email));
            //        ((AppStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResultDto.Name, loginDto.Email);

            //    }
            //    else
            //    {
            //        loginResultDto.Error = "Login failed. Check your username and password.";
            //    }
            //}
            //else
            //{
            //    loginResultDto.Error = "Your account is blocked";
            //}

            return loginResult;
        }

        public async Task<string> GetUserName()
        {
            var name = await _localStorage.GetItemAsStringAsync("userName");
            return name;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("userName");
            ((AppStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        }
    }
}
