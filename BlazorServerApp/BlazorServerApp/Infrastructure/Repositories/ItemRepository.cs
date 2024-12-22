using BlazorServerApp.Application.Interfaces;
using Grpc.Core;
using Items;
using System.Threading.Tasks;

namespace BlazorServerApp.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ItemService.ItemServiceClient _client;
        private readonly CustomAuthenticationStateProvider _authStateProvider;

        public ItemRepository(ItemService.ItemServiceClient client, CustomAuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
            _client = client;
        }

        private async Task<CallOptions> GetAuthenticatedCallOptionsAsync()
        {
            var token = await _authStateProvider.GetTokenAsync();
            Console.WriteLine($"Token: {token}");
            Console.WriteLine($"Authorization Header: Bearer {token}");

            var metadata = new Metadata();
            if (!string.IsNullOrEmpty(token))
            {
                metadata.Add("Authorization", $"Bearer {token}");
            }

            return new CallOptions(metadata);
        }

        public async Task<Item> CreateItemAsync(CreateItem itemDTO)
        {
            try
            {
                var callOptions = await GetAuthenticatedCallOptionsAsync();
                var response = await _client.createItemAsync(itemDTO, callOptions);
                return response;
            }
            catch (RpcException ex)
            {
                throw new ApplicationException("Error creating item", ex);
            }
        }

        public async Task DeleteItemAsync(DeleteItem itemToDelete)
        {
            try
            {
                var callOptions = await GetAuthenticatedCallOptionsAsync();
                await _client.deleteItemAsync(itemToDelete, callOptions);
            }
            catch (RpcException ex)
            {
                throw new ApplicationException($"Error deleting item with Id: {itemToDelete.ItemId}", ex);
            }
        }

        public async Task EditItemAsync(Item item)
        {
            try
            {
                var callOptions = await GetAuthenticatedCallOptionsAsync();
                await _client.editItemAsync(item, callOptions);
            }
            catch (RpcException ex)
            {
                throw new ApplicationException("Error editing item", ex);
            }
        }

        public async Task<IEnumerable<Item>> GetAllItemsAsync()
        {
            try
            {
                var callOptions = await GetAuthenticatedCallOptionsAsync();
                var response = await _client.getAllItemsAsync(new Google.Protobuf.WellKnownTypes.Empty(), callOptions);
                return response.Items.Select(item => new Item
                {
                    ItemId = item.ItemId,
                    ItemName = item.ItemName,
                    Description = item.Description,
                    QuantityInStore = item.QuantityInStore
                }).ToList();
            }
            catch (RpcException ex)
            {
                throw new ApplicationException("Error retrieving all items", ex);
            }
        }
    }
}
