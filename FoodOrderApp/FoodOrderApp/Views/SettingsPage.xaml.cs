using FoodOrderApp.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodOrderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : ContentPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private async void ManageProducts_Clicked(object sender, EventArgs e)
        {
            var foodItemData = new FoodItemData();
            await foodItemData.AddFoodItemsAsync();
        }

        private async void ManageCategories_Clicked(object sender, EventArgs e)
        {
            var categoryData = new CategoryData();
            await categoryData.AddCategoriesAsync();
        }

        private void ManageCart_Clicked(object sender, EventArgs e)
        {
            var cartTable = new CreateCartItemTable();
            if (cartTable.CreateTable())
            {
                DisplayAlert("Success", "CartItem table created!", "OK");
            }
            else
            {
                DisplayAlert("Error", "CartItem table error", "OK");
            }
        }
    }
}