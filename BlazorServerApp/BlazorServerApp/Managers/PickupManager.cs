using BlazorServerApp.Application.UseCases;
using Orders;
using OrderItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using BlazorServerApp.Helpers; // For SortColumn & extension

namespace BlazorServerApp.Managers
{
    public class PickupManager
    {
        private readonly OrderUseCases _orderUseCases;
        private readonly OrderItemUseCases _orderItemUseCases;
        private readonly IToastService _toastService;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly NavigationManager _navigationManager;

        // ------------------------------
        // BASIC PROPERTIES
        // ------------------------------
        public bool IsLoading { get; private set; } = false;
        public string ActiveView { get; private set; } = "Unassigned";
        public Order? SelectedOrder { get; private set; }
        public bool ViewingUnassignedDetails { get; private set; }

        public List<Order> UnassignedOrders { get; private set; } = new();
        public List<Order> AssignedOrders { get; private set; } = new();
        public List<Order> CompletedOrders { get; private set; } = new();

        // ------------------------------
        // PAGINATION
        // ------------------------------
        public int ItemsPerPage { get; private set; } = 5;

        private int _unassignedCurrentPage = 1;
        private int _assignedCurrentPage = 1;
        private int _completedCurrentPage = 1;

        public int UnassignedCurrentPage => _unassignedCurrentPage;
        public int AssignedCurrentPage => _assignedCurrentPage;
        public int CompletedCurrentPage => _completedCurrentPage;

        public int UnassignedTotalPages =>
            (int)Math.Ceiling(UnassignedOrders.Count / (double)ItemsPerPage);

        public int AssignedTotalPages =>
            (int)Math.Ceiling(AssignedOrders.Count / (double)ItemsPerPage);

        public int CompletedTotalPages =>
            (int)Math.Ceiling(CompletedOrders.Count / (double)ItemsPerPage);

        // ------------------------------
        // SORTING STATE
        // ------------------------------
        private SortColumn _unassignedSortColumn = SortColumn.OrderId;
        private bool _unassignedSortAscending = true;

        private SortColumn _assignedSortColumn = SortColumn.OrderId;
        private bool _assignedSortAscending = true;

        private SortColumn _completedSortColumn = SortColumn.OrderId;
        private bool _completedSortAscending = true;

        // ------------------------------
        // CONSTRUCTOR
        // ------------------------------
        public PickupManager(
            OrderUseCases orderUseCases,
            OrderItemUseCases orderItemUseCases,
            IToastService toastService,
            AuthenticationStateProvider authenticationStateProvider,
            NavigationManager navigationManager)
        {
            _orderUseCases = orderUseCases;
            _orderItemUseCases = orderItemUseCases;
            _toastService = toastService;
            _authenticationStateProvider = authenticationStateProvider;
            _navigationManager = navigationManager;
        }

