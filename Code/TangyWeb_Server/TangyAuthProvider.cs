using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace TangyWeb_Server
{
    public class TangyAuthProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;

        public TangyAuthProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var user = await _localStorage.GetItemAsync<string>("tangy_user");

            if (string.IsNullOrWhiteSpace(user))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            return null;
        }
    }
}
