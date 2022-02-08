using System;
using System.Collections.Generic;
using System.Text;

namespace AppShopping.Library.Helpers.WIFIConnect
{
    public interface IWifiConnector
    {
        void ConnectToWifi(string ssid, string password);
    }
}
