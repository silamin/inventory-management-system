using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    private readonly ProtectedSessionStorage _protectedSessionStore;
    private string _cachedToken = null; // Cache the token after first successful fetch
    private bool _isInitialized = false; // Check if the storage has been initialized

    public CustomAuthenticationStateProvider(ProtectedSessionStorage protectedSessionStore)
    {
        _protectedSessionStore = protectedSessionStore;
        InitializeAsync(); // Call InitializeAsync during construction
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_isInitialized && !string.IsNullOrEmpty(_cachedToken))
        {
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(_cachedToken), "jwt");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        return new AuthenticationState(_anonymous);
    }

    public async void MarkUserAsAuthenticated(string token)
    {
        _cachedToken = token; // Cache the token to avoid unnecessary calls
        if (_isInitialized)
        {
            await _protectedSessionStore.SetAsync("authToken", token);
        }

        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        var user = new ClaimsPrincipal(identity);

        // Notify Blazor that the authentication state has changed
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async void MarkUserAsLoggedOut()
    {
        _cachedToken = null; // Clear the token
        if (_isInitialized)
        {
            await _protectedSessionStore.DeleteAsync("authToken");
        }

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    private async Task InitializeAsync()
    {
        if (!_isInitialized)
        {
            var tokenResult = await _protectedSessionStore.GetAsync<string>("authToken");
            _cachedToken = tokenResult.Value;
            _isInitialized = true;

            // Notify Blazor that the authentication state has changed
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var claims = new List<Claim>();
        var payload = jwt.Split('.')[1];
        var jsonBytes = Convert.FromBase64String(PadBase64(payload));
        var keyValuePairs = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

        if (keyValuePairs != null)
        {
            foreach (var kvp in keyValuePairs)
            {
                claims.Add(new Claim(kvp.Key, kvp.Value.ToString()));
            }
        }

        return claims;
    }

    private string PadBase64(string base64)
    {
        switch (base64.Length % 4)
        {
            case 2: return base64 + "==";
            case 3: return base64 + "=";
            default: return base64;
        }
    }
}
