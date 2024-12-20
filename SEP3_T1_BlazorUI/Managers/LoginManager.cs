using Microsoft.JSInterop;
using SEP3_T1_BlazorUI.Models;
using SEP3_T1_BlazorUI.Application.UseCases;
using Microsoft.AspNetCore.Components.Authorization;
using SEP3_T1_BlazorUI.Infrastructure;
using System.Security.Claims;
using SEP3T1BlazorUI.Infrastructure;
using System.Net.Http;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;


namespace SEP3_T1_BlazorUI.Presentation.Managers
{
    public class LoginManager
    {
        private readonly HttpClient _httpClient;


        public LoginManager(AuthUseCases authUseCases, AuthenticationStateProvider authenticationStateProvider, HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public LoginRequest LoginRequest { get; set; } = new LoginRequest();
        public string ErrorMessage { get; set; } = string.Empty;

        public async Task<bool> AttemptLoginAsync()
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("https://localhost:5001/api/auth/login", LoginRequest); // URL of Blazor Server app

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

                    // Store token (could be in local storage, session storage, or in-memory variable)
                    if (result != null && !string.IsNullOrEmpty(result.Token))
                    {
                        // Assuming you're using localStorage (can also use sessionStorage)
                        return true;
                    }
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    ErrorMessage = "Invalid username or password.";
                }
                else
                {
                    ErrorMessage = "Login failed. Please try again later.";
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"An error occurred while logging in: {ex.Message}";
            }

            return false;
        }
        public async Task<string> GetUserRole()
        {
            return "test";
        }
    }
}
