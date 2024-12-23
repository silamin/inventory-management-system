using BlazorServerApp.Application.UseCases;
using Microsoft.AspNetCore.Components.Authorization;
using Grpc.Core; 
using System.Security.Claims;
using System.ComponentModel; 

public class LoginManager : INotifyPropertyChanged
{
    private readonly AuthUseCases _authUseCases;
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public LoginRequest LoginRequest { get; set; } = new();

    // Ensure ErrorMessage is always initialized
    public string ErrorMessage { get; set; } = string.Empty;

    private bool _isLoading;
    public bool IsLoading
    {
        get => _isLoading;
        private set
        {
            if (_isLoading != value)
            {
                _isLoading = value;
                OnPropertyChanged(nameof(IsLoading));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public LoginManager(AuthUseCases authUseCases, AuthenticationStateProvider authenticationStateProvider)
    {
        _authUseCases = authUseCases;
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<bool> AttemptLoginAsync()
    {
        IsLoading = true;
        try
        {
            ErrorMessage = string.Empty; // Clear error messages before login attempt
            var token = await _authUseCases.Login(LoginRequest);
            if (!string.IsNullOrEmpty(token))
            {
                await((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(token);

                return true;
            }
            else
            {
                return false;
            }
        }
        catch (RpcException rpcEx) when (rpcEx.StatusCode == StatusCode.Unauthenticated)
        {
            // Handle 401 Unauthorized
            ErrorMessage = "Wrong credentials. Please try again.";
            return false;
        }
        catch (RpcException rpcEx) when (rpcEx.StatusCode == StatusCode.Unavailable)
        {
            ErrorMessage = "The server is currently unavailable. Please try again later.";
            return false;
        }
        catch (RpcException rpcEx)
        {
            // Handle other RPC exceptions
            ErrorMessage = $"Unexpected server error: {rpcEx.Status.Detail}";
            return false;
        }
        catch (HttpRequestException)
        {
            ErrorMessage = "Network issue. Please check your internet connection.";
            return false;
        }
        catch (TimeoutException)
        {
            ErrorMessage = "The server took too long to respond. Please try again later.";
            return false;
        }
        catch (Exception ex)
        {
            // Log additional information for debugging
            Console.WriteLine($"Unexpected error: {ex.Message}");
            ErrorMessage = "An unexpected error occurred. Please try again later.";
            return false;
        }
        finally
        {
            IsLoading = false; // Set loading to false
        }
    }



    public async Task<string> GetUserRoleAsync()
    {
        var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity?.IsAuthenticated == true)
        {
            var role = user.FindFirst(ClaimTypes.Role)?.Value;
            Console.WriteLine($"Parsed Role: {role}"); // Debug log
            return role?.ToUpperInvariant() ?? string.Empty; // Normalize role to uppercase
        }

        return string.Empty;
    }



    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
public async Task LogoutAsync()
{
    await ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
    
    }

}
