using Firebase.Database;
using Firebase.Database.Query;
using FoodOrderApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FoodOrderApp.Services
{
    public class UserService
    {
        private FirebaseClient client;
        public UserService()
        {
            client = new FirebaseClient("https://foodorderapp-1fd52.firebaseio.com/");
        }

        public async Task<bool> IsUserExists(string uname)
        {
            var user = (await client.Child("Users").OnceAsync<User>())
                .Where(u => u.Object.username.Trim() == uname.Trim())
                .FirstOrDefault();

            return (user != null);
        }

        public async Task<bool> RegisterUser(string uname, string pword)
        {
            uname = uname.Trim();
            pword = pword.Trim();
            if(await IsUserExists(uname) == false)
            {
                await client.Child("Users").PostAsync(new User() {username = uname, password = pword });
                return true;
            }
            return false;
        }

        public async Task<bool> LoginUser(string uname, string pword)
        {
            uname = uname.Trim();
            pword = pword.Trim();
            var user = (await client.Child("Users").OnceAsync<User>())
                .Where(u => u.Object.username == uname && u.Object.password == pword)
                .FirstOrDefault();
            return (user != null);
        }
    }
}
