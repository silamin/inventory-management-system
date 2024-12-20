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
        public IEnumerable<Item> GetAllItems()
        {
            try
            {
                var response = _client.getAllItems(new Google.Protobuf.WellKnownTypes.Empty());

                var items = response.Items.ToList();

                return items;
            }
            catch (RpcException ex)
            {
                throw new ApplicationException("Error retrieving all items", ex);
            }
        }




    }
}
