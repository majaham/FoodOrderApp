using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderApp.Models
{
    public class OrderHistory: List<OrderDetails>
    {
        public string OrderID { get; set; }
        public string Username { get; set; }
        public decimal TotalCost { get; set; }
        public string ReceiptID { get; set; }
    }
}
