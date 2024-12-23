using Entities;

namespace Entities
{
    public class Item
    {
        public int ItemId { get; set; }
        public required string ItemName { get; set; }
        public string ?Description { get; set; }
        public int QuantityInStore {  get; set; }
        
        // Navigation property for OrderItem
        public List<OrderItem> OrderItems { get; set; } // One-to-many relationship with OrderItem
        public bool IsAvailable { get; set; } = true;

        public static Item Create(string name, string? requestDescription, int requestQuantityInStore)
        {
            return new Item
            {
                ItemName = name,
                Description = requestDescription,
                QuantityInStore = requestQuantityInStore
            };
        }
    }
}
