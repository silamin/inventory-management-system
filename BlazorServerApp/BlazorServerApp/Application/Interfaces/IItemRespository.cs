using Items;
using Orders;

namespace BlazorServerApp.Application.Interfaces
{
    public interface IItemRepository
    {
        Task<Item> CreateItemAsync(CreateItem itemDTO);
        Task EditItemAsync(Item item);
        Task DeleteItemAsync(DeleteItem itemId);
        Task<IEnumerable<Item>> GetAllItemsAsync();
    }
}
