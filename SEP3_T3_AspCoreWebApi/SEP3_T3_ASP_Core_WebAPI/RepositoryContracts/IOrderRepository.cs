using Entities;

namespace SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

public interface IOrderRepository
{
    //Task<List<Order>> GetAllOrders();
    Task<Order> AddOrderAsync(Order order);
    Task<List<Order>> GetOrdersByStatus(OrderStatus status);
}