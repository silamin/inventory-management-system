using BlazorServerApp.Application.UseCases;
using Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp.Managers
{
    public class PickupManager
    {
        private readonly OrderUseCases _orderUseCases;

        public bool IsLoading { get; private set; } = false;
        public string ActiveView { get; set; } = "Unassigned";
        public Order? SelectedOrder { get; private set; }
        public bool ViewingUnassignedDetails { get; private set; }

        public List<Order> UnassignedOrders { get; private set; } = new();
        public List<Order> AssignedOrders { get; private set; } = new();
        public List<Order> CompletedOrders { get; private set; } = new();

        public PickupManager(OrderUseCases orderUseCases)
        {
            _orderUseCases = orderUseCases;
        }

        public async Task LoadOrdersAsync()
        {
            IsLoading = true;
            NotifyStateChanged();

            try
            {
                // Fetch orders by status
                var unassignedResponse = await _orderUseCases.GetOrdersByStatusAsync(OrderStatus.NotStarted);
                var assignedResponse = await _orderUseCases.GetOrdersByStatusAsync(OrderStatus.InProgress);
                var completedResponse = await _orderUseCases.GetOrdersByStatusAsync(OrderStatus.Completed);

                UnassignedOrders = unassignedResponse.ToList();
                AssignedOrders = assignedResponse.ToList();
                CompletedOrders = completedResponse.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading orders: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
                NotifyStateChanged();
            }
        }

        public void AssignOrder(Order order)
        {
            UnassignedOrders.Remove(order);
            AssignedOrders.Add(order);
            order.OrderStatus = OrderStatus.InProgress;
            NotifyStateChanged();
        }

        public void ToggleOrderDetails(int orderId, bool isUnassigned)
        {
            if (SelectedOrder?.OrderId == orderId)
            {
                SelectedOrder = null;
            }
            else
            {
                SelectedOrder = (isUnassigned ? UnassignedOrders : AssignedOrders).FirstOrDefault(o => o.OrderId == orderId);
                ViewingUnassignedDetails = isUnassigned;
            }

            NotifyStateChanged();
        }

        public void CompleteOrder()
        {
            if (SelectedOrder != null && CanCompleteOrder())
            {
                SelectedOrder.OrderStatus = OrderStatus.Completed; // Match your proto enum
                SelectedOrder.CompletedAt = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
                CompletedOrders.Add(SelectedOrder);
                AssignedOrders.Remove(SelectedOrder);
                SelectedOrder = null;

                NotifyStateChanged();
            }
        }

        public void PickupItem(GetOrderItem item)
        {
            if (item.QuantityToPick > 0)
            {
                item.QuantityToPick--;
                NotifyStateChanged();
            }
        }

        public void RevertPickupItem(GetOrderItem item)
        {
            if (item.QuantityToPick < item.TotalQuantity)
            {
                item.QuantityToPick++;
                NotifyStateChanged();
            }
        }

        public bool CanCompleteOrder()
        {
            return SelectedOrder?.OrderItems.All(i => i.QuantityToPick == 0) ?? false;
        }

        private Action? _stateChangedCallback;

        public void RegisterStateChangeCallback(Action callback)
        {
            _stateChangedCallback = callback;
        }

        private void NotifyStateChanged()
        {
            _stateChangedCallback?.Invoke();
        }

        public string FormatTimestamp(Google.Protobuf.WellKnownTypes.Timestamp timestamp)
        {
            return timestamp?.ToDateTime().ToString("yyyy-MM-dd") ?? string.Empty;
        }
    }
}
