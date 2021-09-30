using Firebase.Database;
using Firebase.Database.Query;
using FoodOrderApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodOrderApp.Helpers
{
    public class CategoryData
    {
        FirebaseClient client;
        public List<Category>  Categories { get; set; }
        
        public CategoryData()
        {
            client = new FirebaseClient("https://foodorderapp-1fd52.firebaseio.com/");

            Categories = new List<Category>()
            {
                new Category(){CategoryID=1, CategoryName="Burger", CategoryPoster="MainBurger", ImageUrl="Burger.png"},
                new Category(){CategoryID=2, CategoryName="Pizza", CategoryPoster="MainPizza", ImageUrl="Pizza.png"},
                new Category(){CategoryID=3, CategoryName="Dessert", CategoryPoster="MainDessert.png", ImageUrl="Dessert.png"},
                new Category(){CategoryID=4, CategoryName="Veg Burger", CategoryPoster="MainBurger", ImageUrl="Burger.png"},
                new Category(){CategoryID=5, CategoryName="Veg Pizza", CategoryPoster="MainPizza.png", ImageUrl="Pizza.png"},
                new Category(){CategoryID=6, CategoryName="Cakes", CategoryPoster="MainDessert.png", ImageUrl="Dessert.png"}
            };
        }
        public async Task AddCategoriesAsync()
        {
            try
            {
                foreach (Category category in Categories)
                {
                    await client.Child("Categories").PostAsync(category);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }          
        }
    }
}
