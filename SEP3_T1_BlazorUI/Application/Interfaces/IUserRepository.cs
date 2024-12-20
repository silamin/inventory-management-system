using SEP3_T1_BlazorUI.Models;

namespace SEP3_T1_BlazorUI.Application.Interfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        void AddUser(UserDTO user);
        void DeleteUser(User user);
        void UpdateUser(User user);
    }
}
