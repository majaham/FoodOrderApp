using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using FoodOrderApp.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace FoodOrderApp
{
    public partial class App : Application
    {
        public App()
        {
            Xamarin.Forms.Device.SetFlags(new string[] { 
                "MediaElement_Experimental", 
                "AppTheme_Experimental"
            });
            InitializeComponent();

            Crashes.SetEnabledAsync(true);
            Analytics.SetEnabledAsync(true);

            //MainPage = new NavigationPage(new LoginView());
            //MainPage = new NavigationPage(new SettingsPage());
            //MainPage = new NavigationPage(new ProductsView());

            string uname = Preferences.Get("Username", string.Empty);
            if (String.IsNullOrEmpty(uname))
            {
                MainPage = new LoginView();
            }
            else
            {
                MainPage = new ProductsView();
            }
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=292f722c-935b-42e1-a1b4-ee4e299a892d;",
                 typeof(Analytics), typeof(Crashes));
            var cartTable = new Helpers.CreateCartItemTable();
            if (!cartTable.IsCartItemExists())
            {
                cartTable.CreateTable();
            }
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
   
}
