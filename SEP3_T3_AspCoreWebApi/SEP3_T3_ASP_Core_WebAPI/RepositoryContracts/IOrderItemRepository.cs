using Entities;

namespace SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

public interface IOrderItemRepository
{
    Task<OrderItem> GetOrderItemById(int id);
    Task<OrderItem> AddOrderItemAsync(OrderItem orderItem);
    Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem);
    Task<OrderItem> DeleteOrderItemAsync(int id);
    Task<IQueryable<OrderItem>> GetAllOrderItemsByOrderId(int OrderId);
    IQueryable<OrderItem> GetAllOrderItems(); 
}