using Orders;

namespace BlazorServerApp.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<bool> AddOrderAsync(CreateOrder order);
        Task<IEnumerable<Order>> GetOrdersAsync(OrderStatus status);
    }


}
