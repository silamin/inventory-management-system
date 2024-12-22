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

        public async Task<bool> AddOrderAsync(CreateOrder orderRequest)
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

        internal async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
        {
            try
            {
                var orders = await _orderRepository.GetOrdersAsync(status);
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
