using Users;

namespace BlazorServerApp.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task <User>AddUserAsync(UserDTO user);
        Task DeleteUserAsync(User user);
        Task EditUserAsync(User user);
    }

}
