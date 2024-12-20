using BlazorServerApp.Application.UseCases;
using Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp.Managers
{
    public class OrderHistoryManager
    {
        private readonly OrderUseCases _orderUseCases;

        public OrderHistoryManager(OrderUseCases orderUseCases)
        {
            _orderUseCases = orderUseCases;
        }

        // Filters
        public OrderStatus? SelectedStatus { get; set; } = null;
        private string _searchQuery = string.Empty;
        public string SearchQuery
        {
            get => _searchQuery;
            set => _searchQuery = value;
        }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Pagination
        public int CurrentPage { get; private set; } = 1;
        public int PageSize { get; set; } = 4;

        // Sorting
        public string SortColumn { get; private set; } = "CreatedAt";
        public bool Ascending { get; private set; } = true;

        // Cached Orders
        private List<Order> AllOrders = new();

        public async Task LoadAllOrdersAsync()
        {
            AllOrders = (await _orderUseCases.GetAllOrdersAsync()).ToList();
        }

        public void ResetPagination()
        {
            CurrentPage = 1;
        }

        private IEnumerable<Order> FilterAndSortOrders()
        {
            var orders = AllOrders.AsQueryable();

            // Filters
            if (SelectedStatus.HasValue)
                orders = orders.Where(o => o.OrderStatus == SelectedStatus.Value);

            if (!string.IsNullOrWhiteSpace(SearchQuery))
                orders = orders.Where(o => o.OrderId.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

            if (StartDate.HasValue)
                orders = orders.Where(o => o.CreatedAt.ToDateTime() >= StartDate.Value);

            if (EndDate.HasValue)
                orders = orders.Where(o => o.CreatedAt.ToDateTime() <= EndDate.Value);

            // Sorting
            return SortColumn switch
            {
                "OrderId" => Ascending ? orders.OrderBy(o => o.OrderId) : orders.OrderByDescending(o => o.OrderId),
                "CreatedAt" => Ascending ? orders.OrderBy(o => o.CreatedAt) : orders.OrderByDescending(o => o.CreatedAt),
                "Status" => Ascending ? orders.OrderBy(o => o.OrderStatus.ToString()) : orders.OrderByDescending(o => o.OrderStatus.ToString()),
                _ => orders
            };
        }

        public IEnumerable<Order> PagedItems => FilterAndSortOrders()
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize);

        public bool IsFirstPage => CurrentPage == 1;
        public bool IsLastPage => CurrentPage >= TotalPages;

        public int TotalPages => Math.Max(1, (int)Math.Ceiling(FilterAndSortOrders().Count() / (double)PageSize));

        public void PreviousPage()
        {
            if (!IsFirstPage)
                CurrentPage--;
        }

        public void NextPage()
        {
            if (!IsLastPage)
                CurrentPage++;
        }

        public void SortByColumn(string columnName)
        {
            if (SortColumn == columnName)
            {
                // Toggle direction if same column is clicked again
                Ascending = !Ascending;
            }
            else
            {
                // Switch to a new sort column and reset to ascending
                SortColumn = columnName;
                Ascending = true;
            }
        }

        public string GetSortIcon(string columnName)
        {
            if (SortColumn == columnName)
                return Ascending ? "fas fa-sort-up ms-1" : "fas fa-sort-down ms-1";
            return "fas fa-sort ms-1 text-muted";
        }

        public string GetStatusClass(OrderStatus status)
        {
            return status switch
            {
                OrderStatus.Completed => "text-success fw-bold",
                OrderStatus.InProgress => "text-warning fw-bold",
                _ => string.Empty
            };
        }

        public void ClearFilters()
        {
            SelectedStatus = null;
            SearchQuery = string.Empty;
            StartDate = null;
            EndDate = null;
            CurrentPage = 1;
        }
    }
}
