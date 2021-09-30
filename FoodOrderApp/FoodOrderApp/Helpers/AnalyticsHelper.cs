using Microsoft.AppCenter.Analytics;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FoodOrderApp.Helpers
{
    public class AnalyticsHelper
    {
        public async static Task TrackEvent(string eventName, Dictionary<string, string> properties = null)
        {
            if (await Analytics.IsEnabledAsync())
                Analytics.TrackEvent(eventName, properties);
        }
    }
}
