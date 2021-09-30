using FoodOrderApp.Models;
using FoodOrderApp.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    public class ProductsDetailViewModel:BaseViewModel
    {
		private FoodItem _SelectedFoodItem;
		public FoodItem SelectedFoodItem
		{
			get { return _SelectedFoodItem; }
			set { 
				_SelectedFoodItem = value;
				OnPropertyChanged();
			}
		}

		private int _TotalQuantity;
		public int TotalQuantity
		{
			get { return _TotalQuantity; }
			set { 
				
				this._TotalQuantity = value;
				if (this._TotalQuantity < 0)
					this._TotalQuantity = 0;
				if (this._TotalQuantity > 10)
					this._TotalQuantity = 10;
				OnPropertyChanged();
			}
		}

		public Command DecrementOrderCommand { get; set; }
		public Command IncrementOrderCommand { get; set; }
		public Command AddToCartCommand { get; set; }
		public Command ViewCartCommand { get; set; }
		public Command HomeCommand { get; set; }

		public ProductsDetailViewModel(FoodItem foodItem)
		{
			this.SelectedFoodItem = foodItem;
			this.TotalQuantity = 0;			

			DecrementOrderCommand = new Command( () => { this.TotalQuantity--; });
			IncrementOrderCommand = new Command( () => { this.TotalQuantity++; });
			AddToCartCommand = new Command( () => AddToCart());
			ViewCartCommand = new Command(async () => await ViewCartAsync());
			HomeCommand = new Command(async () => await GoToHome());
		}

		private async Task GoToHome()
		{
			await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
		}

		private async Task ViewCartAsync()
		{
			await Application.Current.MainPage.Navigation.PushModalAsync(new CartView());
		}

		private void AddToCart()
		{
			var conn = DependencyService.Get<ISQLite>().GetSQLConnection();
			try
			{
		
				CartItem cartItem = new CartItem()
				{
					ProductID = this.SelectedFoodItem.ProductID,
					ProductName = this.SelectedFoodItem.ProductName,
					Price = this.SelectedFoodItem.Price,
					Quantity = TotalQuantity
				};

				var item = conn.Table<CartItem>().ToList().FirstOrDefault(c => c.ProductID == this.SelectedFoodItem.ProductID);
				if (item == null)
					conn.Insert(cartItem);
				else
				{
					item.Quantity += TotalQuantity;
					conn.Update(item);
				}
				conn.Commit();
				conn.Close();
				Application.Current.MainPage.DisplayAlert("Success", "Selected Item added to cart", "OK");
			}
			catch(Exception ex)
			{
				Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
			}
			finally
			{
				conn.Close();
			}
		}		
	}
}
