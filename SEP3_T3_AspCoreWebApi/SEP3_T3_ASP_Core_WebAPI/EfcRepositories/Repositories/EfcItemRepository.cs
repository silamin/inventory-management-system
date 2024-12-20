using Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SEP3_T3_ASP_Core_WebAPI;
using SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcItemRepository: IItemRepository
{
    private readonly AppDbContext _ctx;
    public EfcItemRepository(AppDbContext ctx)
    {
        this._ctx = ctx;
    }
    
    // Get an item by its id
    public async Task<Item> GetItemById(int id)
    {
        return await _ctx.Items.FindAsync(id) ?? throw new InvalidOperationException();
    }

    // Add an item to the database
    public async Task<Item> AddItemAsync(Item item)
    {
        EntityEntry<Item> entityEntry = await _ctx.Items.AddAsync(item);
        await _ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    // Update an item in the database
    public async Task<Item> UpdateItemAsync(Item item)
    {
        if (!_ctx.Items.Any(i => i.ItemId == item.ItemId))
        {
            throw new InvalidOperationException("Item does not exist");
        }
        _ctx.Items.Update(item);
        await _ctx.SaveChangesAsync();
        return item;
    }

    // Delete an item from the database
    // Delete an item from the database
    public async Task<Item> DeleteItemAsync(int id)
    {
        // Find the item by Id
        var item = await _ctx.Items.FindAsync(id);

        if (item == null)
        {
            throw new KeyNotFoundException($"Item with ID {id} not found.");
        }

        _ctx.Items.Remove(item); // Remove the item from the context
        await _ctx.SaveChangesAsync(); // Save the changes to the database

        return item; // Return the deleted item
    }


    // Get all items from the database
    public IQueryable<Item> GetAllItems()
    {
        return _ctx.Items.Where(item => item.IsAvailable).AsQueryable();
    }

    // Get all items of a specific type from the database
    public IQueryable<Item> GetAllItemsByType(string type)
    {
        return null;
    }
}