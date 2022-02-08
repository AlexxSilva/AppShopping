using AppShopping.Library.Helpers.MVVM;
using AppShopping.Library.Helpers.WIFIConnect;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Windows.Input;

namespace AppShopping.ViewModels
{
    public class WIFIViewModel : BaseViewModel
    {

        private string _message;

        public string Message
        {
            get { return _message; }
            set { _message = value;
                SetProperty(ref _message, value);
            }
        }


        private string _email;

        public string Email
        {
            get { return _email; }
            set { _email = value; }
        }

        public ICommand ConnectToWifiCommand { get; set; }

        public WIFIViewModel()
        {
            ConnectToWifiCommand = new Command(ConnectToWifi);
        }

        private void ConnectToWifi()
        {
            try
            {
                var wifiConnector = Xamarin.Forms.DependencyService.Get<IWifiConnector>();
                wifiConnector.ConnectToWifi("MTCLOG_CL", "22287003");

                HttpClient client = new HttpClient();
                client.GetAsync($"http://appshopping.com.br/wifi/liberar?email={Email}");
            }
            catch(Exception ex)
            {
                Message = ex.Message;
            }
        }

    }
}
