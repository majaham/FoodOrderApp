using FoodOrderApp.Models;
using FoodOrderApp.Services;
using FoodOrderApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    public class CartViewModel: BaseViewModel
    {
        public ObservableCollection<UserCartItem> CartItems { get; set; }

		private decimal _TotalCost;
		public decimal TotalCost
		{
			get { return _TotalCost; }
			set { 
				_TotalCost = value;
				OnPropertyChanged();
			}
		}

		public Command PlaceOrderCommand { get; set; }

		public CartViewModel()
		{
			TotalCost = 0;
			CartItems = new ObservableCollection<UserCartItem>();
			LoadCartItems();
			PlaceOrderCommand = new Command(async () => await PlaceOrderAsync());
		}
		private async Task PlaceOrderAsync()
		{
			//await Application.Current.MainPage.DisplayAlert("Orders", TotalCost.ToString(), "OK");
			string orderId = await new OrderService().PlaceOrderAsync();
			RemoveCartItems();
			if (!string.IsNullOrEmpty(orderId))
			{
				await Application.Current.MainPage.Navigation.PushModalAsync(new OrdersView(orderId));
			}
			else
			{
				await Application.Current.MainPage.DisplayAlert("Order", "No order items selected", "OK");
			}
			
		}

		private void RemoveCartItems()
		{
			var cartItemService = new CartItemService();
			cartItemService.RemoveItemsFromCart();
			this.CartItems.Clear();
		}

		private void LoadCartItems()
		{
			var conn = DependencyService.Get<ISQLite>().GetSQLConnection();
			var data = conn.Table<CartItem>().ToList();
			conn.Close();
			CartItems.Clear();
			foreach(var item in data)
			{
				CartItems.Add(new UserCartItem {
					CartItemID = item.CartItemID,
					ProductID = item.ProductID,
					ProductName = item.ProductName,
					Quantity = item.Quantity,
					Price = item.Price,
					Cost = item.Quantity * item.Price
				});
				TotalCost += (item.Price * item.Quantity);
			}
		}
	}
}
