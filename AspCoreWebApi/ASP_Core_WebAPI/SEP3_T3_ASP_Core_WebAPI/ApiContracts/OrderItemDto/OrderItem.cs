namespace ASP_Core_WebAPI.ApiContracts.OrderItemDto;

public class OrderItem
{
    public int OrderId { get; set; }
    public int ItemId { get; set; }
    public int QuantityToPick { get; set; }
}