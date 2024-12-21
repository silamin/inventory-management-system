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
    public int PageSize { get; set; } = 3;
    public int InProgressPage { get; private set; } = 1;
    public int CompletedPage { get; private set; } = 1;

    // Cached Orders
    private List<Order> InProgressOrders = new();
    private List<Order> CompletedOrders = new();

    public async Task LoadOrdersAsync(OrderStatus status)
    {
        try
        {
            var response = await _orderUseCases.GetOrdersByStatusAsync(status);

            if (status == OrderStatus.InProgress)
            {
                InProgressOrders = response.ToList();
            }
            else if (status == OrderStatus.Completed)
            {
                CompletedOrders = response.ToList();
            }

            Console.WriteLine($"Loaded {response.Count()} orders for {status}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading orders for {status}: {ex.Message}");
        }
    }


    public IEnumerable<Order> GetOrdersByStatus(OrderStatus status)
    {
        var orders = status == OrderStatus.InProgress ? InProgressOrders : CompletedOrders;

        // Ensure filtering logic includes all cases
        var filteredOrders = orders
            .Where(o =>
                (string.IsNullOrEmpty(SearchQuery) || o.OrderId.ToString().Contains(SearchQuery, StringComparison.OrdinalIgnoreCase)) &&
                (!StartDate.HasValue || o.CreatedAt.ToDateTime() >= StartDate.Value) &&
                (!EndDate.HasValue || o.CreatedAt.ToDateTime() <= EndDate.Value));

        // Log the filtered results (for debugging purposes)
        Console.WriteLine($"Filtered Orders for {status}: {filteredOrders.Count()}");

        return filteredOrders.OrderBy(o => o.CreatedAt);
    }


    public int GetTotalPages(OrderStatus status)
    {
        var orders = status == OrderStatus.InProgress ? InProgressOrders : CompletedOrders;
        return Math.Max(1, (int)Math.Ceiling(orders.Count / (double)PageSize));
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
