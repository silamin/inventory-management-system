using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

namespace SEP3_T3_ASP_Core_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
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

        [HttpGet("status/{status}")]
        public async Task<ActionResult<List<GetOrderDTO>>> GetOrdersByStatus([FromRoute] string status)
        {
            try
            {
                // Parse the provided status
                if (!Enum.TryParse<OrderStatus>(status, true, out var orderStatus))
                {
                    return BadRequest($"Invalid order status: {status}");
                }

                // Fetch orders by status from the repository
                var orders = await orderRepository.GetOrdersByStatus(orderStatus);

                // Map orders to DTOs
                var orderDtos = orders.Select(order => new GetOrderDTO
                {
                    OrderId = order.OrderId,
                    OrderStatus = order.OrderStatus.ToString(),
                    DeliveryDate = order.DeliveryDate,
                    CreatedAt = order.CreatedAt,
                    OrderItems = order.OrderItems.Select(oi => new GetOrderItemDTO
                    {
                        itemName = oi.Item.ItemName,
                        QuantityToPick = oi.QuantityToPick,
                        TotalQuantity = oi.TotalQuantity,
                    }).ToList(),
                    AssignedUser = order.AssignedUser?.UserName,
                    CreatedBy = order.CreatedBy.UserName,
                    CompletedAt = order.CompletedAt
                }).ToList();

                return Ok(orderDtos);
            }
            catch (Exception ex)
            {
                // Log and return error response
                Console.WriteLine($"Error in GetOrdersByStatus: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching orders by status");
            }
        }
    }
}
