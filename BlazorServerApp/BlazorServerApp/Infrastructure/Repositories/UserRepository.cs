using BlazorServerApp.Application.Interfaces;
using Grpc.Core;
using Users;

namespace BlazorServerApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserService.UserServiceClient _client;
        private readonly CustomAuthenticationStateProvider _authStateProvider;


        public UserRepository(UserService.UserServiceClient client, CustomAuthenticationStateProvider authStateProvider)
        {
            _client = client;
            _authStateProvider = authStateProvider;
        }

        public async Task<GetUser> AddUserAsync(CreateUser user)
        {
            try
            {
                var callOptions = await GetAuthenticatedCallOptionsAsync();
                var response = await _client.addUserAsync(user, callOptions);

                return response;

            }
            catch (RpcException ex)
            {
                throw new ApplicationException("Error deleting User", ex);
            }
        }

        public async Task DeleteUserAsync(DeleteUser user)
        {
            try
            {
                var callOptions = await GetAuthenticatedCallOptionsAsync();
                await _client.deleteUserAsync(user, callOptions);
            }
            catch (RpcException ex)
            {
                throw new ApplicationException("Error deleting User", ex);
            }
        }


        public async Task EditUserAsync(User user)
        {
            try
            {
                var callOptions = await GetAuthenticatedCallOptionsAsync();
                await _client.editUserAsync(user, callOptions);
            }
            catch (RpcException ex)
            {
                throw new ApplicationException("Error deleting User", ex);
            }
        }

        public async Task<IEnumerable<GetUser>> GetUsersAsync(UserRole role)
        {
            try
            {
                var callOptions = await GetAuthenticatedCallOptionsAsync();
                var request = new Role { UserRole = role };

                var response = await _client.getUsersAsync(request, callOptions);

                return response.Users;
            }
            catch (RpcException ex)
            {
                throw new ApplicationException("Error retrieving users by role", ex);
            }
        }

        private async Task<CallOptions> GetAuthenticatedCallOptionsAsync()
        {
            var token = await _authStateProvider.GetTokenAsync();

            var metadata = new Metadata();
            if (!string.IsNullOrEmpty(token))
            {
                metadata.Add("Authorization", $"Bearer {token}");
            }

            return new CallOptions(metadata);
        }

    }
}
