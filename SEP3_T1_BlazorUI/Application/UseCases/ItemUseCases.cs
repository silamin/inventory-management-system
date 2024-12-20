using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SEP3_T1_BlazorUI.Application.Interfaces;
using SEP3_T1_BlazorUI.Models;

namespace SEP3_T1_BlazorUI.Application.UseCases
{
    public class ItemUseCases
    {
        private readonly IItemRepository _itemRepository;

        public ItemUseCases(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task AddItemAsync(Item item)
        {
            await Task.Delay(1000); // Simulate network delay
            _itemRepository.AddItem(item);
        }

        public async Task<Item> FindItemByNameAndDescriptionAsync(string name, string description)
        {
            await Task.Delay(500); // Simulate network delay
            return _itemRepository.GetAllItems().FirstOrDefault(i => i.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase)
                                                && i.Description.Equals(description, System.StringComparison.OrdinalIgnoreCase));
        }

        public void DeleteItem(Item item)
        {
            _itemRepository.DeleteItem(item);
        }

        public void UpdateItem(Item item)
        {
            _itemRepository.UpdateItem(item);
        }

        public IEnumerable<Item> GetAllItems()
        {
            return _itemRepository.GetAllItems();
        }
    }
}
