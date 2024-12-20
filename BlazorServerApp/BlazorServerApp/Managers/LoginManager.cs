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
    public string ErrorMessage { get; private set; } = string.Empty;

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
            var token = await _authUseCases.Login(LoginRequest);
            if (!string.IsNullOrEmpty(token))
            {
                ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(token);
                return true;
            }
            else
            {
                ErrorMessage = "Invalid username or password. Please try again.";
                return false;
            }
        }
        catch (RpcException rpcEx) when (rpcEx.StatusCode == StatusCode.Unavailable)
        {
            ErrorMessage = "The server is currently unavailable. Please try again later.";
            return false;
        }
        catch (RpcException rpcEx) when (rpcEx.StatusCode == StatusCode.PermissionDenied)
        {
            ErrorMessage = "Invalid username or password. Please try again.";
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
        catch (Exception)
        {
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
            return user.FindFirst(ClaimTypes.Role)?.Value ?? string.Empty;
        }

        return string.Empty;
    }

    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
