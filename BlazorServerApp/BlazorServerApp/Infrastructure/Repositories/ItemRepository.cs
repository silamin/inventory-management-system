using BlazorServerApp.Application.Interfaces;
using Grpc.Core;

namespace BlazorServerApp.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly ItemService.ItemServiceClient _client;

        public ItemRepository(ItemService.ItemServiceClient client)
        {
            _client = client;
        }

        public async Task<Item> CreateItemAsync(ItemDTO itemDTO)
        {
            try
            {
                Console.WriteLine("Sending CreateItem request with data: ");
                Console.WriteLine($"ItemName: {itemDTO.Name}, Description: {itemDTO.Description}, QuantityInStore: {itemDTO.QuantityInStore}");

                var response = await _client.createItemAsync(itemDTO);

                // Log the full response
                Console.WriteLine("Response from gRPC (CreateItem): " + response.ToString());

                // Log each field of the response
                Console.WriteLine($"Item Created - Id: {response.ItemId}, Name: {response.ItemName}, Description: {response.Description}, QuantityInStore: {response.QuantityInStore}");

                return response;
            }
            catch (RpcException ex)
            {
                // Handle gRPC exception appropriately (logging, rethrow, etc.)
                Console.WriteLine("Error during CreateItem request. RPC Exception: " + ex.ToString());
                throw new ApplicationException("Error creating item", ex);
            }
        }

        public async Task DeleteItemAsync(Item item)
        {
            try
            {
                Console.WriteLine("Sending DeleteItem request for ItemId: " + item.ItemId);

                await _client.deleteItemAsync(item);

                // Log success message
                Console.WriteLine($"Successfully deleted item with Id: {item.ItemId}");
            }
            catch (RpcException ex)
            {
                // Handle gRPC exception appropriately (logging, rethrow, etc.)
                Console.WriteLine("Error during DeleteItem request. RPC Exception: " + ex.ToString());
                throw new ApplicationException("Error deleting item", ex);
            }
        }

        public async Task EditItemAsync(Item item)
        {
            try
            {
                Console.WriteLine("Sending EditItem request for ItemId: " + item.ItemId);
                Console.WriteLine($"Updated Values - Name: {item.ItemName}, Description: {item.Description}, QuantityInStore: {item.QuantityInStore}");

                var response = await _client.editItemAsync(item);

                // Log success message
                Console.WriteLine($"Successfully edited item with Id: {item.ItemId}");
            }
            catch (RpcException ex)
            {
                // Handle gRPC exception appropriately (logging, rethrow, etc.)
                Console.WriteLine("Error during EditItem request. RPC Exception: " + ex.ToString());
                throw new ApplicationException("Error editing item", ex);
            }
        }
        public IEnumerable<Item> GetAllItems()
        {
            try
            {
                Console.WriteLine("Sending GetAllItems request...");

                // Call the gRPC service synchronously
                var response = _client.getAllItems(new Google.Protobuf.WellKnownTypes.Empty());

                // Convert RepeatedField<Item> to IEnumerable<Item>
                var items = response.Items.ToList();

                // Log each item
                Console.WriteLine($"Received {items.Count} items from GetAllItems request.");
                foreach (var item in items)
                {
                    Console.WriteLine($"ItemId: {item.ItemId}, ItemName: {item.ItemName}, Description: {item.Description}, QuantityInStore: {item.QuantityInStore}");
                }

                // Return the list of items (since it's already an IEnumerable)
                return items;
            }
            catch (RpcException ex)
            {
                Console.WriteLine("Error during GetAllItems request. RPC Exception: " + ex.ToString());
                throw new ApplicationException("Error retrieving all items", ex);
            }
        }




    }
}
