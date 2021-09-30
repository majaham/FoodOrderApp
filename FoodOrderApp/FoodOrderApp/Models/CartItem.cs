using SQLite;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace FoodOrderApp.Models
{
    [Table("CartItem")]
    public class CartItem
    {
        [AutoIncrement, PrimaryKey]
        public int CartItemID { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
