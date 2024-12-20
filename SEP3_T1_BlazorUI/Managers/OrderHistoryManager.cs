using System;
using System.Collections.Generic;
using System.Linq;
using SEP3_T1_BlazorUI.Models;
using SEP3_T1_BlazorUI.Application.UseCases;

namespace SEP3_T1_BlazorUI.Presentation.Managers
{
    public class OrderHistoryManager
    {
        private readonly OrderUseCases _orderUseCases;

        public OrderHistoryManager(OrderUseCases orderUseCases)
        {
            _orderUseCases = orderUseCases;
        }

        public string SelectedStatus { get; set; } = string.Empty;
        public string SearchQuery { get; set; } = string.Empty;
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public int CurrentPage { get; private set; } = 1;
        public int PageSize { get; set; } = 17;

        public string SortColumn { get; private set; } = "OrderDate";
        public bool Ascending { get; private set; } = true;

        public void SortByOrderId() => SortByColumn("OrderId");
        public void SortByOrderDate() => SortByColumn("OrderDate");
        public void SortByStatus() => SortByColumn("Status");
        public void SortByEmployeeId() => SortByColumn("EmployeeId");


        public IEnumerable<Order> FilteredOrders
        {
            get
            {
                var orders = _orderUseCases.GetAllOrders().AsQueryable();

                if (!string.IsNullOrWhiteSpace(SelectedStatus))
                {
                    orders = orders.Where(o => o.Status.Equals(SelectedStatus, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(SearchQuery) && int.TryParse(SearchQuery, out var orderId))
                {
                    orders = orders.Where(o => o.OrderId == orderId);
                }

                if (StartDate.HasValue)
                {
                    orders = orders.Where(o => o.OrderDate >= StartDate.Value);
                }

                if (EndDate.HasValue)
                {
                    orders = orders.Where(o => o.OrderDate <= EndDate.Value);
                }

                orders = SortColumn switch
                {
                    "OrderId" => Ascending ? orders.OrderBy(o => o.OrderId) : orders.OrderByDescending(o => o.OrderId),
                    "OrderDate" => Ascending ? orders.OrderBy(o => o.OrderDate) : orders.OrderByDescending(o => o.OrderDate),
                    "Status" => Ascending ? orders.OrderBy(o => o.Status) : orders.OrderByDescending(o => o.Status),
                    "EmployeeId" => Ascending ? orders.OrderBy(o => o.EmployeeId) : orders.OrderByDescending(o => o.EmployeeId),
                    _ => orders
                };

                return orders.ToList();
            }
        }

        public IEnumerable<Order> PaginatedOrders => FilteredOrders
            .Skip((CurrentPage - 1) * PageSize)
            .Take(PageSize);

        public int TotalPages => (int)Math.Ceiling(FilteredOrders.Count() / (double)PageSize);
        public bool IsFirstPage => CurrentPage == 1;
        public bool IsLastPage => CurrentPage == TotalPages;

        public void PreviousPage()
        {
            if (!IsFirstPage) CurrentPage--;
        }

        public void NextPage()
        {
            if (!IsLastPage) CurrentPage++;
        }

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

        public string GetSortIcon(string columnName)
        {
            return SortColumn == columnName
                ? (Ascending ? "fas fa-sort-up" : "fas fa-sort-down")
                : "fas fa-sort";
        }

        public string GetStatusClass(string status)
        {
            return status switch
            {
                "Completed" => "text-success fw-bold",
                "Rejected" => "text-danger fw-bold",
                _ => string.Empty
            };
        }

        public void ClearSearch()
        {
            SearchQuery = string.Empty;
            CurrentPage = 1;
        }
    }
}
