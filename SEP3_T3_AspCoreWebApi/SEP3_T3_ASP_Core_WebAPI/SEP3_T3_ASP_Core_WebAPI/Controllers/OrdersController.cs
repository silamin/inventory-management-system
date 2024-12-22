using Entities;
using Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;
using System.Security.Claims;
using static Entities.Roles;

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
        [Authorize(Roles = INVENTORY_MANAGER)]
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
                Console.WriteLine($"Received request to fetch orders with status: {status}");

                // Parse the provided status
                if (!Enum.TryParse<OrderStatus>(status, true, out var orderStatus))
                {
                    Console.WriteLine($"Invalid order status provided: {status}");
                    return BadRequest($"Invalid order status: {status}");
                }
                Console.WriteLine($"Parsed order status: {orderStatus}");

                // Debugging: Log all claims in the token
                Console.WriteLine("Token Claims:");
                foreach (var claim in User.Claims)
                {
                    Console.WriteLine($"Type: {claim.Type}, Value: {claim.Value}");
                }

                // Get the user's role and ID from the token
                var userRole = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role ||
                                                               c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub")?.Value;

                Console.WriteLine($"Extracted user role: {userRole}, user ID: {userId}");

                if (userRole == null || userId == null)
                {
                    Console.WriteLine("Invalid token: missing role or user ID.");
                    return Unauthorized("Invalid token: missing role or user ID.");
                }

                // Fetch orders by status from the repository
                Console.WriteLine($"Fetching orders from repository for status: {orderStatus}");
                var orders = await orderRepository.GetOrdersByStatus(orderStatus);

                Console.WriteLine($"Fetched {orders.Count} orders from repository.");

                // Apply filtering based on role and status
                if (userRole == UserRole.WAREHOUSE_WORKER.ToString())
                {
                    if (orderStatus == OrderStatus.IN_PROGRESS || orderStatus == OrderStatus.COMPLETED)
                    {
                        Console.WriteLine($"Filtering orders for {userRole} with status: {orderStatus}");
                        orders = orders.Where(o => o.AssignedUser?.UserId.ToString() == userId).ToList();
                        Console.WriteLine($"Filtered orders count: {orders.Count}");
                    }
                }

                // Map orders to DTOs
                Console.WriteLine("Mapping orders to DTOs.");
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

                Console.WriteLine($"Successfully mapped {orderDtos.Count} orders to DTOs.");
                return Ok(orderDtos);
            }
            catch (Exception ex)
            {
                // Log and return error response
                Console.WriteLine($"Error in GetOrdersByStatus: {ex.Message}");
                return StatusCode(500, "An error occurred while fetching orders by status");
            }
        }

        [HttpPut("{orderId}/status")]
        [Authorize(Roles = Roles.WAREHOUSE_WORKER)] // Ensure only warehouse workers can access
        public async Task<ActionResult> UpdateOrderStatus(int orderId, [FromBody] OrderStatus NewStatus)
        {
            try
            {
                // Get the user's ID from the token
                var userId = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "sub")?.Value;
                if (userId == null)
                {
                    Console.WriteLine("Invalid token: missing user ID.");
                    return Unauthorized("Invalid token: missing user ID.");
                }

                // Fetch the order from the repository
                var order = await orderRepository.GetOrderByIdAsync(orderId);
                if (order == null)
                {
                    return NotFound($"Order with ID {orderId} not found.");
                }

                // Ensure the order is assigned to the current user if it's in progress or completed
                if (order.AssignedUser?.UserId.ToString() != userId)
                {
                    return Forbid("You are not authorized to update the status of this order.");
                }


                // Validate allowed status transitions
                if (order.OrderStatus == OrderStatus.COMPLETED)
                {
                    return BadRequest("Completed orders cannot be updated.");
                }

                order.OrderStatus = NewStatus;
                if (NewStatus == OrderStatus.COMPLETED)
                {
                    order.CompletedAt = DateTimeOffset.UtcNow;
                }

                await orderRepository.UpdateOrderAsync(order);

                return NoContent();
            }
            catch (Exception ex)
            {
                // Log the error and return a server error response
                Console.WriteLine($"Error in UpdateOrderStatus: {ex.Message}");
                return StatusCode(500, "An error occurred while updating the order status");
            }
        }



    }
}
