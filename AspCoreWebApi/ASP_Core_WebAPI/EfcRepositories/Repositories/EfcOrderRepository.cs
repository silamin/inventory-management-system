using Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ASP_Core_WebAPI;
using ASP_Core_WebAPI.RepositoryContracts;

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

        // Update the items' QuantityInStore and attach OrderItems
        foreach (var orderItem in order.OrderItems)
        {
            var item = await ctx.Items.FirstOrDefaultAsync(i => i.ItemId == orderItem.ItemId);
            if (item == null)
            {
                throw new Exception($"Item with ItemId '{orderItem.ItemId}' not found.");
            }

            if (item.QuantityInStore < orderItem.TotalQuantity)
            {
                throw new Exception($"Item '{item.ItemName}' has insufficient stock. Available: {item.QuantityInStore}, Required: {orderItem.TotalQuantity}");
            }

            // Update QuantityInStore
            item.QuantityInStore -= orderItem.TotalQuantity;

            // Attach the OrderId to OrderItem
            orderItem.OrderId = order.OrderId;
            orderItem.QuantityToPick = orderItem.TotalQuantity;

            ctx.Attach(orderItem); // Attach instead of Add
        }

        // Save changes for OrderItems and Items
        await ctx.SaveChangesAsync();
        return order;
    }


    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await ctx.Orders
            .Include(order => order.OrderItems)
            .ThenInclude(oi => oi.Item)
            .Include(order => order.AssignedUser)
            .Include(order => order.CreatedBy)
            .FirstOrDefaultAsync(order => order.OrderId == orderId);
    }

    public async Task UpdateOrderAsync(Order order)
    {
        ctx.Orders.Update(order);
        await ctx.SaveChangesAsync();
    }

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
