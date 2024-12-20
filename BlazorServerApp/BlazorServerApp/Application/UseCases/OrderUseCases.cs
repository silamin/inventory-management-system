using BlazorServerApp.Application.Interfaces;
using Orders;

namespace BlazorServerApp.Application.UseCases
{
    public class OrderUseCases
    {
        private readonly IOrderRepository _orderRepository;

        public OrderUseCases(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        }

        public async Task<Order> AddOrderAsync(OrderRequest orderRequest)
        {
            if (orderRequest == null) throw new ArgumentNullException(nameof(orderRequest));

            try
            {
                var response = await _orderRepository.AddOrderAsync(orderRequest);
                return response;
            }
            catch (Exception ex)
            {
                // Handle exception appropriately (logging, rethrow, etc.)
                throw new ApplicationException("Error adding order", ex);
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                var orders = await _orderRepository.GetAllOrdersAsync();
                return orders;
            }
            catch (Exception ex)
            {
                // Handle exception appropriately (logging, rethrow, etc.)
                throw new ApplicationException("Error retrieving orders", ex);
            }
        }
    }
}
