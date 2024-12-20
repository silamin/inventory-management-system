using BlazorServerApp.Application.Interfaces;
using Users;

namespace BlazorServerApp.Application.UseCases
{
    public class UserUseCases
    {
        private readonly IUserRepository _userRepository;

        public UserUseCases(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            try
            {
                return await _userRepository.GetAllUsersAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving users", ex);
            }
        }

        internal async Task DeleteUserAsync(User user)
        {
            try
            {
                 await _userRepository.DeleteUserAsync(user);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving users", ex);
            }
        }

        internal async Task EditUserAsync(User editingUser)
        {
            try
            {
                await _userRepository.EditUserAsync(editingUser);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error retrieving users", ex);
            }
        }
    }
}
