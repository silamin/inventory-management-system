namespace Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public OrderStatus OrderStatus { get; set; } // Enum for order status
        public DateTime DeliveryDate { get; set; }

        // Navigation property to OrderItem for many-to-many relationship
        public ICollection<OrderItem> OrderItems { get; set; } // OrderItems in the order

        // Navigation property to User for one-to-many relationship
        public User? AssignedUser { get; set; } // User assigned to the order
        public int? UserId { get; set; } // Foreign key to AssignedUser (made nullable)

        // Navigation property to User for created by relationship
        public User CreatedBy { get; set; } // Navigation property to the user who created the order
        public int CreatedById { get; set; } // Foreign key to the User who created the order

        public DateTimeOffset CreatedAt { get; set; } // Timestamp for order creation
        public DateTimeOffset? CompletedAt { get; set; } // Nullable timestamp for when the order is marked as completed

    }
    public enum OrderStatus
    {
        NOT_STARTED,
        IN_PROGRESS,
        COMPLETED
    }
}
