using SEP3_T1_BlazorUI.Application.Interfaces;
using SEP3_T1_BlazorUI.Models;

namespace SEP3_T1_BlazorUI.Infrastructure.Repositories
{
    public class OrderRepository: IOrderRepository
    {
        private readonly List<Order> _orders;
        private int _nextOrderId = 1;

        public OrderRepository()
        {
            _orders = new List<Order>();
            InitializeMockData();
        }

        public IEnumerable<Order> GetAllOrders() => _orders;

        public void AddOrder(Order order)
        {
            order.OrderId = _nextOrderId++;
            _orders.Add(order);
        }

        private void InitializeMockData()
        {
            var orders = new List<Order>
{
    // New order #1 (Completed)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-1),
        Status = "Completed",
        EmployeeId = "101",
        TotalQuantity = 30,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 2, Name = "Coffee", QuantityOrdered = 8 },
            new OrderItem { ItemId = 4, Name = "Bread", QuantityOrdered = 6 },
            new OrderItem { ItemId = 3, Name = "Chocolate", QuantityOrdered = 4 },
            new OrderItem { ItemId = 5, Name = "Butter", QuantityOrdered = 4 },
            new OrderItem { ItemId = 7, Name = "Eggs", QuantityOrdered = 5 },
            new OrderItem { ItemId = 6, Name = "Cheese", QuantityOrdered = 3 }
        }
    },

