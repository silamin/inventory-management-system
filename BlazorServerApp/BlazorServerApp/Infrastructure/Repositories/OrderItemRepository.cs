using BlazorServerApp.Application.Interfaces;
using Grpc.Core;
using Items;
using OrderItems;
using Orders;

namespace BlazorServerApp.Infrastructure.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        private readonly OrderItemService.OrderItemServiceClient _client;
        private readonly CustomAuthenticationStateProvider _authStateProvider;

        public OrderItemRepository(OrderItemService.OrderItemServiceClient client, CustomAuthenticationStateProvider authStateProvider)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            _authStateProvider = authStateProvider;
        }

        public async Task UpdateOrderItemAsync(UpdateOrderItemRequest orderItemDTO)
        {
            try
            {
                var callOptions = await GetAuthenticatedCallOptionsAsync();
                await _client.updateOrderItemAsync(orderItemDTO, callOptions);
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
