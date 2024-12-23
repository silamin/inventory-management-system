using Orders;

namespace BlazorServerApp.Helpers
{
    public enum SortColumn
    {
        OrderId,
        DeliveryDate,
        CreatedAt
    }

    public static class OrderExtensions
    {
        public static IEnumerable<Order> OrderByColumn(
            this IEnumerable<Order> source,
            SortColumn column,
            bool ascending)
        {
            return column switch
            {
                SortColumn.OrderId => ascending
                    ? source.OrderBy(o => o.OrderId)
                    : source.OrderByDescending(o => o.OrderId),

                SortColumn.DeliveryDate => ascending
                    ? source.OrderBy(o => o.DeliveryDate?.ToDateTime())
                    : source.OrderByDescending(o => o.DeliveryDate?.ToDateTime()),

                SortColumn.CreatedAt => ascending
                    ? source.OrderBy(o => o.CreatedAt?.ToDateTime())
                    : source.OrderByDescending(o => o.CreatedAt?.ToDateTime()),

                _ => source
            };
        }
    }

}
