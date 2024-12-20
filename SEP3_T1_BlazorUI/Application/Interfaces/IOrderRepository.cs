using SEP3_T1_BlazorUI.Models;

namespace SEP3_T1_BlazorUI.Application.Interfaces
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAllOrders();
        void AddOrder(Order order);

    }
}
