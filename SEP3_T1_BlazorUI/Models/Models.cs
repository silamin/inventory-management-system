using System.ComponentModel.DataAnnotations;

namespace SEP3_T1_BlazorUI.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Item name is required and cannot be an empty string.")]

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int QuantityInStore { get; set; }
        public int OrderQuantity { get; set; }
        public bool IsSelected { get; set; }
    }


    public class Order
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalQuantity { get; set; }
        public List<OrderItem> OrderItems { get; set; } = new();
        public string Status { get; set; } = "Pending";
        public string? EmployeeId { get; set; }
    }

    public class OrderItem
    {
        public int ItemId { get; set; }
        public string Name { get; set; } = "";
        public int QuantityOrdered { get; set; }
    }

    public class User
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required and cannot be an empty string.")]
        [RegularExpression(@"^\S+$", ErrorMessage = "Username cannot be empty or contain whitespace.")]
        public string Username { get; set; } = "";
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required and cannot be an empty string.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = "";
        public int WorkingNumber { get; set; }
        public Role Role { get; set; }
    }

    public class UserDTO : IValidatableObject
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required and cannot be an empty string.")]
        [RegularExpression(@"^\S+$", ErrorMessage = "Username cannot be empty or contain whitespace.")]
        public string Username { get; set; } = "";

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required and cannot be an empty string.")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long.")]
        public string Password { get; set; } = "";

        public Role? Role { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(Username))
            {
                yield return new ValidationResult(
                    "Username cannot be an empty string or whitespace.",
                    new[] { nameof(Username) }
                );
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                yield return new ValidationResult(
                    "Password cannot be an empty string or whitespace.",
                    new[] { nameof(Password) }
                );
            }
        }
    }


    public enum Role
    {
        InventoryManager,
        WarehouseWorker
    }
}
