using FoodOrderApp.Models;
using System.IO;
using System;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(FoodOrderApp.Droid.SQLiteAndroid))]
namespace FoodOrderApp.Droid
{   
    public class SQLiteAndroid : ISQLite
    {
        SQLiteConnection ISQLite.GetSQLConnection()
        {
            string dbname = "FoodOrder.db3";
            string directory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(directory, dbname);
            var conn = new SQLiteConnection(path);
            return conn;
        }
    }
}