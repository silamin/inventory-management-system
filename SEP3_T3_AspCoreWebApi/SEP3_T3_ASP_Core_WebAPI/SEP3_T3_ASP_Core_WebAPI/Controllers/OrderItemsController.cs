using Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

namespace SEP3_T3_ASP_Core_WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderItemsController: ControllerBase
{
    private readonly IOrderItemRepository orderItemRepository;
    
    public OrderItemsController(IOrderItemRepository orderItemRepository)
    {
        this.orderItemRepository = orderItemRepository;
    }

    // ********** CREATE Endpoints **********
    // POST: /OrderItems
    [HttpPost]
    public async Task<ActionResult<OrderItem>> AddOrderItem([FromBody] OrderItem orderItem)
    {
        OrderItem created = await orderItemRepository.AddOrderItemAsync(orderItem);
        return Created($"/OrderItems/{created.OrderItemId}", created);
    }

    // ********** UPDATE Endpoints **********
    // PUT: /OrderItems/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrderItem([FromRoute] int id, [FromBody] OrderItem orderItem)
    {
        try
        {
            OrderItem orderItemToUpdate = await orderItemRepository.GetOrderItemById(id);
            orderItemToUpdate.OrderId = orderItem.OrderId;
            orderItemToUpdate.ItemId = orderItem.ItemId;
            orderItemToUpdate.TotalQuantity = orderItem.TotalQuantity;
            
            await orderItemRepository.UpdateOrderItemAsync(orderItemToUpdate);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound($"OrderItem with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

    // ********** Delete Endpoints **********
    // DELETE: /OrderItems/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrderItem([FromRoute] int id)
    {
        try
        {
            await orderItemRepository.DeleteOrderItemAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException)
        {
            return NotFound($"OrderItem with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

    // ********** GET Endpoints **********
    // GET: /OrderItems/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<OrderItem>> GetOrderItemById([FromRoute] int id)
    {
        try
        {
            OrderItem orderItem = await orderItemRepository.GetOrderItemById(id);
            return Ok(orderItem);
        }
        catch (InvalidOperationException)
        {
            return NotFound($"OrderItem with ID {id} not found.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }

    // GET: /OrderItems
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetAllOrderItems()
    {
        List<OrderItem> orderItems = await orderItemRepository.GetAllOrderItems().ToListAsync();
        return Ok(orderItems);
    }

    // GET: /OrderItems/Order/{orderId}
    [HttpGet("Order/{orderId}")]
    public async Task<ActionResult<IEnumerable<OrderItem>>> GetOrderItemsByOrderId([FromRoute] int orderId)
    {
        IQueryable<OrderItem> orderItems = await orderItemRepository.GetAllOrderItemsByOrderId(orderId);
        return Ok(orderItems);
    }
    
}