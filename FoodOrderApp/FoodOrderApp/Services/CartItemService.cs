using FoodOrderApp.Models;
using Xamarin.Forms;

namespace FoodOrderApp.Services
{
    public class CartItemService
    {
        public int GetUserCartCout()
        {
            var conn = DependencyService.Get<ISQLite>().GetSQLConnection();
            int count = conn.Table<CartItem>().Count();
            conn.Close();
            return count;
        }
        public void RemoveItemsFromCart()
        {
            var conn = DependencyService.Get<ISQLite>().GetSQLConnection();
            conn.DeleteAll<CartItem>();
            conn.Commit();
            conn.Close();
        }
    }
}
