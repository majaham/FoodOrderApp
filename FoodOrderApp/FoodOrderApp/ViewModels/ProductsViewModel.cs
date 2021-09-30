using FoodOrderApp.Models;
using FoodOrderApp.Services;
using FoodOrderApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    public class ProductsViewModel: BaseViewModel
    {
		private string _Username;
		public string Username
		{
			get { return _Username; }
			set { 
				_Username = value;
				OnPropertyChanged();
			}
		}

		private int _UserCartItemsCount;
		public int UserCartItemsCount
		{
			get { return _UserCartItemsCount; }
			set { 
				_UserCartItemsCount = value;
				OnPropertyChanged();
			}
		}

		private string _SearchTerm;
		public string SearchTerm
		{
			get { return _SearchTerm; }
			set { 
				_SearchTerm = value;
				OnPropertyChanged();
			}
		}


		public ObservableCollection<Category> Categories { set; get; }
		public ObservableCollection<FoodItem> LatestItems { set; get; }

		public Command ViewCartCommand { get; set; }
		public Command LogoutCommand { get; set; }
		public Command OrderHistoryCommand { get; set; }
		public Command SearchCommand { get; set; }

		public ProductsViewModel()
		{
			string uname = Preferences.Get("Username", string.Empty);
			Username = (String.IsNullOrEmpty(uname)) ? "Guest" : uname;

			UserCartItemsCount = new CartItemService().GetUserCartCout();

			Categories = new ObservableCollection<Category>();
			LatestItems = new ObservableCollection<FoodItem>();

			GetCategories();
			GetLatestItems();

			ViewCartCommand = new Command(async () => await ViewCart());
			LogoutCommand = new Command(async () => await LogoutUser());
			OrderHistoryCommand = new Command(async () => await OrderHistoryAsync());
			SearchCommand = new Command(async () => await SearchFoodItemAsync());
		}

		private async Task SearchFoodItemAsync()
		{
			await Application.Current.MainPage.Navigation.PushModalAsync(new SearchResultsView(SearchTerm));
		}

		private async Task OrderHistoryAsync()
		{
			await Application.Current.MainPage.Navigation.PushModalAsync(new OrderHistoryView());
		}

		private async Task LogoutUser()
		{
			await Application.Current.MainPage.Navigation.PushModalAsync(new LogoutView());
		}

		private async Task ViewCart()
		{
			await Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
		}

		private async void GetLatestItems()
		{
			var data = await new FoodItemDataService().GetNewFoodItemsAsync(3);
			LatestItems.Clear();
			foreach(var item in data)
			{
				LatestItems.Add(item);
			}
		}

		private async void GetCategories()
		{

			var data = await new CategoryDataService().GetCategoriesAsync();
			Categories.Clear();
			foreach (var item in data)
			{
				Categories.Add(item);
			}
		}
	}
}
