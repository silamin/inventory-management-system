using Entities;

namespace ASP_Core_WebAPI.RepositoryContracts;

public interface IItemRepository
{
    Task<Item> GetItemById(int id);
    Task<Item> AddItemAsync(Item item);
    Task<Item> UpdateItemAsync(Item item);
    Task<Item> DeleteItemAsync(int id);
    IQueryable<Item> GetAllItems();
}