using AppShopping.iOS.Library.Helpers.WIFIConnect;
using AppShopping.Library.Helpers.WIFIConnect;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(WifiConnector))]

namespace AppShopping.iOS.Library.Helpers.WIFIConnect
{
    public class WifiConnector : IWifiConnector
    {
        public void ConnectToWifi(string ssid, string password)
        {
            throw new NotImplementedException();
        }
    }
}