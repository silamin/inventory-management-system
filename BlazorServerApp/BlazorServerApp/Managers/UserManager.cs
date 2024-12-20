using Microsoft.AspNetCore.Components.Forms;
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
        public List<User> Users { get; private set; } = new();
        public string SearchQuery { get; set; } = string.Empty;

        public User? EditingUser { get; private set; }

        public async Task<IEnumerable<IGrouping<UserRole, User>>> GetGroupedUsersAsync()
        {
            try
            {
                // Call the RefreshUsersAsync method to ensure users are up-to-date
                await RefreshUsersAsync();

                Console.WriteLine($"[UserManager] Users loaded: {Users.Count}");

                // Filter and group users
                var groupedUsers = Users
                    .Where(u => u.IsActive) 
                    .Where(u => string.IsNullOrEmpty(SearchQuery) ||
                                u.Username.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase))
                    .GroupBy(u => u.UserRole);

                Console.WriteLine($"[UserManager] Number of groups: {groupedUsers.Count()}");

                return groupedUsers;
            }
            catch (Exception ex)
            {
                Console.WriteLine($" [UserManager] Error occurred while grouping users: {ex.Message}");
                Console.WriteLine(ex.StackTrace);
                return Enumerable.Empty<IGrouping<UserRole, User>>(); // Return an empty result on failure
            }
        }

        public UserManager(UserUseCases userUseCases, IToastService toastService)
        {
            _userUseCases = userUseCases;
            _toastService = toastService;
        }

        /// <summary>
        /// Loads users on initialization
        /// </summary>
        public async Task InitializeAsync()
        {
            await RefreshUsersAsync();
        }

        /// <summary>
        /// Refreshes the user list from the backend
        /// </summary>
        public async Task RefreshUsersAsync()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;
                Users = (await _userUseCases.GetAllUsersAsync()).Where(u => u.IsActive).ToList(); 
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

        /// <summary>
        /// Clears the search query
        /// </summary>
        public void ClearSearch() => SearchQuery = string.Empty;

        /// <summary>
        /// Converts the Role Enum to a human-readable string
        /// </summary>
        public string HumanizeRole(UserRole role)
        {
            return role switch
            {
                UserRole.InventoryManager => "Inventory Manager",
                UserRole.WarehouseWorker => "Warehouse Worker",
                _ => role.ToString()
            };
        }

        /// <summary>
        /// Toggles edit mode for the selected user
        /// </summary>
        public void ToggleEditUser(User user)
        {
            // If we're already editing this user, exit edit mode
            if (EditingUser?.Userid == user.Userid)
            {
                EditingUser = null;
                return;
            }

            // Otherwise, set this user as the current editing user
            EditingUser = new User
            {
                Username = user.Username,
                Password = string.Empty, // Don't pre-populate passwords for security
                Userid = user.Userid,
                UserRole = user.UserRole
            };
        }

        /// <summary>
        /// Deletes a user and updates the user list
        /// </summary>
        public async Task DeleteUserAsync(User user)
        {
            try
            {
                await _userUseCases.DeleteUserAsync(user);
                Users.Remove(user); // Remove the user from the local list
                _toastService.ShowSuccess($"User '{user.Username}' was deleted successfully.");
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

                // Update the user in the Users list
                var userIndex = Users.FindIndex(u => u.Userid == EditingUser.Userid);
                if (userIndex != -1)
                {
                    Users[userIndex] = new User
                    {
                        Username = EditingUser.Username,
                        Password = EditingUser.Password,
                        Userid = EditingUser.Userid,
                        UserRole = EditingUser.UserRole,
                        IsActive = EditingUser.IsActive
                    };
                }

                // Re-group users after the edit
                await GetGroupedUsersAsync();

                _toastService.ShowSuccess("User details updated successfully.");
                EditingUser = null; // Exit edit mode
            }
            catch (Exception ex)
            {
                _toastService.ShowError("An error occurred while updating the user: " + ex.Message);
            }
        }


        /// <summary>
        /// Cancels edit mode without saving changes
        /// </summary>
        public void CancelEdit()
        {
            EditingUser = null;
        }
    }
}
