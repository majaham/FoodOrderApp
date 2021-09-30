using FoodOrderApp.Models;
using FoodOrderApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderApp.ViewModels
{
    public class SearchResultsViewModel: BaseViewModel
    {
		private int _TotalItems;
		public int TotalItems
		{
			get { return _TotalItems; }
			set {
				_TotalItems = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<FoodItem> FoodItems { get; set; }

		public SearchResultsViewModel(string searchTerm)
		{
			TotalItems = 0;
			FoodItems = new ObservableCollection<FoodItem>();
			LoadFoodItems(searchTerm);
		}

		private async void LoadFoodItems(string searchTerm)
		{
			var items = await new FoodItemDataService().GetFoodItemsAsync(searchTerm);
			FoodItems.Clear();
			foreach(var item in items)
			{
				FoodItems.Add(item);
			}
			TotalItems = items.Count;
		}
	}
}
