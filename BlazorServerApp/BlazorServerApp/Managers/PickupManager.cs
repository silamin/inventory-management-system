using BlazorServerApp.Application.UseCases;
using Orders;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServerApp.Managers
{
    public class PickupManager
    {
        private readonly OrderUseCases _orderUseCases;

        public PickupManager(OrderUseCases orderUseCases)
        {
            _orderUseCases = orderUseCases;
        }

        public List<Order> Orders { get; private set; } = new();
        public bool IsLoading { get; private set; } = true;

        public async Task LoadOrdersAsync()
        {
            IsLoading = true;
            try
            {
                Orders = (await _orderUseCases.GetOrdersByStatusAsync(OrderStatus.InProgress)).ToList();
            }
            finally
            {
                IsLoading = false;
            }
        }


    }
}
