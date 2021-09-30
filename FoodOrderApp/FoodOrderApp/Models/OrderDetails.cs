using System;
using System.Collections.Generic;
using System.Text;

namespace FoodOrderApp.Models
{
    public class OrderDetails
    {
        public string OrderDetailsID { get; set; }
        public string OrderID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
