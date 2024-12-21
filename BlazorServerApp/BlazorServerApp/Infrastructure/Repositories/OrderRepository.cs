using BlazorServerApp.Application.Interfaces;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders;
using Status = Orders.Status;

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
                // Call the gRPC method `createOrder` and await the response
                var response = await _client.createOrderAsync(order);

                // Return the success flag from the response
                return response.Success;
            }
            catch (RpcException ex)
            {
                // Log the exception or handle it accordingly
                throw new ApplicationException($"gRPC error while adding order: {ex.Status.Detail}", ex);
            }
            catch (Exception ex)
            {
                // Handle any other exceptions
                throw new ApplicationException("Unexpected error while adding order.", ex);
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

        public async Task<IEnumerable<Order>> GetOrdersAsync(OrderStatus status)
        {
            try
            {
                // Wrap the OrderStatus enum value in a Status message
                var request = new Status { OrderStatus = status };

                // Call the gRPC service and await the response
                var response = await _client.getOrdersAsync(request);

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
