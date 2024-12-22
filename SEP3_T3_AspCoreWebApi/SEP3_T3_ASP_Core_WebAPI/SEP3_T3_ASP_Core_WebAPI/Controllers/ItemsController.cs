using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP3_T3_ASP_Core_WebAPI.ApiContracts.ItemDto;
using SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;


namespace SEP3_T3_ASP_Core_WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = "INVENTORY_MANAGER")]
public class ItemsController: ControllerBase
{
    private readonly IItemRepository itemRepository;
    
    public ItemsController(IItemRepository itemRepository)
    {
        this.itemRepository = itemRepository;
    }

    // ********** GET Endpoints **********
    // GET: /Items
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Item>>> GetAllItems()
    {
        List<Item> dtos = await itemRepository.GetAllItems()
            .Select(item => new Item
            {
                ItemId = item.ItemId,
                ItemName = item.ItemName,
                Description = item.Description,
                QuantityInStore = item.QuantityInStore
            })
            .ToListAsync();
        return Ok(dtos);
    }

    // GET: /Items/{id}
    

    // ********** CREATE Endpoints **********
    // POST: /Items
    [HttpPost]
    public async Task<ActionResult<Item>> AddItem([FromBody] ItemDto request)
    {
        Item item = Entities.Item.Create(request.ItemName, request.Description, request.QuantityInStore);
        Item created = await itemRepository.AddItemAsync(item);
        Item dto = new()
        {
            ItemId = created.ItemId,
            ItemName = created.ItemName,
            Description = created.Description,
            QuantityInStore = created.QuantityInStore
        };
        return Created($"/Items/{dto.ItemId}", created);
    }

    // ********** UPDATE Endpoints **********
    // PUT: /Items/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateItem([FromRoute] int id, [FromBody] ItemDto request)
    {
        try
        {
            Item itemToUpdate = await itemRepository.GetItemById(id);
            itemToUpdate.ItemName = request.ItemName;
            itemToUpdate.Description = request.Description;
            itemToUpdate.QuantityInStore = request.QuantityInStore;
            
            await itemRepository.UpdateItemAsync(itemToUpdate);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Item with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }
    

    // DELETE: /Items/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteItem([FromRoute] int id)
    {
        try
        {
            await itemRepository.DeleteItemAsync(id); // Call the updated repository method
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound($"Item with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

}