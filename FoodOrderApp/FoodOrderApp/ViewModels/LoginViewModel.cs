using FoodOrderApp.Helpers;
using FoodOrderApp.Services;
using FoodOrderApp.Views;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FoodOrderApp.ViewModels
{
    public class LoginViewModel: BaseViewModel
    {
        private string _username;
        public string Username
        {
            set
            {
                this._username = value;
                OnPropertyChanged();
            }
            get
            {
                return this._username;
            }
        }

        private string _password;
        public string Password
        {
            set
            {
                this._password = value;
                OnPropertyChanged();
            }
            get
            {
                return this._password;
            }
        }

        private bool _isBusy;
        public bool IsBusy
        {
            set
            {
                this._isBusy = value;
                OnPropertyChanged();
            }
            get
            {
                return this._isBusy;
            }
        }

        private bool _result;
        public bool Result
        {
            set
            {
                this._result = value;
                OnPropertyChanged();
            }
            get
            {
                return this._result;
            }
        }

        private bool _IsEnabled;

        public bool IsEnabled
        {
            get { return _IsEnabled; }
            set { 

                _IsEnabled = value;
                OnPropertyChanged();
            }
        }


        public Command LoginCommand { get; set; }
        public Command RegisterCommand { get; set; }

        private UserService service;

        public LoginViewModel()
        {
            IsEnabled = false;
            service = new UserService();
            LoginCommand = new Command( async () => await LoginCommandAsync());
            RegisterCommand = new Command(async () => await RegisterCommandAsync());
        }

        private async Task RegisterCommandAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                await AnalyticsHelper.TrackEvent($"Register command executing for user {Username}");
                Result = await service.RegisterUser(this.Username, this.Password);
                //var cartTable = new Helpers.CreateCartItemTable();

                if (Result)
                {
                    //cartTable.CreateTable();
                    await Application.Current.MainPage.DisplayAlert("Register", "Registration Successful", "OK");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "User already exists. Try different credentials", "OK");
                }
            }catch(Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                var properties = new Dictionary<string, string> { { "username", Username } };
                await CrashesHelper.TrackError(ex, properties);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoginCommandAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                await AnalyticsHelper.TrackEvent($"Login command executing for user {Username}");
                Result = await service.LoginUser(this.Username, this.Password);
                if (Result)
                {
                    Preferences.Set("Username", this.Username);
                    await Application.Current.MainPage.Navigation.PushModalAsync(new ProductsView());
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Error", "Invalid username or password", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                var properties = new Dictionary<string, string> { { "username", Username } };
                await CrashesHelper.TrackError(ex, properties);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
