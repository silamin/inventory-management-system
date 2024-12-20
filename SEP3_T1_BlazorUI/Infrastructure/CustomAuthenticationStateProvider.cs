using SEP3_T1_BlazorUI.Application.Interfaces;
using Blazored.LocalStorage; // Added for ILocalStorageService
using Microsoft.AspNetCore.Components.Authorization; // Added for AuthenticationStateProvider, AuthenticationState
using System.Security.Claims; // Added for Claim, ClaimsIdentity, ClaimsPrincipal

namespace SEP3_T1_BlazorUI.Infrastructure
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILocalStorageService _localStorage;

        public CustomAuthenticationStateProvider(IAuthRepository authRepository, ILocalStorageService localStorage)
        {
            _authRepository = authRepository;
            _localStorage = localStorage;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");

            if (string.IsNullOrEmpty(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var claims = ParseClaimsFromToken(token).ToList();

            // Check for token expiration
            var expirationClaim = claims.FirstOrDefault(c => c.Type == "exp")?.Value;
            if (!string.IsNullOrEmpty(expirationClaim) && long.TryParse(expirationClaim, out long expirationTimestamp))
            {
                var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationTimestamp).UtcDateTime;
                if (expirationDateTime < DateTime.UtcNow)
                {
                    // Token has expired
                    await MarkUserAsLoggedOut();
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }
            }

            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        public async Task MarkUserAsAuthenticated(string token)
        {
            var claims = ParseClaimsFromToken(token).ToList();
            var expirationClaim = claims.FirstOrDefault(c => c.Type == "exp")?.Value;

            if (!string.IsNullOrEmpty(expirationClaim) && long.TryParse(expirationClaim, out long expirationTimestamp))
            {
                var expirationDateTime = DateTimeOffset.FromUnixTimeSeconds(expirationTimestamp).UtcDateTime;
                if (expirationDateTime < DateTime.UtcNow)
                {
                    // Do not authenticate if the token is expired
                    await MarkUserAsLoggedOut();
                    return;
                }
            }

            await _localStorage.SetItemAsync("authToken", token);

            var identity = new ClaimsIdentity(claims, "jwt");
            var user = new ClaimsPrincipal(identity);
            var authenticationState = new AuthenticationState(user);

            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        public async Task MarkUserAsLoggedOut()
        {
            await _localStorage.RemoveItemAsync("authToken");

            var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
            var authenticationState = new AuthenticationState(anonymous);

            NotifyAuthenticationStateChanged(Task.FromResult(authenticationState));
        }

        private IEnumerable<Claim> ParseClaimsFromToken(string token)
        {
            var parts = token.Split('|');
            if (parts.Length == 4) // Assuming new token format: Username|WorkingNumber|Role|ExpirationTimestamp
            {
                return new List<Claim>
                {
                    new Claim(ClaimTypes.Name, parts[0]), // Username
                    new Claim("WorkingNumber", parts[1]), // Working Number
                    new Claim(ClaimTypes.Role, parts[2]), // Role
                    new Claim("exp", parts[3])            // Expiration
                };
            }
            return new List<Claim>();
        }
    }
}
