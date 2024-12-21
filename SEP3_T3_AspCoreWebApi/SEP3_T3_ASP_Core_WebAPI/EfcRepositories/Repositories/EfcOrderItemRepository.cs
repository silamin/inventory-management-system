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
}