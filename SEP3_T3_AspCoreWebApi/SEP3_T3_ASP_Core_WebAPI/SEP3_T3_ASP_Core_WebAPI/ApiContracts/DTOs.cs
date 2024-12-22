using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Entities.DTOs
{
    public class GetOrderDTO
    {
        public int OrderId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime DeliveryDate { get; set; }
        public List<GetOrderItemDTO> OrderItems { get; set; }
        public string AssignedUser { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset? CompletedAt { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
    }

    public class CreateOrderDTO
    {
        public DateTime DeliveryDate { get; set; }
        public List<CreateOrderItemDTO> OrderItems { get; set; } // Corrected from CreateOrderDTO to CreateOrderItemDTO
        public int CreatedBy { get; set; }
    }
    public class UpdateOrderStatusDTO
    {
        public int orderId { get; set; }
        public string NewStatus { get; set; } = string.Empty;
    }
    public class UpdateOrderItemDTO
    {
        public int orderItemId { get; set; }
        public int QuantityToPick { get; set; } // Quantity of the item in the order
    }

    public class OrderItemDTO
    {
        public ItemDTO item { get; set; }
        public int QuantityToPick { get; set; }
    }
    public class GetOrderItemDTO
    {
        public string itemName { get; set; }
        public int QuantityToPick { get; set; }
        public int TotalQuantity { get; set; }
    }

    public class CreateOrderItemDTO
    {
        public int ItemId { get; set; }
        public int TotalQuantity { get; set; }
    }

    public class ItemDTO
    {
        public int ItemId { get; set; }
        public required string ItemName { get; set; }
        public string? Description { get; set; }
        public int QuantityInStore { get; set; }
    }
}
