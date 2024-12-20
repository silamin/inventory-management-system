using BlazorServerApp.Application.Interfaces;

namespace BlazorServerApp.Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AuthService.AuthServiceClient _client;

        public AuthRepository(AuthService.AuthServiceClient client)
        {
            _client = client;
        }

        public async Task<string> LoginAsync(LoginRequest loginRequest)
        {
            var response = await _client.loginAsync(loginRequest);
            Console.WriteLine("Response from gRPC server:");
            Console.WriteLine($"Token: {response.Token}");

            return response.Token;
        }
    }
}