using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;


namespace FoodOrderApp.Helpers
{
    public class CrashesHelper
    {
        public async static Task TrackError(Exception ex, Dictionary<string, string> properties = null)
        {
            if (await Crashes.IsEnabledAsync())
                Crashes.TrackError(ex, properties);
        }
    }
}
