using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SEP3_T3_ASP_Core_WebAPI;
using SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcOrderRepository : IOrderRepository
{
    private readonly AppDbContext ctx;

    public EfcOrderRepository(AppDbContext ctx)
    {
        this.ctx = ctx;
    }

    public async Task<Order> AddOrderAsync(Order order)
    {
        if (order.CreatedById <= 0)
        {
            throw new Exception("The 'CreatedBy' field is required and must contain a valid UserId.");
        }

        var createdByUser = await ctx.Users.FirstOrDefaultAsync(u => u.UserId == order.CreatedById);
        if (createdByUser == null)
        {
            throw new Exception($"User with UserId '{order.CreatedById}' not found.");
        }

        // Ensure timestamps are in UTC
        order.CreatedAt = DateTimeOffset.UtcNow;
        order.DeliveryDate = order.DeliveryDate.ToUniversalTime();
        order.CreatedBy = createdByUser;

        // Save Order first
        await ctx.Orders.AddAsync(order);
        await ctx.SaveChangesAsync(); // Now OrderId is available!
        Console.WriteLine($"Order created successfully with OrderId: {order.OrderId}");

        // Attach the OrderId to all OrderItems
        foreach (var orderItem in order.OrderItems)
        {
            orderItem.OrderId = order.OrderId; // Attach the new OrderId to each OrderItem
            orderItem.QuantityToPick = orderItem.TotalQuantity;
            ctx.Attach(orderItem); // Attach instead of Add
        }

        await ctx.SaveChangesAsync(); // Save all changes at once
        return order;
    }

    /*
  *public async Task<List<Order>> GetAllOrders()
    {
        return await ctx.Orders
            .Include(order => order.OrderItems)
            .ThenInclude(oi => oi.Item )
            .Include(order => order.AssignedUser)
            .Include(order => order.CreatedBy)
            .ToListAsync();
    }
  * 
  */

    public async Task<List<Order>> GetOrdersByStatus(OrderStatus status)
    {
        return await ctx.Orders
            .Where(order => order.OrderStatus == status)
            .Include(order => order.OrderItems)
            .ThenInclude(oi => oi.Item)
            .Include(order => order.AssignedUser)
            .Include(order => order.CreatedBy)
            .ToListAsync();
    }
}
