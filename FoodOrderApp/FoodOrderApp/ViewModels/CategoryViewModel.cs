using FoodOrderApp.Models;
using FoodOrderApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace FoodOrderApp.ViewModels
{
    public class CategoryViewModel:BaseViewModel
    {
		private Category _SelectedCategory;
		public Category SelectedCategory
		{
			get { return _SelectedCategory; }
			set {
				_SelectedCategory = value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<FoodItem> FoodItemsByCategory { set; get; }

		private int _TotalFoodItems;
		public int TotalFoodItems
		{
			get { return _TotalFoodItems; }
			set { 
				_TotalFoodItems = value;
				OnPropertyChanged();
			}
		}

		public CategoryViewModel(Category category)
		{
			FoodItemsByCategory = new ObservableCollection<FoodItem>();
			SelectedCategory = category;
			GetFoodItemsByCategory(category.CategoryID);
		}

		private async void GetFoodItemsByCategory(int categoryID)
		{
			var data = await new FoodItemDataService().GetFoodItemsByCategoryAsync(categoryID);
			FoodItemsByCategory.Clear();
			foreach(var item in data)
			{
				FoodItemsByCategory.Add(item);
			}
			TotalFoodItems = FoodItemsByCategory.Count;
		}
	}
}
