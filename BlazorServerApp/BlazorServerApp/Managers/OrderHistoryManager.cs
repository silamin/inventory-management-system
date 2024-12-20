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

        // Pagination for each status
        public Dictionary<OrderStatus, int> CurrentPage { get; private set; } = new();
        public Dictionary<OrderStatus, int> TotalPages { get; private set; } = new();
        public int PageSize { get; set; } = 3;

        // Sorting
        public string SortColumn { get; private set; } = "CreatedAt";
        public bool Ascending { get; private set; } = true;

        // Cached Orders
        private List<Order> AllOrders = new();

        // Load all orders from the backend
        public async Task LoadAllOrdersAsync()
        {
            AllOrders = (await _orderUseCases.GetAllOrdersAsync()).ToList();
        }

        public void ResetAllPagination()
        {
            CurrentPage.Clear();
        }

        private IEnumerable<Order> FilterAndSortOrders()
        {
            var orders = AllOrders.AsQueryable();

            // Apply filters
            if (SelectedStatus.HasValue)
                orders = orders.Where(o => o.OrderStatus == SelectedStatus.Value);

            if (!string.IsNullOrWhiteSpace(SearchQuery))
                orders = orders.Where(o => o.OrderId.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase));

            if (StartDate.HasValue)
                orders = orders.Where(o => o.CreatedAt.ToDateTime() >= StartDate.Value);

            if (EndDate.HasValue)
                orders = orders.Where(o => o.CreatedAt.ToDateTime() <= EndDate.Value);

            // Apply sorting
            return SortColumn switch
            {
                "OrderId" => Ascending ? orders.OrderBy(o => o.OrderId) : orders.OrderByDescending(o => o.OrderId),
                "CreatedAt" => Ascending ? orders.OrderBy(o => o.CreatedAt) : orders.OrderByDescending(o => o.CreatedAt),
                "Status" => Ascending ? orders.OrderBy(o => o.OrderStatus.ToString()) : orders.OrderByDescending(o => o.OrderStatus.ToString()),
                _ => orders
            };
        }

        public IEnumerable<Order> GetPagedOrders()
        {
            return AllOrders
                .GroupBy(o => o.OrderStatus)
                .SelectMany(g => g.Skip((GetCurrentPage(g.Key) - 1) * PageSize).Take(PageSize));
        }

        public IEnumerable<Order> GetPagedOrdersByStatus(OrderStatus status)
        {
            var filteredOrders = FilterAndSortOrders().Where(o => o.OrderStatus == status).ToList();
            var currentPage = GetCurrentPage(status);
            return filteredOrders
                .Skip((currentPage - 1) * PageSize)
                .Take(PageSize);
        }

        public int GetCurrentPage(OrderStatus status)
        {
            if (!CurrentPage.ContainsKey(status))
                CurrentPage[status] = 1;
            return CurrentPage[status];
        }

        public int GetTotalPages(OrderStatus status)
        {
            if (!TotalPages.ContainsKey(status))
            {
                var totalOrders = FilterAndSortOrders().Where(o => o.OrderStatus == status).Count();
                TotalPages[status] = Math.Max(1, (int)Math.Ceiling(totalOrders / (double)PageSize));
            }
            return TotalPages[status];
        }

        public void NextPage(OrderStatus status)
        {
            var currentPage = GetCurrentPage(status);
            var totalPages = GetTotalPages(status);
            if (currentPage < totalPages)
            {
                CurrentPage[status] = currentPage + 1;
            }
        }

        public void PreviousPage(OrderStatus status)
        {
            var currentPage = GetCurrentPage(status);
            if (currentPage > 1)
            {
                CurrentPage[status] = currentPage - 1;
            }
        }

        public bool IsFirstPage(OrderStatus status) => GetCurrentPage(status) == 1;

        public bool IsLastPage(OrderStatus status) => GetCurrentPage(status) >= GetTotalPages(status);

        public void SortByColumn(string columnName)
        {
            if (SortColumn == columnName)
            {
                // Toggle sorting direction if the same column is clicked
                Ascending = !Ascending;
            }
            else
            {
                // Switch to a new sort column and default to ascending
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
            ResetAllPagination();
        }
    }
}
