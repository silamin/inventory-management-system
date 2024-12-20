using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;
using SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

namespace SEP3_T3_ASP_Core_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddOrder([FromBody] CreateOrderDTO createOrderDTO)
        {
            try
            {
                // Log the incoming request
                Console.WriteLine($"Received CreateOrderDTO: {System.Text.Json.JsonSerializer.Serialize(createOrderDTO)}");

                // Validate OrderItems
                if (createOrderDTO.OrderItems == null || !createOrderDTO.OrderItems.Any())
                {
                    return BadRequest("Order must have at least one item.");
                }

                // Convert DeliveryDate and CreatedAt to UTC
                createOrderDTO.DeliveryDate = createOrderDTO.DeliveryDate.ToUniversalTime();

                // Create the order from the DTO
                var order = new Order
                {
                    DeliveryDate = createOrderDTO.DeliveryDate, // Ensure this is in UTC
                    CreatedById = createOrderDTO.CreatedBy,
                    CreatedAt = DateTime.UtcNow, // Set CreatedAt to current UTC time
                    OrderItems = createOrderDTO.OrderItems.Select(itemDto => new OrderItem
                    {
                        ItemId = itemDto.ItemId,
                        TotalQuantity = itemDto.TotalQuantity
                    }).ToList()
                };

                // Log the order to be saved
                Console.WriteLine($"Mapped Order: {System.Text.Json.JsonSerializer.Serialize(order)}");

                // Call the repository to save the order
                var createdOrder = await orderRepository.AddOrderAsync(order);

                // Return success
                return Ok(true);
            }
            catch (Exception ex)
            {
                // Log the error for debugging
                Console.WriteLine($"Error in AddOrder: {ex.Message}");
                return StatusCode(500, "An error occurred while creating the order");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOrder([FromRoute] int id, [FromBody] Order order)
        {
            try
            {
                var orderToUpdate = await orderRepository.GetOrderById(id);
                if (orderToUpdate == null)
                {
                    return NotFound($"Order with ID {id} not found.");
                }

                orderToUpdate.OrderStatus = order.OrderStatus;
                orderToUpdate.DeliveryDate = order.DeliveryDate;
                orderToUpdate.OrderItems = order.OrderItems;

                await orderRepository.UpdateOrderAsync(orderToUpdate);
                return NoContent();
            }
            catch (InvalidOperationException)
            {
                return NotFound($"Order with ID {id} not found.");
            }
        }


        [HttpGet]
        public async Task<ActionResult<List<GetOrderDTO>>> GetAllOrders()
        {
            var orders = await orderRepository.GetAllOrders();
            var orderDtos = orders.Select(order => new GetOrderDTO
            {
                OrderId = order.OrderId,
                OrderStatus = order.OrderStatus.ToString(),
                DeliveryDate = order.DeliveryDate,
                CreatedAt = order.CreatedAt,
                OrderItems = order.OrderItems.Select(oi => new GetOrderItemDTO
                {
                    itemName = oi.Item.ItemName, // Include only itemName
                    QuantityToPick = oi.TotalQuantity,
                    TotalQuantity = oi.TotalQuantity,
                }).ToList(),
                AssignedUser = order.AssignedUser?.UserName,
                CreatedBy = order.CreatedBy.UserName
            }).ToList();

            return Ok(orderDtos);
        }

    }
}