    // New order #2 (Pending)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-2),
        Status = "Pending",
        EmployeeId = "102",
        TotalQuantity = 22,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 10, Name = "Orange Juice", QuantityOrdered = 4 },
            new OrderItem { ItemId = 12, Name = "Yogurt", QuantityOrdered = 6 },
            new OrderItem { ItemId = 9, Name = "Bananas", QuantityOrdered = 5 },
            new OrderItem { ItemId = 1, Name = "Milk", QuantityOrdered = 7 }
        }
    },

    // New order #3 (Rejected)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-3),
        Status = "Rejected",
        EmployeeId = "103",
        TotalQuantity = 20,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 4, Name = "Bread", QuantityOrdered = 6 },
            new OrderItem { ItemId = 7, Name = "Eggs", QuantityOrdered = 4 },
            new OrderItem { ItemId = 3, Name = "Chocolate", QuantityOrdered = 5 },
            new OrderItem { ItemId = 6, Name = "Cheese", QuantityOrdered = 5 }
        }
    },

    // New order #4 (Completed)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-4),
        Status = "Completed",
        EmployeeId = "104",
        TotalQuantity = 28,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 2, Name = "Coffee", QuantityOrdered = 10 },
            new OrderItem { ItemId = 1, Name = "Milk", QuantityOrdered = 8 },
            new OrderItem { ItemId = 12, Name = "Yogurt", QuantityOrdered = 4 },
            new OrderItem { ItemId = 8, Name = "Apples", QuantityOrdered = 6 }
        }
    },

    // New order #5 (Pending)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-5),
        Status = "Pending",
        EmployeeId = "105",
        TotalQuantity = 18,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 10, Name = "Orange Juice", QuantityOrdered = 5 },
            new OrderItem { ItemId = 11, Name = "Cereal", QuantityOrdered = 8 },
            new OrderItem { ItemId = 5, Name = "Butter", QuantityOrdered = 5 }
        }
    },

    // New order #6 (Completed)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-6),
        Status = "Completed",
        EmployeeId = "106",
        TotalQuantity = 35,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 9, Name = "Bananas", QuantityOrdered = 8 },
            new OrderItem { ItemId = 4, Name = "Bread", QuantityOrdered = 10 },
            new OrderItem { ItemId = 7, Name = "Eggs", QuantityOrdered = 6 },
            new OrderItem { ItemId = 1, Name = "Milk", QuantityOrdered = 6 },
            new OrderItem { ItemId = 12, Name = "Yogurt", QuantityOrdered = 5 }
        }
    },

    // New order #7 (Rejected)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-7),
        Status = "Rejected",
        EmployeeId = "107",
        TotalQuantity = 25,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 3, Name = "Chocolate", QuantityOrdered = 5 },
            new OrderItem { ItemId = 6, Name = "Cheese", QuantityOrdered = 5 },
            new OrderItem { ItemId = 5, Name = "Butter", QuantityOrdered = 5 },
            new OrderItem { ItemId = 8, Name = "Apples", QuantityOrdered = 5 },
            new OrderItem { ItemId = 2, Name = "Coffee", QuantityOrdered = 5 }
        }
    },

    // New order #8 (Completed)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-8),
        Status = "Completed",
        EmployeeId = "108",
        TotalQuantity = 50,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 2, Name = "Coffee", QuantityOrdered = 15 },
            new OrderItem { ItemId = 12, Name = "Yogurt", QuantityOrdered = 10 },
            new OrderItem { ItemId = 6, Name = "Cheese", QuantityOrdered = 8 },
            new OrderItem { ItemId = 4, Name = "Bread", QuantityOrdered = 8 },
            new OrderItem { ItemId = 5, Name = "Butter", QuantityOrdered = 9 }
        }
    },

    // New order #9 (Pending)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-9),
        Status = "Pending",
        EmployeeId = "109",
        TotalQuantity = 30,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 10, Name = "Orange Juice", QuantityOrdered = 8 },
            new OrderItem { ItemId = 3, Name = "Chocolate", QuantityOrdered = 7 },
            new OrderItem { ItemId = 11, Name = "Cereal", QuantityOrdered = 6 },
            new OrderItem { ItemId = 7, Name = "Eggs", QuantityOrdered = 6 },
            new OrderItem { ItemId = 1, Name = "Milk", QuantityOrdered = 3 }
        }
    },

    // New order #10 (Completed)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-10),
        Status = "Completed",
        EmployeeId = "110",
        TotalQuantity = 45,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 9, Name = "Bananas", QuantityOrdered = 12 },
            new OrderItem { ItemId = 7, Name = "Eggs", QuantityOrdered = 10 },
            new OrderItem { ItemId = 1, Name = "Milk", QuantityOrdered = 8 },
            new OrderItem { ItemId = 4, Name = "Bread", QuantityOrdered = 8 },
            new OrderItem { ItemId = 5, Name = "Butter", QuantityOrdered = 7 }
        }
    },

    // New order #11 (Rejected)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-11),
        Status = "Rejected",
        EmployeeId = "111",
        TotalQuantity = 40,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 3, Name = "Chocolate", QuantityOrdered = 10 },
            new OrderItem { ItemId = 8, Name = "Apples", QuantityOrdered = 10 },
            new OrderItem { ItemId = 6, Name = "Cheese", QuantityOrdered = 5 },
            new OrderItem { ItemId = 2, Name = "Coffee", QuantityOrdered = 10 },
            new OrderItem { ItemId = 5, Name = "Butter", QuantityOrdered = 5 }
        }
    },

    // New order #12 (Completed)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-12),
        Status = "Completed",
        EmployeeId = "112",
        TotalQuantity = 60,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 2, Name = "Coffee", QuantityOrdered = 15 },
            new OrderItem { ItemId = 1, Name = "Milk", QuantityOrdered = 10 },
            new OrderItem { ItemId = 4, Name = "Bread", QuantityOrdered = 10 },
            new OrderItem { ItemId = 7, Name = "Eggs", QuantityOrdered = 8 },
            new OrderItem { ItemId = 6, Name = "Cheese", QuantityOrdered = 8 },
            new OrderItem { ItemId = 12, Name = "Yogurt", QuantityOrdered = 9 }
        }
    },

    // New order #13 (Pending)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-13),
        Status = "Pending",
        EmployeeId = "113",
        TotalQuantity = 25,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 8, Name = "Apples", QuantityOrdered = 6 },
            new OrderItem { ItemId = 3, Name = "Chocolate", QuantityOrdered = 7 },
            new OrderItem { ItemId = 9, Name = "Bananas", QuantityOrdered = 5 },
            new OrderItem { ItemId = 7, Name = "Eggs", QuantityOrdered = 7 }
        }
    },

    // New order #14 (Completed)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-14),
        Status = "Completed",
        EmployeeId = "114",
        TotalQuantity = 50,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 4, Name = "Bread", QuantityOrdered = 10 },
            new OrderItem { ItemId = 12, Name = "Yogurt", QuantityOrdered = 8 },
            new OrderItem { ItemId = 6, Name = "Cheese", QuantityOrdered = 7 },
            new OrderItem { ItemId = 10, Name = "Orange Juice", QuantityOrdered = 6 },
            new OrderItem { ItemId = 5, Name = "Butter", QuantityOrdered = 6 },
            new OrderItem { ItemId = 7, Name = "Eggs", QuantityOrdered = 8 }
        }
    },

    // New order #15 (Pending)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-15),
        Status = "Pending",
        EmployeeId = "115",
        TotalQuantity = 37,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 6, Name = "Cheese", QuantityOrdered = 8 },
            new OrderItem { ItemId = 12, Name = "Yogurt", QuantityOrdered = 8 },
            new OrderItem { ItemId = 3, Name = "Chocolate", QuantityOrdered = 9 },
            new OrderItem { ItemId = 10, Name = "Orange Juice", QuantityOrdered = 7 },
            new OrderItem { ItemId = 2, Name = "Coffee", QuantityOrdered = 5 }
        }
    },

    // New order #16 (Completed)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-16),
        Status = "Completed",
        EmployeeId = "116",
        TotalQuantity = 42,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 1, Name = "Milk", QuantityOrdered = 10 },
            new OrderItem { ItemId = 9, Name = "Bananas", QuantityOrdered = 9 },
            new OrderItem { ItemId = 8, Name = "Apples", QuantityOrdered = 8 },
            new OrderItem { ItemId = 6, Name = "Cheese", QuantityOrdered = 7 },
            new OrderItem { ItemId = 4, Name = "Bread", QuantityOrdered = 8 }
        }
    },

    // New order #17 (Rejected)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-17),
        Status = "Rejected",
        EmployeeId = "117",
        TotalQuantity = 33,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 5, Name = "Butter", QuantityOrdered = 7 },
            new OrderItem { ItemId = 3, Name = "Chocolate", QuantityOrdered = 10 },
            new OrderItem { ItemId = 4, Name = "Bread", QuantityOrdered = 8 },
            new OrderItem { ItemId = 7, Name = "Eggs", QuantityOrdered = 8 }
        }
    },

    // New order #18 (Completed)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-18),
        Status = "Completed",
        EmployeeId = "118",
        TotalQuantity = 27,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 12, Name = "Yogurt", QuantityOrdered = 9 },
            new OrderItem { ItemId = 10, Name = "Orange Juice", QuantityOrdered = 6 },
            new OrderItem { ItemId = 7, Name = "Eggs", QuantityOrdered = 6 },
            new OrderItem { ItemId = 6, Name = "Cheese", QuantityOrdered = 6 }
        }
    },

    // New order #19 (Rejected)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-19),
        Status = "Rejected",
        EmployeeId = "119",
        TotalQuantity = 25,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 9, Name = "Bananas", QuantityOrdered = 5 },
            new OrderItem { ItemId = 8, Name = "Apples", QuantityOrdered = 5 },
            new OrderItem { ItemId = 4, Name = "Bread", QuantityOrdered = 5 },
            new OrderItem { ItemId = 2, Name = "Coffee", QuantityOrdered = 5 },
            new OrderItem { ItemId = 3, Name = "Chocolate", QuantityOrdered = 5 }
        }
    },

    // New order #20 (Completed)
    new Order
    {
        OrderId = _nextOrderId++,
        OrderDate = DateTime.Now.AddDays(-20),
        Status = "Completed",
        EmployeeId = "120",
        TotalQuantity = 50,
        OrderItems = new List<OrderItem>
        {
            new OrderItem { ItemId = 2, Name = "Coffee", QuantityOrdered = 15 },
            new OrderItem { ItemId = 1, Name = "Milk", QuantityOrdered = 10 },
            new OrderItem { ItemId = 7, Name = "Eggs", QuantityOrdered = 8 },
            new OrderItem { ItemId = 12, Name = "Yogurt", QuantityOrdered = 9 },
            new OrderItem { ItemId = 4, Name = "Bread", QuantityOrdered = 8 }
        }
    }
};
            ;


            foreach (var order in orders)
            {
                _orders.Add(order);
            }
        }
    }
}
