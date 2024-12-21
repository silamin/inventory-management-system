using BlazorServerApp.Application.Interfaces;
using Grpc.Core;
using Users;

namespace BlazorServerApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserService.UserServiceClient _client;

        public UserRepository(UserService.UserServiceClient client)
        {
            _client = client;
        }

        public async Task<GetUser> AddUserAsync(CreateUser user)
        {
            try
            {
                var response = await _client.addUserAsync(user);

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
                await _client.deleteUserAsync(user);
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
                await _client.editUserAsync(user);
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
                var request = new Role { UserRole = role };

                var response = await _client.getUsersAsync(request);

                return response.Users;
            }
            catch (RpcException ex)
            {
                throw new ApplicationException("Error retrieving users by role", ex);
            }
        }

    }
}
