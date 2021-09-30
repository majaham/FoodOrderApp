using Firebase.Database;
using Firebase.Database.Query;
using FoodOrderApp.Models;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace FoodOrderApp.Services
{
    public class OrderHistoryService
    {
        FirebaseClient client;
        List<OrderHistory> UserOrders;
        public OrderHistoryService()
        {
            client = new FirebaseClient("https://foodorderapp-1fd52.firebaseio.com/");
            UserOrders = new List<OrderHistory>();
        }

        public async Task<List<OrderHistory>> GetOrderDetailsAsync()
        {
            string uname = Preferences.Get("Username", "Guest");
            var orders = (await client.Child("Orders").OnceAsync<Order>())
                .Where(o => o.Object.Username.Equals(uname))
                .Select(o => new Order
                {
                    OrderID = o.Object.OrderID,
                    TotalCost = o.Object.TotalCost,
                    ReceiptID = o.Object.ReceiptID
                }).ToList();
            foreach(var order in orders)
            {
                OrderHistory orderHistory = new OrderHistory();
                orderHistory.OrderID = order.OrderID;
                orderHistory.ReceiptID = order.ReceiptID;
                orderHistory.Username = order.Username;
                orderHistory.TotalCost = order.TotalCost;
                var orderDetails = (await client.Child("OrderDetails").OnceAsync<OrderDetails>())
                    .Where(od => od.Object.OrderID.Equals(order.OrderID))
                    .Select(od => new OrderDetails
                    {
                        OrderID = od.Object.OrderID,
                        OrderDetailsID = od.Object.OrderDetailsID,
                        ProductID = od.Object.ProductID,
                        Price = od.Object.Price,
                        ProductName = od.Object.ProductName,
                        Quantity = od.Object.Quantity
                    }).ToList();
                orderHistory.AddRange(orderDetails);
                UserOrders.Add(orderHistory);
            }
            return UserOrders;
        }
    }
}
