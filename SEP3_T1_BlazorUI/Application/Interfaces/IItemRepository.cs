using SEP3_T1_BlazorUI.Models;

namespace SEP3_T1_BlazorUI.Application.Interfaces
{
    public interface IItemRepository
    {
        IEnumerable<Item> GetAllItems();
        void AddItem(Item item);
        void DeleteItem(Item item);
        void UpdateItem(Item item);
    }
}
