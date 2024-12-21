using BlazorServerApp.Application.Interfaces;
using Grpc.Core;
using Items;

namespace BlazorServerApp.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ItemService.ItemServiceClient _client;

        public ItemRepository(ItemService.ItemServiceClient client)
        {
            _client = client;
        }

        public async Task<Item> CreateItemAsync(CreateItem itemDTO)
        {
            try
            {
                var response = await _client.createItemAsync(itemDTO);

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

                // Step 2: Call the gRPC deleteItem method
                await _client.deleteItemAsync(itemToDelete);
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
                var response = await _client.editItemAsync(item);
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
                var response = await _client.getAllItemsAsync(new Google.Protobuf.WellKnownTypes.Empty());
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
