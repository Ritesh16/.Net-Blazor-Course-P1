using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using Tangy_Business.Repository.Interfaces;
using Tangy_Models.Dtos;
using TangyWeb_Server.Services.Interfaces;

namespace TangyWeb_Server.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IAccountRepository _accountRepository;

        public AuthService(
                         ILocalStorageService localStorage, IAccountRepository accountRepository)
        {
            _localStorage = localStorage;
            _accountRepository = accountRepository;
        }
        public async Task<LoginResultDto> Login(LoginDto loginDto)
        {
            var output = await _accountRepository.Login(loginDto);
            if (output.Successful)
            {
                await _localStorage.SetItemAsStringAsync("user", output.Name);
            }

            return output;
        }
    }
}
