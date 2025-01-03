﻿using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASP_Core_WebAPI.RepositoryContracts;
using static Entities.Roles;

namespace ASP_Core_WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(Roles = WAREHOUSE_WORKER)]
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
    public async Task<ActionResult> UpdateOrderItem([FromRoute] int id, [FromBody] int QuantityToPick)
    {
        try
        {
            // Fetch the order item
            var orderItem = await orderItemRepository.GetOrderItemByIdAsync(id);
            if (orderItem == null)
            {
                return NotFound($"OrderItem with ID {id} not found.");
            }

            // Update the pick quantity
            if (QuantityToPick > orderItem.TotalQuantity || QuantityToPick < 0)
            {
                return BadRequest("QuantityToPick cannot be greater than TotalQuantity or less than 0.");
            }

            orderItem.QuantityToPick = QuantityToPick;

            // Save the updated order item
            await orderItemRepository.UpdateOrderItemAsync(orderItem);

            return NoContent();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, $"An error occurred: {e.Message}");
        }
    }



}