using Users;

namespace BlazorServerApp.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<GetUser>> GetAllUsersAsync();
        Task <GetUser>AddUserAsync(CreateUser user);
        Task DeleteUserAsync(DeleteUser user);
        Task EditUserAsync(User user);
    }

}
