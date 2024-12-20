using SEP3_T3_ASP_Core_WebAPI.ApiContracts.OrderItemDto;

namespace SEP3_T3_ASP_Core_WebAPI.ApiContracts.OrderDto;

public class OrderDto
{
    public int UserId { get; set; }
    public required string OrderStatus { get; set; }
    public List<OrderItem>? OrderList { get; set; }
    public DateTime DeliveryDate { get; set; }
}