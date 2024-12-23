using ASP_Core_WebAPI.ApiContracts.OrderItemDto;

namespace ASP_Core_WebAPI.ApiContracts.OrderDto;

public class OrderDto
{
    public int UserId { get; set; }
    public required string OrderStatus { get; set; }
    public List<OrderItem>? OrderList { get; set; }
    public DateTime DeliveryDate { get; set; }
}