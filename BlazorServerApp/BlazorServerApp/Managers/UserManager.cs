using Blazored.Toast.Services;
using BlazorServerApp.Application.UseCases;
using Users;

namespace BlazorServerApp.Managers
{
    public class UserManager
    {
        private readonly UserUseCases _userUseCases;
        private readonly IToastService _toastService;

        public bool IsLoading { get; private set; } = true;
        public string ErrorMessage { get; private set; } = string.Empty;
        public List<GetUser> Users { get; private set; } = new();
        public string SearchQuery { get; set; } = string.Empty;
        public User? EditingUser { get; private set; }

        public UserManager(UserUseCases userUseCases, IToastService toastService)
        {
            _userUseCases = userUseCases;
            _toastService = toastService;
        }

        public async Task RefreshUsersAsync(UserRole role)
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var usersByRole = await _userUseCases.GetUsersByRoleAsync(role); // Pass the role here
                Users = usersByRole.Select(u => new GetUser
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    UserRole = u.UserRole
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred while fetching users: " + ex.Message;
            }
            finally
            {
                IsLoading = false;
            }
        }


        public async Task<List<User>> GetUsersByRoleAsync(UserRole role)
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var users = await _userUseCases.GetUsersByRoleAsync(role);
                return users.Select(u => new User
                {
                    UserId = u.UserId,
                    Username = u.UserName,
                    Password = string.Empty,
                    UserRole = u.UserRole
                }).ToList();
            }
            catch (Exception ex)
            {
                ErrorMessage = "An error occurred while fetching users: " + ex.Message;
                return new List<User>();
            }
            finally
            {
                IsLoading = false;
            }
        }

        public void ClearSearch() => SearchQuery = string.Empty;

        public string HumanizeRole(UserRole role)
        {
            return role switch
            {
                UserRole.InventoryManager => "Inventory Manager",
                UserRole.WarehouseWorker => "Warehouse Worker",
                _ => role.ToString()
            };
        }

        public void ToggleEditUser(GetUser user)
        {
            if (EditingUser?.UserId == user.UserId)
            {
                EditingUser = null;
                return;
            }

            EditingUser = new User
            {
                UserId = user.UserId,
                Username = user.UserName,
                Password = string.Empty,
                UserRole = user.UserRole
            };
        }

        public async Task DeleteUserAsync(DeleteUser user)
        {
            try
            {
                await _userUseCases.DeleteUserAsync(user);
                Users.RemoveAll(u => u.UserId == user.UserId);
                _toastService.ShowSuccess($"User with UserId '{user.UserId}' was deleted successfully.");
            }
            catch (Exception ex)
            {
                _toastService.ShowError("An error occurred while deleting the user: " + ex.Message);
            }
        }

        public async Task SaveUserAsync()
        {
            if (EditingUser == null) return;

            try
            {
                await _userUseCases.EditUserAsync(EditingUser);

                var userIndex = Users.FindIndex(u => u.UserId == EditingUser.UserId);
                if (userIndex != -1)
                {
                    Users[userIndex] = new GetUser
                    {
                        UserId = EditingUser.UserId,
                        UserName = EditingUser.Username,
                        UserRole = EditingUser.UserRole
                    };
                }

                _toastService.ShowSuccess("User details updated successfully.");
                EditingUser = null;
            }
            catch (Exception ex)
            {
                _toastService.ShowError("An error occurred while updating the user: " + ex.Message);
            }
        }

        public void CancelEdit()
        {
            EditingUser = null;
        }
    }
}
