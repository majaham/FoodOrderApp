using Firebase.Database;
using FoodOrderApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderApp.Services
{
    public class FoodItemDataService
    {
        private FirebaseClient client;
        public FoodItemDataService()
        {
            client = new FirebaseClient("https://foodorderapp-1fd52.firebaseio.com/");
        }
        public async Task<ObservableCollection<FoodItem>> GetFoodItemsAsync(string searchTerm = null)
        {
            var obsFoodItems = new ObservableCollection<FoodItem>();
            var fooditems = (await client.Child("FoodItems").OnceAsync<FoodItem>())
                .Select(f => ParseFoodItem(f)).ToList();
            var searchFoodItems = fooditems.Where(f => string.IsNullOrWhiteSpace(searchTerm) 
                                    || f.ProductName.ToLower().Contains(searchTerm)).ToList();
            foreach(var item in searchFoodItems)
            {
                obsFoodItems.Add(item);
            }
            return obsFoodItems;
        }
       
        public async Task<ObservableCollection<FoodItem>> GetFoodItemsByCategoryAsync(int category)
        {
            var categoryFoodItems = new ObservableCollection<FoodItem>();
            var foodItems = (await client.Child("FoodItems").OnceAsync<FoodItem>())
                .Where(f => f.Object.CategoryID == category);
            foreach(var foodItem in foodItems)
            {
                categoryFoodItems.Add(ParseFoodItem(foodItem));
            }
            return categoryFoodItems;
        }

        public async Task<ObservableCollection<FoodItem>> GetNewFoodItemsAsync(int count)
        {
            var newFoodItems = new ObservableCollection<FoodItem>();
            var foodItems = (await GetFoodItemsAsync()).OrderByDescending(f => f.ProductID).Take(count);
            foreach(var f in foodItems)
            {
                newFoodItems.Add(f);
            }
            return newFoodItems;
        }

        private FoodItem ParseFoodItem(FirebaseObject<FoodItem> f)
        {
            return new FoodItem
            {
                ProductID = f.Object.ProductID,
                ProductName = f.Object.ProductName,
                ImageUrl = f.Object.ImageUrl,
                CategoryID = f.Object.CategoryID,
                Rating = f.Object.Rating,
                Description = f.Object.Description,
                RatingDetail = f.Object.RatingDetail,
                HomeSelected = f.Object.HomeSelected,
                Price = f.Object.Price
            };
        }      

    }
}