        // ------------------------------
        // LOADING ORDERS + VIEW SWITCH
        // ------------------------------
        public async Task LoadOrdersAsync(string view)
        {
            IsLoading = true;
            NotifyStateChanged();
            try
            {
                if (view == "Unassigned")
                {
                    var unassignedResponse = await _orderUseCases.GetOrdersByStatusAsync(OrderStatus.NotStarted);
                    UnassignedOrders = unassignedResponse.ToList();
                }
                else if (view == "Assigned")
                {
                    var assignedResponse = await _orderUseCases.GetOrdersByStatusAsync(OrderStatus.InProgress);
                    AssignedOrders = assignedResponse.ToList();
                }
                else if (view == "Completed")
                {
                    var completedResponse = await _orderUseCases.GetOrdersByStatusAsync(OrderStatus.Completed);
                    CompletedOrders = completedResponse.ToList();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading orders: {ex.Message}");
                _toastService.ShowError("Failed to load orders. Please try again.");
            }
            finally
            {
                IsLoading = false;
                NotifyStateChanged();
            }
        }

        public async Task ChangeViewAsync(string newView)
        {
            if (ActiveView != newView)
            {
                ActiveView = newView;

                // Optionally reset page index if switching views
                if (newView == "Unassigned") _unassignedCurrentPage = 1;
                if (newView == "Assigned") _assignedCurrentPage = 1;
                if (newView == "Completed") _completedCurrentPage = 1;

                await LoadOrdersAsync(newView);
            }
        }

        // ------------------------------
        // SORTING METHODS
        // ------------------------------
        public void SortUnassigned(SortColumn column)
        {
            if (_unassignedSortColumn == column)
                _unassignedSortAscending = !_unassignedSortAscending;
            else
            {
                _unassignedSortColumn = column;
                _unassignedSortAscending = true;
            }
            NotifyStateChanged();
        }

        public void SortAssigned(SortColumn column)
        {
            if (_assignedSortColumn == column)
                _assignedSortAscending = !_assignedSortAscending;
            else
            {
                _assignedSortColumn = column;
                _assignedSortAscending = true;
            }
            NotifyStateChanged();
        }

        public void SortCompleted(SortColumn column)
        {
            if (_completedSortColumn == column)
                _completedSortAscending = !_completedSortAscending;
            else
            {
                _completedSortColumn = column;
                _completedSortAscending = true;
            }
            NotifyStateChanged();
        }

        // ------------------------------
        // PAGED + SORTED GETTERS
        // ------------------------------
        public IEnumerable<Order> GetPagedUnassignedOrders()
        {
            // 1) Sort
            var sorted = UnassignedOrders.OrderByColumn(_unassignedSortColumn, _unassignedSortAscending);
            // 2) Page
            return sorted
                .Skip((_unassignedCurrentPage - 1) * ItemsPerPage)
                .Take(ItemsPerPage);
        }

        public IEnumerable<Order> GetPagedAssignedOrders()
        {
            var sorted = AssignedOrders.OrderByColumn(_assignedSortColumn, _assignedSortAscending);
            return sorted
                .Skip((_assignedCurrentPage - 1) * ItemsPerPage)
                .Take(ItemsPerPage);
        }

        public IEnumerable<Order> GetPagedCompletedOrders()
        {
            var sorted = CompletedOrders.OrderByColumn(_completedSortColumn, _completedSortAscending);
            return sorted
                .Skip((_completedCurrentPage - 1) * ItemsPerPage)
                .Take(ItemsPerPage);
        }

        // ------------------------------
        // NEXT / PREV PAGE
        // ------------------------------
        public void NextUnassignedPage()
        {
            if (_unassignedCurrentPage < UnassignedTotalPages)
            {
                _unassignedCurrentPage++;
                NotifyStateChanged();
            }
        }

        public void PrevUnassignedPage()
        {
            if (_unassignedCurrentPage > 1)
            {
                _unassignedCurrentPage--;
                NotifyStateChanged();
            }
        }

        public void NextAssignedPage()
        {
            if (_assignedCurrentPage < AssignedTotalPages)
            {
                _assignedCurrentPage++;
                NotifyStateChanged();
            }
        }

        public void PrevAssignedPage()
        {
            if (_assignedCurrentPage > 1)
            {
                _assignedCurrentPage--;
                NotifyStateChanged();
            }
        }

        public void NextCompletedPage()
        {
            if (_completedCurrentPage < CompletedTotalPages)
            {
                _completedCurrentPage++;
                NotifyStateChanged();
            }
        }

        public void PrevCompletedPage()
        {
            if (_completedCurrentPage > 1)
            {
                _completedCurrentPage--;
                NotifyStateChanged();
            }
        }

        // ------------------------------
        // OTHER ORDER OPERATIONS
        // ------------------------------
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
                await LoadOrdersAsync(ActiveView);
                _toastService.ShowSuccess($"Order #{order.OrderId} assigned successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error assigning order: {ex.Message}");
                _toastService.ShowError($"Failed to assign Order #{order.OrderId}.");
            }
            finally
            {
                NotifyStateChanged();
            }
        }

        public void ToggleOrderDetails(int orderId, string activeView)
        {
            if (SelectedOrder?.OrderId == orderId)
            {
                // Deselect the order if it's already selected
                SelectedOrder = null;
            }
            else
            {
                // Select the order based on the active view
                SelectedOrder = activeView switch
                {
                    "Unassigned" => UnassignedOrders.FirstOrDefault(o => o.OrderId == orderId),
                    "Assigned" => AssignedOrders.FirstOrDefault(o => o.OrderId == orderId),
                    "Completed" => CompletedOrders.FirstOrDefault(o => o.OrderId == orderId),
                    _ => null
                };
                ViewingUnassignedDetails = (activeView == "Unassigned");
            }

            NotifyStateChanged(); // Update the UI
        }

        public bool CanCompleteOrder()
        {
            return SelectedOrder?.OrderItems.All(i => i.QuantityToPick == 0) ?? false;
        }

        public async Task CompleteOrderAsync()
        {
            if (SelectedOrder == null || !CanCompleteOrder()) return;

            try
            {
                var request = new UpdateOrderStatusRequest
                {
                    OrderId = SelectedOrder.OrderId,
                    NewStatus = OrderStatus.Completed
                };

                await _orderUseCases.UpdateOrderStatusAsync(request);
                SelectedOrder = null;
                await LoadOrdersAsync(ActiveView);
                _toastService.ShowSuccess("Order completed successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error completing order: {ex.Message}");
                _toastService.ShowError("Failed to complete order.");
            }
            finally
            {
                NotifyStateChanged();
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
                        OrderItemId = item.OrderItemId,
                        QuantityToPick = item.QuantityToPick - 1
                    };

                    await _orderItemUseCases.UpdateOrderItemsAsync(request);
                    item.QuantityToPick--;
                    _toastService.ShowSuccess($"Picked up 1 unit of {item.ItemName}.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error picking up item: {ex.Message}");
                    _toastService.ShowError($"Failed to pick up {item.ItemName}.");
                }
                finally
                {
                    NotifyStateChanged();
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
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error reverting pickup item: {ex.Message}");
                    _toastService.ShowError($"Failed to revert pickup for {item.ItemName}.");
                }
                finally
                {
                    NotifyStateChanged();
                }
            }
        }

        // ------------------------------
        // HELPERS / STATE CHANGE
        // ------------------------------
        public string FormatTimestamp(Google.Protobuf.WellKnownTypes.Timestamp timestamp)
        {
            return timestamp?.ToDateTime().ToString("yyyy-MM-dd") ?? string.Empty;
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

        public void Logout()
        {
            ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _navigationManager.NavigateTo("/login");
        }
    }
}
