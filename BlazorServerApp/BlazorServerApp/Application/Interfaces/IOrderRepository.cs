using Orders;

namespace BlazorServerApp.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> AddOrderAsync(OrderRequest order);
    }


}
