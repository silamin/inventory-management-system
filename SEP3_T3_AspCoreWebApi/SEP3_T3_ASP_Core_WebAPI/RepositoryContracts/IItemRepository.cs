using Entities;

namespace SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

public interface IItemRepository
{
    Task<Item> GetItemById(int id);
    Task<Item> AddItemAsync(Item item);
    Task<Item> UpdateItemAsync(Item item);
    Task<Item> DeleteItemAsync(int id);
    IQueryable<Item> GetAllItems();
    IQueryable<Item> GetAllItemsByType(string type);
}