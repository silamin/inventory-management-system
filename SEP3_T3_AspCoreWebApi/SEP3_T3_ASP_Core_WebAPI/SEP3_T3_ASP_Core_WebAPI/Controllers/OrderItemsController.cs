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

    // ********** UPDATE Endpoints **********
    // PUT: /OrderItems/{id}
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrderItem([FromRoute] int id, [FromBody] OrderItem orderItem)
    {
        try
        {            
            await orderItemRepository.UpdateOrderItemAsync(orderItem);
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


    
}