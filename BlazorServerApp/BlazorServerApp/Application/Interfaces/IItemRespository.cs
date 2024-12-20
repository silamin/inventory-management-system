using Items;

namespace BlazorServerApp.Application.Interfaces
{
    public interface IItemRepository
    {
        Task<Item> CreateItemAsync(CreateItem itemDTO);
        Task EditItemAsync(Item item);
        Task DeleteItemAsync(DeleteItem itemId);
        IEnumerable<Item> GetAllItems();
    }
}
