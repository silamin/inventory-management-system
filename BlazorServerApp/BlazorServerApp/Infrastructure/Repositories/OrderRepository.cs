using BlazorServerApp.Application.Interfaces;
using Grpc.Core;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orders;
using Status = Orders.Status;
using Items;

namespace BlazorServerApp.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderService.OrderServiceClient _client;
        private readonly CustomAuthenticationStateProvider _authStateProvider;

        public OrderRepository(OrderService.OrderServiceClient client, CustomAuthenticationStateProvider authStateProvider)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _authStateProvider = authStateProvider;
        }

        public async Task<bool> AddOrderAsync(CreateOrder order)
        {
            try
            {
                var callOptions = await GetAuthenticatedCallOptionsAsync();
                var response = await _client.createOrderAsync(order, callOptions);

                return response.Success;
            }
            catch (RpcException ex)
            {
                throw new ApplicationException($"gRPC error while adding order: {ex.Status.Detail}", ex);
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Unexpected error while adding order.", ex);
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync(OrderStatus status)
        {
            try
            {
                var callOptions = await GetAuthenticatedCallOptionsAsync();
                var request = new Status { OrderStatus = status };

                var response = await _client.getOrdersAsync(request, callOptions);

                return response.Orders;
            }
            catch (RpcException ex)
            {
                throw new ApplicationException("Error retrieving all orders", ex);
            }
        }
        public async Task UpdateOrderStatus(UpdateOrderStatusRequest newStatus)
        {
            try
            {
                var callOptions = await GetAuthenticatedCallOptionsAsync();
                await _client.updateOrderStatusAsync(newStatus, callOptions);
            }
            catch (RpcException ex)
            {
                throw new ApplicationException("Error editing item", ex);
            }
        }

        private async Task<CallOptions> GetAuthenticatedCallOptionsAsync()
        {
            var token = await _authStateProvider.GetTokenAsync();

            var metadata = new Metadata();
            if (!string.IsNullOrEmpty(token))
            {
                metadata.Add("Authorization", $"Bearer {token}");
            }

            return new CallOptions(metadata);
        }

    }
}
