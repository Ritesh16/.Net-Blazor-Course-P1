using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;

namespace TangyWeb_Server
{
    public class AppStateProvider : AuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        
        public AppStateProvider(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            //var userName = await _localStorage.GetItemAsync<string>("userName");

            //if (string.IsNullOrWhiteSpace(userName))
            //{
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            //}

            //return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity("userName", userName, "")));
        }

        public void MarkUserAsAuthenticated(string userName)
        {
            var authenticatedUser = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, userName) }, "userName"));
            var authState = Task.FromResult(new AuthenticationState(authenticatedUser));
            NotifyAuthenticationStateChanged(authState);
        }

        public void MarkUserAsLoggedOut()
        {
            var anonymousUser = new ClaimsPrincipal(new ClaimsIdentity());
            var authState = Task.FromResult(new AuthenticationState(anonymousUser));
            NotifyAuthenticationStateChanged(authState);
        }
    }
}
