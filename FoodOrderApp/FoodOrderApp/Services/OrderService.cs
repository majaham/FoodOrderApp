using Firebase.Database;
using Firebase.Database.Query;
using FoodOrderApp.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodOrderApp.Services
{
    public class OrderService
    {
        private FirebaseClient client;
        public OrderService()
        {
            client = new FirebaseClient("https://foodorderapp-1fd52.firebaseio.com/");
        }
        public async Task<string> PlaceOrderAsync()
        {
            var conn = DependencyService.Get<ISQLite>().GetSQLConnection();
            var data = conn.Table<CartItem>().ToList();
            conn.Close();
            if (data.Count == 0)
                return string.Empty;
            string orderID = Guid.NewGuid().ToString();
            string username = Preferences.Get("Username", "Guest");
            decimal totalPrice = 0;
            foreach(var item in data)
            {
                OrderDetails orderDetails = new OrderDetails
                {
                    OrderDetailsID = Guid.NewGuid().ToString(),
                    OrderID = orderID,
                    ProductID = item.ProductID,
                    ProductName = item.ProductName,
                    Quantity = item.Quantity,
                    Price = item.Price
                };
                totalPrice += (item.Price * item.Quantity);
                await client.Child("OrderDetails").PostAsync<OrderDetails>(orderDetails);
            }
            await client.Child("Orders").PostAsync<Order>(new Order { OrderID = orderID, Username = username, TotalCost = totalPrice });
            return orderID;
        }
    }
}
