using FoodOrderApp.Models;
using FoodOrderApp.ViewModels;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FoodOrderApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResultsView : ContentPage
    {
        public SearchResultsView(string searchTerm)
        {
            InitializeComponent();
            LabelName.Text = "Welcome " + Preferences.Get("Username", "Guest");
            var searchItems = new SearchResultsViewModel(searchTerm);
            BindingContext = searchItems;
        }

        private async void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedProduct = e.CurrentSelection.FirstOrDefault() as FoodItem;
            if (selectedProduct == null)
                return;
            await Navigation.PushModalAsync(new ProductsDetailView(selectedProduct));
            ((CollectionView)sender).SelectedItem = null;
        }

        private async void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}