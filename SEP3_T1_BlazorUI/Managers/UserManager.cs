using SEP3_T1_BlazorUI.Application.UseCases;
using SEP3_T1_BlazorUI.Models;
using Microsoft.AspNetCore.Components.Forms;
using Blazored.Toast.Services;

namespace SEP3_T1_BlazorUI.Presentation.Managers
{
    public class UserManager
    {
        private readonly UserUseCases _userUseCases;
        private readonly IToastService _toastService;

        public UserDTO NewUser { get; private set; }
        public EditContext EditContext { get; private set; }
        public bool IsLoading { get; private set; }
        public string SearchQuery { get; set; } = string.Empty;
        public User? EditingUser { get; private set; }

        public List<Role> Roles => _userUseCases.Roles;

        public IEnumerable<IGrouping<Role, User>> GroupedUsers =>
            _userUseCases.GetAllUsers()
                .Where(u => string.IsNullOrEmpty(SearchQuery) ||
                            u.Username.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                            u.WorkingNumber.ToString().Contains(SearchQuery))
                .GroupBy(u => u.Role);

        public UserManager(UserUseCases userUseCases, IToastService toastService)
        {
            _userUseCases = userUseCases;
            _toastService = toastService;
            NewUser = new UserDTO
            {
                Role = Role.WarehouseWorker
            };
            EditContext = new EditContext(NewUser);
        }

        //Add new user
        public async Task HandleAddUser()
        {
            if (!EditContext.Validate())
                return;

            IsLoading = true;

            try
            {
                var isUsernameTaken = _userUseCases.IsUsernameTaken(NewUser.Username);
                if (isUsernameTaken)
                {
                    _toastService.ShowError("This username is already taken. Please choose another one.");
                    return;
                }

                await Task.Run(() => _userUseCases.AddUser(NewUser));

                _toastService.ShowSuccess($"User '{NewUser.Username}' added successfully!");

                ResetForm();
            }
            catch (Exception)
            {
                _toastService.ShowError("An error occurred while adding the user.");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private void ResetForm()
        {
            NewUser = new UserDTO
            {
                Role = Roles.FirstOrDefault()
            };
            EditContext = new EditContext(NewUser);
        }

        //Update existing user
        public void ClearSearch()
        {
            SearchQuery = string.Empty;
        }

        public void ToggleEditUser(User user)
        {
            if (EditingUser?.WorkingNumber == user.WorkingNumber)
            {
                // If you want to toggle edit mode, uncomment the line below
                // EditingUser = null;
                return;
            }

            EditingUser = new User
            {
                Username = user.Username,
                Password = string.Empty, 
                WorkingNumber = user.WorkingNumber,
                Role = user.Role
            };
        }

        public void DeleteUser(User user)
        {
            try
            {
                _userUseCases.DeleteUser(user);
                _toastService.ShowInfo($"User '{user.Username}' was deleted successfully.");
            }
            catch (Exception)
            {
                _toastService.ShowError("An error occurred while deleting the user.");
            }
        }

        public void SaveUser()
        {
            if (EditingUser == null)
                return;

            var existingUsers = _userUseCases.GetAllUsers()
                .Where(u => u.WorkingNumber != EditingUser.WorkingNumber)
                .ToList();

            if (existingUsers.Any(u => u.Username.Equals(EditingUser.Username, StringComparison.OrdinalIgnoreCase)))
            {
                _toastService.ShowError("This username is already taken. Please choose another one.");
                return;
            }

            try
            {
                _userUseCases.UpdateUser(EditingUser);
                _toastService.ShowSuccess("User details updated successfully.");
                EditingUser = null; 
            }
            catch (Exception)
            {
                _toastService.ShowError("An error occurred while updating the user.");
            }
        }

        public void CancelEdit()
        {
            EditingUser = null;
        }

        //Both edit and create util
        public string HumanizeRole(Role role)
        {
            if (role == Role.InventoryManager)
                return "Inventory Manager";
            else if (role == Role.WarehouseWorker)
                return "Warehouse Worker";
            else
                return role.ToString();
        }

        public string ValidationClass(object model, string fieldName)
        {
            if (model == null) return string.Empty;

            var fieldIdentifier = new FieldIdentifier(model, fieldName);
            var isValid = !EditContext.GetValidationMessages(fieldIdentifier).Any();
            return isValid ? "" : "is-invalid";
        }

    }
}
