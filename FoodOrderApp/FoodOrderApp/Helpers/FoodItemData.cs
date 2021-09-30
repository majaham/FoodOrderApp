using Firebase.Database;
using Firebase.Database.Query;
using FoodOrderApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodOrderApp.Helpers
{
    public class FoodItemData
    {
        private FirebaseClient client;
        private List<FoodItem> FoodItems;
        public FoodItemData()
        {
            client = client = new FirebaseClient("https://foodorderapp-1fd52.firebaseio.com/");
            FoodItems = new List<FoodItem>
            {
                new FoodItem(){ProductID = 1, CategoryID=1, ImageUrl="MainBurger", ProductName="Burger and Pizza Hub 1",
                             Price=45, Description="Burger - Pizza - Breakfast", HomeSelected="CompleteHeart",
                             Rating="4.8", RatingDetail="(121 ratings)"},
                new FoodItem(){ProductID = 2, CategoryID=1, ImageUrl="MainBurger", ProductName="Burger and Pizza Hub 2",
                             Price=45, Description="Burger - Pizza - Breakfast", HomeSelected="EmptyHeart",
                             Rating="4.8", RatingDetail="(121 ratings)"},
                new FoodItem(){ProductID = 3, CategoryID=1, ImageUrl="MainBurger", ProductName="Burger and Pizza Hub 3",
                             Price=45, Description="Burger - Pizza - Breakfast", HomeSelected="CompleteHeart",
                             Rating="4.8", RatingDetail="(121 ratings)"},
                new FoodItem(){ProductID = 4, CategoryID=1, ImageUrl="MainBurger", ProductName="Burger and Pizza Hub 1",
                             Price=45, Description="Burger - Pizza - Breakfast", HomeSelected="EmptyHeart",
                             Rating="4.8", RatingDetail="(121 ratings)"},
                new FoodItem(){ProductID = 5, CategoryID=2, ImageUrl="MainPizza", ProductName="Pizza",
                             Price=45, Description="Pizza - Breakfast", HomeSelected="CompleteHeart",
                             Rating="4.4", RatingDetail="(120 ratings)"},
                new FoodItem(){ProductID = 6, CategoryID=2, ImageUrl="MainPizza", ProductName="Pizza Hub 2",
                             Price=45, Description="Pizza Hub 2 - Breakfast", HomeSelected="EmptyHeart",
                             Rating="4.8", RatingDetail="(156 ratings)"},
                new FoodItem(){ProductID = 7, CategoryID=3, ImageUrl="MainDessert", ProductName="Ice Creams",
                             Price=45, Description="Icecream - Breakfast", HomeSelected="CompleteHeart",
                             Rating="4.4", RatingDetail="(120 ratings)"},
                new FoodItem(){ProductID = 8, CategoryID=3, ImageUrl="MainBurger", ProductName="Cakes",
                             Price=45, Description="Cool Cake - Breakfast", HomeSelected="EmptyHeart",
                             Rating="4.8", RatingDetail="(156 ratings)"}
            };
        }
        public async Task AddFoodItemsAsync()
        {
            try
            {
                foreach(FoodItem foodItem in FoodItems)
                {
                    await client.Child("FoodItems").PostAsync(foodItem);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

    }
}
