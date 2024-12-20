using SEP3T1BlazorUI.Infrastructure;
using System.Threading.Tasks;

namespace BlazorServerApp.Data
{
    public class AuthRepository
    {
        private readonly AuthService.AuthServiceClient _client;

        public AuthRepository(AuthService.AuthServiceClient client)
        {
            _client = client;
        }

        public async Task<string> LoginAsync(string username, string password)
        {
            try
            {
                var request = new LoginRequest
                {
                    Username = username,
                    Password = password
                };

                var response = await _client.loginAsync(request);
                return response.Token;
            }
            catch (Exception ex)
            {
                return $"Login failed: {ex.Message}";
            }
        }

        public async Task<string> RegisterAsync(string username, string password, string role)
        {
            try
            {
                var request = new RegisterRequest
                {
                    Username = username,
                    Password = password
                };

                var response = await _client.registerAsync(request);
                return $"User registered: {response.Username}";
            }
            catch (Exception ex)
            {
                return $"Registration failed: {ex.Message}";
            }
        }
    }
}
