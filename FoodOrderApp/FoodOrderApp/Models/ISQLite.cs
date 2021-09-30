using SQLite;

namespace FoodOrderApp.Models
{
    public interface ISQLite
    {
        SQLiteConnection GetSQLConnection();
    }
}
