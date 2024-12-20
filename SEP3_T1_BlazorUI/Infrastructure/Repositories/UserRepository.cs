using SEP3_T1_BlazorUI.Application.Interfaces;
using SEP3_T1_BlazorUI.Models;

namespace SEP3_T1_BlazorUI.Infrastructure.Repositories
{
    public class UserRepository: IUserRepository
    {
        private readonly List<User> _users;

        public UserRepository()
        {
            _users = new List<User>
        {
new User { Username = "admin", Password = "admin", WorkingNumber = 1, Role = Role.InventoryManager },
new User { Username = "worker", Password = "worker", WorkingNumber = 2, Role = Role.WarehouseWorker }
            };
        }

        public IEnumerable<User> GetAllUsers() => _users;

        public void AddUser(UserDTO userDTO)
        {
            // Generate a new WorkingNumber for the new user
            int newWorkingNumber = _users.Count == 0 ? 1 : _users.Max(u => u.WorkingNumber) + 1;

            // Convert UserDTO to User and generate the working number
            var newUser = new User
            {
                Username = userDTO.Username,
                Password = userDTO.Password,
                Role = userDTO.Role!.Value,
                WorkingNumber = newWorkingNumber
            };

            _users.Add(newUser);
        }

        public void DeleteUser(User user)
        {
            _users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            var _user = _users.FirstOrDefault(u => u.WorkingNumber == user.WorkingNumber);
            if (user != null)
            {
                _user.Username = user.Username;
                _user.Password = user.Password;
                _user.Role = user.Role;
            }
            else
            {
                throw new Exception("User not found");
            }
        }
    }
}
