using Entities;
using Microsoft.EntityFrameworkCore;
using ASP_Core_WebAPI;
using ASP_Core_WebAPI.RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcOrderItemRepository: IOrderItemRepository
{
    private readonly AppDbContext _ctx;
    public EfcOrderItemRepository(AppDbContext ctx)
    {
        this._ctx = ctx;
    }

    public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem)
    {
        _ctx.OrderItems.Update(orderItem);
        await _ctx.SaveChangesAsync();
        return orderItem;
    }
    //DO not allo pick more then total
    public async Task<OrderItem?> GetOrderItemByIdAsync(int id)
    {
        return await _ctx.OrderItems
            .Include(oi => oi.Item) // Include related item data
            .Include(oi => oi.Order) // Include related order data
            .FirstOrDefaultAsync(oi => oi.OrderItemId == id);
    }
}