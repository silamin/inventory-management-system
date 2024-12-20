using SEP3_T1_BlazorUI.Models;
using SEP3_T1_BlazorUI.Application.Interfaces;

namespace SEP3_T1_BlazorUI.Application.UseCases
{
    public class UserUseCases
    {
        private readonly IUserRepository _userRepository;

        // Add a static list of roles
        public List<Role> Roles { get; } = new List<Role> { Role.InventoryManager, Role.WarehouseWorker };

        public UserUseCases(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = _userRepository.GetAllUsers();
            Console.WriteLine($"Users count: {users.Count()}"); // Debug line
            return users;
        }

        public void AddUser(UserDTO user)
        {
            if (IsUsernameTaken(user.Username))
            {
                throw new Exception("Username already taken.");
            }

            _userRepository.AddUser(user);
        }

        public IEnumerable<User> GetUsersByRole(Role role)
        {
            var users = _userRepository.GetAllUsers();
            return users.Where(u => u.Role == role);
        }

        public bool IsUsernameTaken(string username)
        {
            var users = _userRepository.GetAllUsers();
            return users.Any(u => u.Username.Equals(username, StringComparison.OrdinalIgnoreCase));
        }
        public void DeleteUser(User user)
        {
            _userRepository.DeleteUser(user); // Implement this in your repository
        }
        public void UpdateUser(User user)
        {
            _userRepository.UpdateUser(user); // Implement this in your repository
        }
    }

}
