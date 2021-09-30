using FoodOrderApp.Models;
using FoodOrderApp.Services;
using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    public class OrderHistoryViewModel: BaseViewModel
    {
        public ObservableCollection<OrderHistory> OrderDetails { get; set; }

        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { 
                _IsBusy = value;
                OnPropertyChanged();
            }
        }

        public OrderHistoryViewModel()
        {
            OrderDetails = new ObservableCollection<OrderHistory>();
            LoadUserOrders();
        }

        private async void LoadUserOrders()
        {
            try
            {
                IsBusy = true;
                var service = new OrderHistoryService();
                var data = await service.GetOrderDetailsAsync();
                OrderDetails.Clear();
                foreach(var order in data)
                {
                    OrderDetails.Add(order);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");               
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
