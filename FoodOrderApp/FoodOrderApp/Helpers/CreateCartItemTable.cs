using FoodOrderApp.Models;
using System;
using Xamarin.Forms;

namespace FoodOrderApp.Helpers
{
    public class CreateCartItemTable
    {
        public bool CreateTable()
        {
            try
            {
                var conn = DependencyService.Get<ISQLite>().GetSQLConnection();
                conn.CreateTable<CartItem>();               
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
           
        }
        public bool IsCartItemExists() {
            int result;
            SQLite.SQLiteConnection conn = null;
            try
            {
                conn = DependencyService.Get<ISQLite>().GetSQLConnection();
                result = conn.GetTableInfo("CartItem").Count;
            }
            finally
            {
                conn.Close();
            }         
           
            return (result > 0);
        }
    }
}
