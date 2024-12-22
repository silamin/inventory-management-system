using BlazorServerApp.Application.Interfaces;
using BlazorServerApp.Infrastructure.Repositories;
using OrderItems;
using Orders;

namespace BlazorServerApp.Application.UseCases
{
    public class OrderItemUseCases
    {
        private readonly IOrderItemRepository _orderItemRepository;

        public OrderItemUseCases(IOrderItemRepository orderItemRepository)
        {
            _orderItemRepository = orderItemRepository ?? throw new ArgumentNullException(nameof(orderItemRepository));
        }
        public async Task UpdateOrderItemsAsync(UpdateOrderItemRequest orderItemDto)
        {
            if (orderItemDto == null) throw new ArgumentNullException(nameof(orderItemDto));
            try
            {
                await _orderItemRepository.UpdateOrderItemAsync(orderItemDto);
            }
            catch (Exception ex)
            {
                // Handle exception appropriately (logging, rethrow, etc.)
                throw new ApplicationException("Error editing item", ex);
            }
        }
    }
}
