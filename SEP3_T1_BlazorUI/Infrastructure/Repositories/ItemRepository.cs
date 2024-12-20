using SEP3_T1_BlazorUI.Application.Interfaces;
using SEP3_T1_BlazorUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SEP3_T1_BlazorUI.Infrastructure.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly List<Item> _items;

        public ItemRepository()
        {
            _items = new List<Item>();
            InitializeMockData();
        }

        public IEnumerable<Item> GetAllItems() => _items;

        public void AddItem(Item item)
        {
            item.Id = _items.Any() ? _items.Max(i => i.Id) + 1 : 1;
            _items.Add(item);
        }

        public void DeleteItem(Item item) => _items.Remove(item);

        private void InitializeMockData()
        {
            var items = new List<Item>
            {
                new Item { Id = 1, Name = "Milk", Description = "1 Gallon of Whole Milk", QuantityInStore = 20 },
                new Item { Id = 2, Name = "Coffee", Description = "Ground Coffee Beans, 1 lb", QuantityInStore = 15 },
                new Item { Id = 3, Name = "Chocolate", Description = "Dark Chocolate Bar, 70% Cocoa", QuantityInStore = 25 },
                new Item { Id = 4, Name = "Bread", Description = "Whole Wheat Bread Loaf", QuantityInStore = 30 },
                new Item { Id = 5, Name = "Butter", Description = "Salted Butter, 1 lb", QuantityInStore = 10 },
                new Item { Id = 6, Name = "Cheese", Description = "Cheddar Cheese, 1 lb", QuantityInStore = 12 },
                new Item { Id = 7, Name = "Eggs", Description = "Dozen Large Eggs", QuantityInStore = 18 },
                new Item { Id = 8, Name = "Apples", Description = "Red Apples, 1 lb", QuantityInStore = 22 },
                new Item { Id = 9, Name = "Bananas", Description = "Bananas, 1 lb", QuantityInStore = 25 },
                new Item { Id = 10, Name = "Orange Juice", Description = "1 Gallon of Orange Juice", QuantityInStore = 14 },
                new Item { Id = 11, Name = "Cereal", Description = "Oat Cereal, 500g", QuantityInStore = 16 },
                new Item { Id = 12, Name = "Yogurt", Description = "Greek Yogurt, 1 lb", QuantityInStore = 20 },
                new Item { Id = 13, Name = "Honey", Description = "Organic Honey, 12 oz", QuantityInStore = 8 },
                new Item { Id = 14, Name = "Tea", Description = "Green Tea Bags, 20 count", QuantityInStore = 25 },
                new Item { Id = 15, Name = "Sugar", Description = "Granulated Sugar, 2 lb", QuantityInStore = 30 },
                new Item { Id = 16, Name = "Rice", Description = "Basmati Rice, 1 kg", QuantityInStore = 50 },
                new Item { Id = 17, Name = "Pasta", Description = "Italian Spaghetti, 500g", QuantityInStore = 40 },
                new Item { Id = 18, Name = "Tomato Sauce", Description = "Tomato Sauce, 16 oz", QuantityInStore = 25 },
                new Item { Id = 19, Name = "Salt", Description = "Table Salt, 1 lb", QuantityInStore = 100 },
                new Item { Id = 20, Name = "Pepper", Description = "Ground Black Pepper, 4 oz", QuantityInStore = 35 },
                new Item { Id = 21, Name = "Olive Oil", Description = "Extra Virgin Olive Oil, 1L", QuantityInStore = 12 },
                new Item { Id = 22, Name = "Vinegar", Description = "Apple Cider Vinegar, 16 oz", QuantityInStore = 20 },
                new Item { Id = 23, Name = "Onions", Description = "Yellow Onions, 1 lb", QuantityInStore = 60 },
                new Item { Id = 24, Name = "Potatoes", Description = "Russet Potatoes, 1 lb", QuantityInStore = 70 },
                new Item { Id = 25, Name = "Carrots", Description = "Fresh Carrots, 1 lb", QuantityInStore = 50 },
                new Item { Id = 26, Name = "Chicken Breast", Description = "Boneless Chicken Breast, 1 lb", QuantityInStore = 25 },
                new Item { Id = 27, Name = "Beef", Description = "Ground Beef, 1 lb", QuantityInStore = 30 },
                new Item { Id = 28, Name = "Fish", Description = "Frozen Fish Fillet, 1 lb", QuantityInStore = 15 },
                new Item { Id = 29, Name = "Shrimp", Description = "Frozen Shrimp, 1 lb", QuantityInStore = 10 },
                new Item { Id = 30, Name = "Ice Cream", Description = "Vanilla Ice Cream, 1 Gallon", QuantityInStore = 20 },
            };

            foreach (var item in items)
            {
                item.OrderQuantity = item.QuantityInStore > 0 ? 1 : 0;
            }

            _items.AddRange(items);
            Console.WriteLine("Mock data initialized in ItemRepository.");
        }

        public void UpdateItem(Item item)
        {
            var existingItem = _items.FirstOrDefault(i => i.Id == item.Id);
            if (existingItem == null)
            {
                throw new InvalidOperationException("Item not found.");
            }

            existingItem.Name = item.Name;
            existingItem.Description = item.Description;
            existingItem.QuantityInStore = item.QuantityInStore;
        }
    }
}
