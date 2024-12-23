using BlazorServerApp.Application.UseCases;
using Orders;

public class OrderHistoryManager
{
    private readonly OrderUseCases _orderUseCases;

    public OrderHistoryManager(OrderUseCases orderUseCases)
    {
        _orderUseCases = orderUseCases;
    }

    // Filters
    public string SearchQuery { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    // Pagination
    public int InProgressPageSize { get; set; } = 3;
    public int CompletedPageSize { get; set; } = 7;
    public int InProgressPage { get; private set; } = 1;
    public int CompletedPage { get; private set; } = 1;

    // Sorting
    public string SortColumn { get; private set; } = "CreatedAt"; // Default sort column
    public bool Ascending { get; private set; } = true;

    // Cached Orders
    private List<Order> InProgressOrders = new();
    private List<Order> CompletedOrders = new();

    private Action? _stateChangedCallback;

    public bool IsLoading { get; private set; } = true;
    public void RegisterStateChangeCallback(Action callback)
    {
        _stateChangedCallback = callback;
    }
    private void NotifyStateChanged()
    {
        _stateChangedCallback?.Invoke();
    }

    private List<Order> NotStartedOrders = new();

    public async Task LoadOrdersAsync(OrderStatus status)
    {
        IsLoading = true;
        NotifyStateChanged();

        try
        {
            var response = await _orderUseCases.GetOrdersByStatusAsync(status);

            if (status == OrderStatus.NotStarted)
            {
                NotStartedOrders = response.ToList();
            }
            else if (status == OrderStatus.InProgress)
            {
                InProgressOrders = response.ToList();
            }
            else if (status == OrderStatus.Completed)
            {
                CompletedOrders = response.ToList();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading orders for {status}: {ex.Message}");
        }
        finally
        {
            IsLoading = false;
            NotifyStateChanged();
        }
    }

    public IEnumerable<Order> GetOrdersByStatus(OrderStatus status)
    {
        var orders = status switch
        {
            OrderStatus.NotStarted => NotStartedOrders,
            OrderStatus.InProgress => InProgressOrders,
            OrderStatus.Completed => CompletedOrders,
            _ => new List<Order>()
        };
        var filteredOrders = orders
    .Where(o =>
        (string.IsNullOrEmpty(SearchQuery) || o.OrderId.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)) &&
        (!StartDate.HasValue || o.CreatedAt.ToDateTime() >= StartDate.Value) &&
        (!EndDate.HasValue || o.CreatedAt.ToDateTime() <= EndDate.Value));

        // Sort orders based on the selected column and direction
        return SortOrders(filteredOrders);

    }


    private IEnumerable<Order> SortOrders(IEnumerable<Order> orders)
    {
        return SortColumn switch
        {
            "OrderId" => Ascending ? orders.OrderBy(o => o.OrderId) : orders.OrderByDescending(o => o.OrderId),
            "OrderDate" => Ascending ? orders.OrderBy(o => o.CreatedAt) : orders.OrderByDescending(o => o.CreatedAt),
            "DeliveryDate" => Ascending ? orders.OrderBy(o => o.DeliveryDate) : orders.OrderByDescending(o => o.DeliveryDate),
            "AssignedUser" => Ascending ? orders.OrderBy(o => o.AssignedUser) : orders.OrderByDescending(o => o.AssignedUser),
            "CreatedBy" => Ascending ? orders.OrderBy(o => o.CreatedByUser) : orders.OrderByDescending(o => o.CreatedByUser),
            "CompletedAt" => Ascending ? orders.OrderBy(o => o.CompletedAt) : orders.OrderByDescending(o => o.CompletedAt),

            _ => orders
        };
    }

    public void SetSorting(string column)
    {
        if (SortColumn == column)
        {
            Ascending = !Ascending; // Toggle sorting direction
        }
        else
        {
            SortColumn = column;
            Ascending = true; // Default to ascending when switching columns
        }
    }

    public int GetTotalPages(OrderStatus status)
    {
        var pageSize = GetPageSize(status);
        var orders = status == OrderStatus.InProgress ? InProgressOrders : CompletedOrders;
        return Math.Max(1, (int)Math.Ceiling(orders.Count / (double)pageSize));
    }

    public int GetPageSize(OrderStatus status)
    {
        return status == OrderStatus.InProgress ? InProgressPageSize : CompletedPageSize;
    }

    public void NextPage(OrderStatus status)
    {
        if (status == OrderStatus.InProgress)
            InProgressPage++;
        else if (status == OrderStatus.Completed)
            CompletedPage++;
    }

    public void PreviousPage(OrderStatus status)
    {
        if (status == OrderStatus.InProgress && InProgressPage > 1)
            InProgressPage--;
        else if (status == OrderStatus.Completed && CompletedPage > 1)
            CompletedPage--;
    }

    public void ClearFilters()
    {
        SearchQuery = string.Empty;
        StartDate = null;
        EndDate = null;
    }
}
