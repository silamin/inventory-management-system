using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace SEP3_T3_ASP_Core_WebAPI.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context, IPasswordHasher<User> passwordHasher, bool clearDatabase, bool seedData)
        {
            // Begin a transaction to ensure data integrity
            using var transaction = context.Database.BeginTransaction();

            try
            {
                // Clear existing data if ClearDatabase is true
                if (clearDatabase)
                {
                    context.OrderItems.RemoveRange(context.OrderItems);
                    context.Orders.RemoveRange(context.Orders);
                    context.Items.RemoveRange(context.Items);
                    context.Users.RemoveRange(context.Users);
                    context.SaveChanges();
                }

                // Insert mock data if SeedData is true
                if (seedData)
                {
                    // ----- Step 1: Create Users -----
                    var users = new List<User>();

                    // Generate 15 Inventory Managers
                    for (int i = 1; i <= 15; i++)
                    {
                        users.Add(new User
                        {
                            UserName = $"manager{i}",
                            UserRole = UserRole.INVENTORY_MANAGER,
                            Password = passwordHasher.HashPassword(null, $"manager{i}password")
                        });
                    }

                    // Generate 15 Warehouse Workers
                    for (int i = 1; i <= 15; i++)
                    {
                        users.Add(new User
                        {
                            UserName = $"worker{i}",
                            UserRole = UserRole.WAREHOUSE_WORKER,
                            Password = passwordHasher.HashPassword(null, $"worker{i}password")
                        });
                    }

                    context.Users.AddRange(users);
                    context.SaveChanges();

                    // Retrieve specific users for use in orders
                    var user1 = context.Users.First(u => u.UserName == "manager1");
                    var user2 = context.Users.First(u => u.UserName == "worker1");

                    // ----- Step 2: Insert Items -----
                    var items = new List<Item>
                    {
                        new Item { ItemName = "Milk", Description = "1 Gallon of Whole Milk", QuantityInStore = 20 },
                        new Item { ItemName = "Coffee", Description = "Ground Coffee Beans, 1 lb", QuantityInStore = 15 },
                        new Item { ItemName = "Chocolate", Description = "Dark Chocolate Bar, 70% Cocoa", QuantityInStore = 25 },
                        new Item { ItemName = "Bread", Description = "Whole Wheat Bread Loaf", QuantityInStore = 30 },
                        new Item { ItemName = "Butter", Description = "Salted Butter, 1 lb", QuantityInStore = 10 },
                        new Item { ItemName = "Cheese", Description = "Cheddar Cheese, 1 lb", QuantityInStore = 12 },
                        new Item { ItemName = "Eggs", Description = "Dozen Large Eggs", QuantityInStore = 18 },
                        new Item { ItemName = "Apples", Description = "Red Apples, 1 lb", QuantityInStore = 22 },
                        new Item { ItemName = "Bananas", Description = "Bananas, 1 lb", QuantityInStore = 25 },
                        new Item { ItemName = "Orange Juice", Description = "1 Gallon of Orange Juice", QuantityInStore = 14 },
                        new Item { ItemName = "Cereal", Description = "Oat Cereal, 500g", QuantityInStore = 16 },
                        new Item { ItemName = "Yogurt", Description = "Greek Yogurt, 1 lb", QuantityInStore = 20 },
                        new Item { ItemName = "Honey", Description = "Organic Honey, 12 oz", QuantityInStore = 8 }
                    };

                    context.Items.AddRange(items);
                    context.SaveChanges();

                    // ----- Step 3: Insert Orders -----
                    var orders = new List<Order>();

                    context.Orders.AddRange(orders);
                    context.SaveChanges();

                    // ----- Step 4: Insert Additional Orders and OrderItems -----
                    var random = new Random();
                    for (int i = 4; i <= 20; i++)
                    {
                        var order = new Order
                        {
                            OrderStatus = i % 3 == 0 ? OrderStatus.COMPLETED : OrderStatus.IN_PROGRESS,
                            DeliveryDate = DateTime.UtcNow.AddDays(-i),
                            CreatedById = i % 2 == 0 ? user1.UserId : user2.UserId,
                            UserId = i % 2 == 0 ? user1.UserId : user2.UserId,
                            CreatedAt = DateTimeOffset.UtcNow,
                            OrderItems = new List<OrderItem>()
                        };

                        int numberOfItems = random.Next(3, 7);

                        for (int j = 0; j < numberOfItems; j++)
                        {
                            var randomItem = items[random.Next(items.Count)];
                            int totalQuantity = random.Next(1, 11);

                            if (!order.OrderItems.Any(oi => oi.ItemId == randomItem.ItemId))
                            {
                                var orderItem = new OrderItem
                                {
                                    ItemId = randomItem.ItemId,
                                    TotalQuantity = totalQuantity
                                };

                                if (order.OrderStatus == OrderStatus.IN_PROGRESS)
                                {
                                    orderItem.QuantityToPick = random.Next(1, totalQuantity);
                                }

                                order.OrderItems.Add(orderItem);
                            }
                        }

                        orders.Add(order);
                    }

                    context.Orders.AddRange(orders.GetRange(3, orders.Count - 3));
                    context.SaveChanges();
                }

                // Commit the transaction
                transaction.Commit();
            }
            catch (Exception ex)
            {
                // Rollback the transaction if any error occurs
                transaction.Rollback();
                throw new Exception("An error occurred while seeding the database.", ex);
            }
        }
    }
}
