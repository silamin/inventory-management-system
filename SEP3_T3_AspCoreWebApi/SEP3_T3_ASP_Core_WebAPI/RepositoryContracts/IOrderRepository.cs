using Entities;

namespace SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

public interface IOrderRepository
{
    Task<Order> GetOrderById(int id);
    Task<Order> AddOrderAsync(Order order);
    Task<Order> UpdateOrderAsync(Order order);
    Task<Order> DeleteOrderAsync(int id);
    Task<List<Order>> GetAllOrders();
    Task<IQueryable<Order>> GetAllOrdersByType(string type);
}