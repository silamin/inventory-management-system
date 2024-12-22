using Entities;

namespace SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

public interface IOrderItemRepository
{
    Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem);
    Task<OrderItem?> GetOrderItemByIdAsync(int id); // Fetch an order item by ID

}