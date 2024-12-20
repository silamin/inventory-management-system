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
                    var user1 = new User
                    {
                        UserName = "admin",
                        UserRole = UserRole.INVENTORY_MANAGER,
                        Password = passwordHasher.HashPassword(null, "admin"),
                    };

                    var user2 = new User
                    {
                        UserName = "worker",
                        UserRole = UserRole.WAREHOUSE_WORKER,
                        Password = passwordHasher.HashPassword(null, "worker")
                    };

                    context.Users.AddRange(user1, user2);

                    context.SaveChanges();

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
                                var orders = new List<Order>
                                {
                                    new Order
                                    {
                                        OrderStatus = OrderStatus.COMPLETED,
                                        DeliveryDate = DateTime.UtcNow.AddDays(-1),
                                        CreatedById = user1.UserId,
                                        UserId = user1.UserId,
                                        CreatedAt = DateTimeOffset.UtcNow,
                                        OrderItems = new List<OrderItem>
                                        {
                                            new OrderItem { ItemId = items[1].ItemId, TotalQuantity = 8 }, // Coffee
                                            new OrderItem { ItemId = items[3].ItemId, TotalQuantity = 6 }, // Bread
                                            new OrderItem { ItemId = items[2].ItemId, TotalQuantity = 4 }  // Chocolate
                                        }
                                    },
                                    new Order
                                    {
                                        OrderStatus = OrderStatus.COMPLETED,
                                        DeliveryDate = DateTime.UtcNow.AddDays(5),
                                        CreatedById = user2.UserId,
                                        CreatedBy = user2,
                                        UserId= null,
                                        AssignedUser = null,
                                        CreatedAt = DateTimeOffset.UtcNow,
                                        OrderItems = new List<OrderItem>
                                        {
                                            new OrderItem { ItemId = items[8].ItemId, TotalQuantity = 5 }, // Bananas
                                            new OrderItem { ItemId = items[0].ItemId, TotalQuantity = 7 }  // Milk
                                        }
                                    },
                                    new Order
                                    {
                                        OrderStatus = OrderStatus.COMPLETED,
                                        DeliveryDate = DateTime.UtcNow.AddDays(3),
                                        CreatedById = user1.UserId,
                                        CreatedBy= user1,
                                        UserId = user1.UserId,
                                        AssignedUser = user1,
                                        CreatedAt = DateTimeOffset.UtcNow,
                                        OrderItems = new List<OrderItem>
                                        {
                                            new OrderItem { ItemId = items[5].ItemId, TotalQuantity = 3 }, // Cheese
                                            new OrderItem { ItemId = items[4].ItemId, TotalQuantity = 2 }  // Butter
                                        }
                                    }
                                };
                                                    context.Orders.AddRange(orders);
                                context.SaveChanges();
                                                    // ----- Step 4: Insert Additional Orders and OrderItems -----
                                   var random = new Random(); // Ensuring a single Random instance for unique randomness
                                for (int i = 3; i < 20; i++) // Creating orders #4 to #20
                                {
                                    var order = new Order
                                    {
                                        OrderStatus = i % 3 == 0 ? OrderStatus.COMPLETED : OrderStatus.IN_PROGRESS,
                                        DeliveryDate = DateTime.UtcNow.AddDays(-i),
                                        CreatedById = (i % 2 == 0) ? user1.UserId : user2.UserId, // Assign a CreatedById from user1 or user2
                                        UserId = (i % 2 == 0) ? user1.UserId : user2.UserId, // Assign a UserId from user1 or user2
                                        CreatedAt = DateTimeOffset.UtcNow, // Timestamp for order creation
                                        OrderItems = new List<OrderItem>()
                                    };

                                    int numberOfItems = random.Next(3, 7); // Each order has between 3 to 6 items

                                    for (int j = 0; j < numberOfItems; j++)
                                    {
                                        var randomItem = items[random.Next(items.Count)]; // Pick a random item from the items list
                                        int quantity = random.Next(1, 11); // Quantity between 1 to 10

                                        // Avoid duplicate items in the same order
                                        if (!order.OrderItems.Any(oi => oi.ItemId == randomItem.ItemId))
                                        {
                                            order.OrderItems.Add(new OrderItem
                                            {
                                                ItemId = randomItem.ItemId,
                                                TotalQuantity = quantity
                                            });
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
