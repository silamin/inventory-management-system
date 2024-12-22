using Items;
using OrderItems;

namespace BlazorServerApp.Application.Interfaces
{
    public interface IOrderItemRepository
    {
        Task UpdateOrderItemAsync(UpdateOrderItemRequest orderItemDTO);
    }
}
