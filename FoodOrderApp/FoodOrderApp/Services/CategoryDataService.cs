﻿using Firebase.Database;
using FoodOrderApp.Models;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderApp.Services
{
    public class CategoryDataService
    {
        FirebaseClient client;
        public CategoryDataService()
        {
            client = new FirebaseClient("https://foodorderapp-1fd52.firebaseio.com/");
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            var categories = (await client.Child("Categories")
                .OnceAsync<Category>())
                .Select(c => new Category {
                    CategoryID = c.Object.CategoryID,
                    CategoryName = c.Object.CategoryName,
                    CategoryPoster = c.Object.CategoryPoster,
                    ImageUrl = c.Object.ImageUrl
                }).ToList();
            return categories;
        }
    }
}
