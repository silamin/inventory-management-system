using BlazorServerApp.Application.UseCases;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Items;
using Microsoft.AspNetCore.Components.Authorization;
using Orders;

namespace BlazorServerApp.Managers
{
    public class InventoryManager
    {
        private readonly ItemUseCases _itemUseCases;
        private readonly OrderUseCases _orderUseCases;
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        private string _searchQuery = string.Empty;

        // This will hold the currently loaded items as a snapshot of data
        private List<ItemViewModel> _allItems = new List<ItemViewModel>();

        public InventoryManager(ItemUseCases itemUseCases, OrderUseCases orderUseCases, AuthenticationStateProvider authenticationStateProvider)
        {
            _itemUseCases = itemUseCases;
            _orderUseCases = orderUseCases;
            _authenticationStateProvider = authenticationStateProvider;
        }

        // Initial load method; call this once on page load (e.g., OnInitializedAsync in the component)
        public void LoadData()
        {
            var items = _itemUseCases.GetAllItems();
            _allItems = items.Select(item => new ItemViewModel
            {
                Id = item.ItemId,
                Name = item.ItemName,
                Description = item.Description,
                QuantityInStore = item.QuantityInStore
            }).ToList();
        }

        // Refresh data to simulate a fresh navigation to this page
        public void RefreshData()
        {
            // Re-load all items from the use case to get the freshest data
            LoadData();
        }

        // Search filter
        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                if (_searchQuery != value)
                {
                    _searchQuery = value;
                    CurrentPage = 1;
                }
            }
        }

        public void ClearSearch()
        {
            SearchQuery = string.Empty;
            CurrentPage = 1;
        }

        // Sorting
        public string SortColumn { get; private set; } = "Name";
        public bool Ascending { get; private set; } = true;

        public void SortByName() => SortByColumn("Name");
        public void SortByDescription() => SortByColumn("Description");
        public void SortByQuantityInStore() => SortByColumn("QuantityInStore");

        public void SortByColumn(string columnName)
        {
            if (SortColumn == columnName)
            {
                Ascending = !Ascending;
            }
            else
            {
                SortColumn = columnName;
                Ascending = true;
            }
        }

        public string GetSortIcon(string columnName) => SortColumn == columnName
            ? (Ascending ? "fas fa-sort-up" : "fas fa-sort-down")
            : "fas fa-sort";

        public IEnumerable<ItemViewModel> FilterAndSortItems()
        {
            var items = _allItems.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                items = items.Where(i => i.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                                         i.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            return SortColumn switch
            {
                "Name" => Ascending ? items.OrderBy(i => i.Name) : items.OrderByDescending(i => i.Name),
                "Description" => Ascending ? items.OrderBy(i => i.Description) : items.OrderByDescending(i => i.Description),
                "QuantityInStore" => Ascending ? items.OrderBy(i => i.QuantityInStore) : items.OrderByDescending(i => i.QuantityInStore),
                _ => items
            };
        }

        // Pagination
        public int CurrentPage { get; private set; } = 1;
        public int PageSize { get; private set; } = 12;

        public IEnumerable<ItemViewModel> PagedItems => FilterAndSortItems()
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize);

        public bool IsFirstPage => CurrentPage == 1;
        public bool IsLastPage => CurrentPage >= TotalPages;

        public int TotalPages => Math.Max(1, (int)Math.Ceiling(FilterAndSortItems().Count() / (double)PageSize));

        public void PreviousPage()
        {
            if (!IsFirstPage)
                CurrentPage--;

            if (CurrentPage > TotalPages)
                CurrentPage = TotalPages;
        }

        public void NextPage()
        {
            if (!IsLastPage)
                CurrentPage++;

            if (CurrentPage > TotalPages)
                CurrentPage = TotalPages;
        }

        // Item operations
        public async Task UpdateItem(ItemViewModel item)
        {
            var updatedItem = new Item
            {
                ItemId = item.Id,
                ItemName = item.Name,
                Description = item.Description,
                QuantityInStore = item.QuantityInStore
            };

            await _itemUseCases.EditItemAsync(updatedItem);
        }

        public async Task DeleteItemAsync(ItemViewModel item)
        {
            try
            {
                var itemToDelete = new DeleteItem
                {
                    ItemId = item.Id
                };

                // Call the gRPC deleteItem method
                await _itemUseCases.DeleteItemAsync(itemToDelete);

                // Remove the item from the local _allItems list only if the delete was successful
                _allItems.RemoveAll(i => i.Id == item.Id);

                // Log success message
                Console.WriteLine($"Successfully deleted item with Id: {item.Id}");
            }
            catch (RpcException ex)
            {
                // Handle gRPC exception appropriately (logging, rethrow, etc.)
                Console.WriteLine($"Error during DeleteItem request for Id: {item.Id}. RPC Exception: {ex}");
                throw new ApplicationException($"Error deleting item with Id: {item.Id}", ex);
            }
            catch (Exception ex)
            {
                // Handle any other types of exceptions
                Console.WriteLine($"An unexpected error occurred while deleting item with Id: {item.Id}. Exception: {ex}");
                throw new ApplicationException($"Unexpected error deleting item with Id: {item.Id}", ex);
            }
        }


        public bool HasSelectedItems => FilterAndSortItems().Any(i => i.IsSelected);

        public async Task PlaceOrder()
        {
            var selectedItems = FilterAndSortItems().Where(i => i.IsSelected).ToList();
            var orderItems = selectedItems.Select(i => new CreateOrderItem
            {
                ItemId = i.Id,
                TotalQuantity = i.OrderQuantity
            }).ToList();

            var orderRequest = new CreateOrder
            {
                OrderItems = { orderItems },
                DeliveryDate = Timestamp.FromDateTime(DateTime.UtcNow),
                CreatedBy = 1
            };

            await _orderUseCases.AddOrderAsync(orderRequest);
        }
    }

}


public class ItemViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int QuantityInStore { get; set; }

    // Client-side properties for UI interactions
    public bool IsSelected { get; set; } = false; 
    public int OrderQuantity { get; set; } = 0;
    public bool IsDeleting { get; set; } = false;
}
