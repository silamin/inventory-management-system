using Entities;
using SEP3_T3_ASP_Core_WebAPI;
using SEP3_T3_ASP_Core_WebAPI.RepositoryContracts;

namespace EfcRepositories.Repositories;

public class EfcOrderItemRepository: IOrderItemRepository
{
    private readonly AppDbContext _ctx;
    public EfcOrderItemRepository(AppDbContext ctx)
    {
        this._ctx = ctx;
    }

    public async Task<OrderItem> GetOrderItemById(int id)
    {
        return await _ctx.OrderItems.FindAsync(id) ?? throw new InvalidOperationException();
    }

    public async Task<OrderItem> AddOrderItemAsync(OrderItem orderItem)
    {
        var entityEntry = await _ctx.OrderItems.AddAsync(orderItem);
        await _ctx.SaveChangesAsync();
        return entityEntry.Entity;
    }

    public async Task<OrderItem> UpdateOrderItemAsync(OrderItem orderItem)
    {
        if (!_ctx.OrderItems.Any(o => o.OrderItemId == orderItem.OrderItemId))
        {
            throw new InvalidOperationException("OrderItem does not exist");
        }
        _ctx.OrderItems.Update(orderItem);
        await _ctx.SaveChangesAsync();
        return orderItem;
    }

    public async Task<OrderItem> DeleteOrderItemAsync(int id)
    {
        return await _ctx.OrderItems.FindAsync(id) ?? throw new InvalidOperationException();
    }

    public Task<IQueryable<OrderItem>> GetAllOrderItemsByOrderId(int orderId)
    {
        return Task.FromResult(_ctx.OrderItems.Where(o => o.OrderId == orderId));
    }

    public IQueryable<OrderItem> GetAllOrderItems()
    {
        return _ctx.OrderItems.AsQueryable();
    }
}