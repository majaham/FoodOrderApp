using FoodOrderApp.Services;
using FoodOrderApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    public class LogoutViewModel: BaseViewModel
    {
		private int _UserCartItemCount;
		public int UserCartItemCount
		{
			get { return _UserCartItemCount; }
			set { 
				_UserCartItemCount = value;
				OnPropertyChanged();
			}
		}

		private bool _IsCartExists;
		public bool IsCartExists
		{
			get { return _IsCartExists; }
			set {
				_IsCartExists = value;
				OnPropertyChanged();
			}
		}

		public Command LogoutCommand { get; set; }
		public Command GoToCartCommand { get; set; }

		public LogoutViewModel()
		{
			UserCartItemCount = new CartItemService().GetUserCartCout();
			IsCartExists = (UserCartItemCount > 0);

			LogoutCommand = new Command(async () => await LogoutUserAsync());
			GoToCartCommand = new Command(async () => await GoToCartAsync());
		}

		private async Task GoToCartAsync()
		{
			await Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
		}

		private async Task LogoutUserAsync()
		{
			var cartItemService = new CartItemService();
			cartItemService.RemoveItemsFromCart();
			Preferences.Remove("Username");
			await Application.Current.MainPage.Navigation.PushModalAsync(new LoginView());
		}
	}
}
