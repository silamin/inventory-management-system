using BlazorServerApp.Application.Interfaces;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders;

namespace BlazorServerApp.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderService.OrderServiceClient _client;

        public OrderRepository(OrderService.OrderServiceClient client)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
        }

        public async Task<bool> AddOrderAsync(CreateOrder order)
        {
            try
            {
                // This part is left unimplemented since it's unrelated to the issue
                throw new NotImplementedException();
            }
            catch (RpcException ex)
            {
                // Handle gRPC exception appropriately (logging, rethrow, etc.)
                throw new ApplicationException("Error adding order", ex);
            }
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            try
            {
                // Create an empty request as required by the gRPC method signature
                var request = new Empty();

                // Call the gRPC service and await the response
                var response = await _client.getAllOrdersAsync(request);

                // Extract and return the list of orders from the response
                return response.Orders;
            }
            catch (RpcException ex)
            {
                // Log and rethrow the exception with an appropriate message
                throw new ApplicationException("Error retrieving all orders", ex);
            }
        }
    }
}
