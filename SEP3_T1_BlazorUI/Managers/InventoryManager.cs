using SEP3_T1_BlazorUI.Models;
using SEP3_T1_BlazorUI.Application.UseCases;
using Microsoft.AspNetCore.Components.Authorization;

namespace SEP3_T1_BlazorUI.Presentation.Managers
{
    public class InventoryManager
    {
        private readonly ItemUseCases _itemUseCases;
        private readonly OrderUseCases _orderUseCases;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private string _searchQuery = string.Empty;

        public InventoryManager(ItemUseCases itemUseCases, OrderUseCases orderUseCases, AuthenticationStateProvider authenticationStateProvider)
        {
            _itemUseCases = itemUseCases;
            _orderUseCases = orderUseCases;
            _authenticationStateProvider = authenticationStateProvider;
        }

        //Search filter
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

        //Sorting
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

        public IEnumerable<Item> FilterAndSortItems()
        {
            var items = _itemUseCases.GetAllItems().AsQueryable();

            if (!string.IsNullOrWhiteSpace(SearchQuery))
            {
                items = items.Where(i =>
                    i.Name.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase) ||
                    i.Description.Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));
            }

            items = SortColumn switch
            {
                "Name" => Ascending ? items.OrderBy(i => i.Name) : items.OrderByDescending(i => i.Name),
                "Description" => Ascending ? items.OrderBy(i => i.Description) : items.OrderByDescending(i => i.Description),
                "QuantityInStore" => Ascending ? items.OrderBy(i => i.QuantityInStore) : items.OrderByDescending(i => i.QuantityInStore),
                _ => items
            };

            return items;
        }

        //Pagination
        public bool IsFirstPage => CurrentPage == 1;
        public bool IsLastPage => CurrentPage >= TotalPages;

        public int TotalPages => Math.Max(1, (int)Math.Ceiling(FilterAndSortItems().Count() / (double)PageSize));

        public int CurrentPage { get; private set; } = 1;
        public int PageSize { get; private set; } = 12;

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

        public IEnumerable<Item> PagedItems => FilterAndSortItems()
    .Skip((CurrentPage - 1) * PageSize)
    .Take(PageSize);

        public void UpdateItem(Item item)
        {
            _itemUseCases.UpdateItem(item);
        }

        public void DeleteItem(Item item)
        {
            _itemUseCases.DeleteItem(item);
        }

        public bool HasSelectedItems => FilterAndSortItems().Any(i => i.IsSelected);

        public async Task PlaceOrder()
        {
            var selectedItems = FilterAndSortItems().Where(i => i.IsSelected && i.OrderQuantity > 0).ToList();
            if (!selectedItems.Any())
            {
                Console.WriteLine("No items selected for the order.");
                return;
            }

            var authState = await _authenticationStateProvider.GetAuthenticationStateAsync();
            var employeeIdClaim = authState.User.Claims.FirstOrDefault(c => c.Type == "WorkingNumber");

            if (employeeIdClaim == null)
            {
                Console.WriteLine("Failed to get Employee ID from the authentication state.");
                return;
            }

            var employeeId = employeeIdClaim.Value;

            var newOrder = new Order
            {
                OrderDate = DateTime.Now,
                Status = "Pending",
                TotalQuantity = selectedItems.Sum(i => i.OrderQuantity),
                EmployeeId = employeeId, 
                OrderItems = selectedItems.Select(i => new OrderItem
                {
                    ItemId = i.Id,
                    Name = i.Name,
                    QuantityOrdered = i.OrderQuantity
                }).ToList()
            };

            Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(newOrder, Newtonsoft.Json.Formatting.Indented));

            _orderUseCases.AddOrder(newOrder);

            foreach (var item in selectedItems)
            {
                item.QuantityInStore -= item.OrderQuantity; 
                if (item.QuantityInStore < 0) item.QuantityInStore = 0; 
                item.OrderQuantity = 0; 
                item.IsSelected = false; 
                UpdateItem(item); 
            }
        }
    }
}
