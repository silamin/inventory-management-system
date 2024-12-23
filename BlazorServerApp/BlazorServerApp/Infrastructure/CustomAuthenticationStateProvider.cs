using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;

public class CustomAuthenticationStateProvider : AuthenticationStateProvider
{
    private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());
    private readonly ProtectedSessionStorage _protectedSessionStore;
    private string? _cachedToken;

    public CustomAuthenticationStateProvider(ProtectedSessionStorage protectedSessionStore)
    {
        _protectedSessionStore = protectedSessionStore;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        if (_cachedToken == null)
        {
            _cachedToken = await GetTokenAsync();
        }

        if (!string.IsNullOrEmpty(_cachedToken))
        {
            var identity = new ClaimsIdentity(ParseClaimsFromJwt(_cachedToken), "jwt");
            var user = new ClaimsPrincipal(identity);
            return new AuthenticationState(user);
        }

        return new AuthenticationState(_anonymous);
    }

    public async Task MarkUserAsAuthenticated(string token)
    {
        _cachedToken = token;
        await _protectedSessionStore.SetAsync("authToken", token);

        var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");
        var user = new ClaimsPrincipal(identity);

        // Notify Blazor that the authentication state has changed
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }

    public async Task MarkUserAsLoggedOut()
    {
        _cachedToken = null;
        await _protectedSessionStore.DeleteAsync("authToken");

        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(_anonymous)));
    }

    public async Task<string?> GetTokenAsync()
    {
        try
        {
            var tokenResult = await _protectedSessionStore.GetAsync<string>("authToken");
            return tokenResult.Success ? tokenResult.Value : null;
        }
        catch (InvalidOperationException)
        {
            // Log warning: JavaScript interop not available during prerendering.
            return null;
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
                var claimType = kvp.Key.Equals("role", StringComparison.OrdinalIgnoreCase)
                                ? ClaimTypes.Role
                                : kvp.Key;
                claims.Add(new Claim(claimType, kvp.Value.ToString()));
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
