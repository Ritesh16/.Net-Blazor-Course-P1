﻿using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using TangyWeb_Client.Services.Interfaces;
using System.Reflection.Metadata;
using Tangy_Models.Dtos;
using Tangy_Common;

namespace TangyWeb_Client.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<OutputDto> Register(RegisterDto registerModel)
        {
            var registerModelJson = JsonSerializer.Serialize(registerModel);
            var response = await _httpClient.PostAsync("api/account/register", new StringContent(registerModelJson, Encoding.UTF8, "application/json"));
            var registerResponse = JsonSerializer.Deserialize<OutputDto>(await response.Content.ReadAsStringAsync(),
                                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return registerResponse;
        }

        public async Task<LoginResultDto> Login(LoginDto loginModel)
        {
            var loginAsJson = JsonSerializer.Serialize(loginModel);
            var response = await _httpClient.PostAsync("api/account/Login", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
            var loginResult = JsonSerializer.Deserialize<LoginResultDto>(await response.Content.ReadAsStringAsync(), 
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
            {
                return loginResult;
            }

            await _localStorage.SetItemAsync(Constants.AuthToken, loginResult.Token);
            await _localStorage.SetItemAsync(Constants.UserDetails, loginResult.User);
            ((AuthStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResult.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync(Constants.AuthToken);
            await _localStorage.RemoveItemAsync(Constants.UserDetails);
            ((AuthStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }
    }
}
