using BlazorServerApp.Application.UseCases;
using Orders;
using OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;

namespace BlazorServerApp.Managers
{
    public class PickupManager
    {
        private readonly OrderUseCases _orderUseCases;
        private readonly OrderItemUseCases _orderItemUseCases;

        public bool IsLoading { get; private set; } = false;
        public string ActiveView { get; set; } = "Unassigned";
        public Order? SelectedOrder { get; private set; }
        public bool ViewingUnassignedDetails { get; private set; }

        public List<Order> UnassignedOrders { get; private set; } = new();
        public List<Order> AssignedOrders { get; private set; } = new();
        public List<Order> CompletedOrders { get; private set; } = new();
        private readonly IToastService _toastService;


        public PickupManager(OrderUseCases orderUseCases, OrderItemUseCases orderItemUseCases, IToastService toastService)
        {
            _orderUseCases = orderUseCases;
            _orderItemUseCases = orderItemUseCases;
            _toastService = toastService;
        }

        public async Task LoadOrdersAsync()
        {
            IsLoading = true;
            NotifyStateChanged();

            try
            {
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

        public async Task AssignOrderAsync(Order order)
        {
            try
            {
                var request = new UpdateOrderStatusRequest
                {
                    OrderId = order.OrderId,
                    NewStatus = OrderStatus.InProgress
                };

                await _orderUseCases.UpdateOrderStatusAsync(request);

                UnassignedOrders.Remove(order);
                AssignedOrders.Add(order);
                order.OrderStatus = OrderStatus.InProgress;

                NotifyStateChanged();

                // Show success toast notification
                _toastService.ShowSuccess($"Order #{order.OrderId} successfully assigned and marked as 'In Progress'.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error assigning order: {ex.Message}");

                // Show error toast notification
                _toastService.ShowError($"Failed to assign Order #{order.OrderId}. Please try again.");
            }
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

        public async Task CompleteOrderAsync()
        {
            if (SelectedOrder != null && CanCompleteOrder())
            {
                try
                {
                    var request = new UpdateOrderStatusRequest
                    {
                        OrderId = SelectedOrder.OrderId,
                        NewStatus = OrderStatus.Completed
                    };

                    await _orderUseCases.UpdateOrderStatusAsync(request);

                    SelectedOrder.OrderStatus = OrderStatus.Completed;
                    SelectedOrder.CompletedAt = Google.Protobuf.WellKnownTypes.Timestamp.FromDateTime(DateTime.UtcNow);
                    CompletedOrders.Add(SelectedOrder);
                    AssignedOrders.Remove(SelectedOrder);
                    SelectedOrder = null;

                    _toastService.ShowSuccess("Order completed successfully!");


                    NotifyStateChanged();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error completing order: {ex.Message}");
                    _toastService.ShowError("Failed to complete the order.");

                }
            }
        }

        public async Task PickupItemAsync(GetOrderItem item)
        {
            if (item.QuantityToPick > 0)
            {
                try
                {
                    var request = new UpdateOrderItemRequest
                    {
                        OrderItemId = item.OrderItemId, // Correct field for the ID
                        QuantityToPick = item.QuantityToPick - 1
                    };

                    await _orderItemUseCases.UpdateOrderItemsAsync(request);

                    item.QuantityToPick--;
                    _toastService.ShowSuccess($"Picked up 1 unit of {item.ItemName}.");

                    NotifyStateChanged();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error picking up item: {ex.Message}");
                    _toastService.ShowError($"Failed to pick up {item.ItemName}.");

                }
            }
        }

        public async Task RevertPickupItemAsync(GetOrderItem item)
        {
            if (item.QuantityToPick < item.TotalQuantity)
            {
                try
                {
                    var request = new UpdateOrderItemRequest
                    {
                        OrderItemId = item.OrderItemId,
                        QuantityToPick = item.QuantityToPick + 1
                    };

                    await _orderItemUseCases.UpdateOrderItemsAsync(request);

                    item.QuantityToPick++;
                    _toastService.ShowWarning($"Reverted pickup of 1 unit of {item.ItemName}.");

                    NotifyStateChanged();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reverting pickup item: {ex.Message}");
                    _toastService.ShowError($"Failed to revert pickup for {item.ItemName}.");
                }
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
