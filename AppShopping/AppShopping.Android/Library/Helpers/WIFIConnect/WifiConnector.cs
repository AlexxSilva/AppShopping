using Android.App;
using Android.Content;
using Android.Net.Wifi;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppShopping.Droid.Library.Helpers.WIFIConnect;
using AppShopping.Library.Helpers.WIFIConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(WifiConnector))]

namespace AppShopping.Droid.Library.Helpers.WIFIConnect
{
    public class WifiConnector : IWifiConnector
    {
        public void ConnectToWifi(string ssid, string password)
        {
            var context = Android.App.Application.Context;
            var wifiManager = (WifiManager)context.GetSystemService(Context.WifiService);

            var formattedSsid = $"\"{ssid}\"";
            var formattedPassword = $"\"{password}\"";

            var wifiConfig = new WifiConfiguration
            {
                Ssid = formattedSsid,
                PreSharedKey = formattedPassword
            };

            var addNetwork = wifiManager.AddNetwork(wifiConfig);


            var network = wifiManager.ConfiguredNetworks
                 .FirstOrDefault(n => n.Ssid == $"\"{ssid}\"");

            if (network == null)
            {
                 throw new Exception($"Não foi possivel encontrar a rede : {ssid}");
            }

            wifiManager.Disconnect();
            var enableNetwork = wifiManager.EnableNetwork(network.NetworkId, true);

            wifiManager.Reconnect();
        }
    }
}