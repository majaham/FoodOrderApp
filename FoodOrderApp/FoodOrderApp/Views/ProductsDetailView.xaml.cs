using FoodOrderApp.Models;
using FoodOrderApp.ViewModels;
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
    public partial class ProductsDetailView : ContentPage
    {
        private ProductsDetailViewModel pvm;
        public ProductsDetailView(FoodItem foodItem)
        {
            InitializeComponent();

            pvm = new ProductsDetailViewModel(foodItem);

            this.BindingContext = pvm;
        }

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}