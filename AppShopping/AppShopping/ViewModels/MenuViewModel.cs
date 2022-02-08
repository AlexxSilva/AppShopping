using AppShopping.Library.Helpers.MVVM;
using MvvmHelpers.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppShopping.ViewModels
{
    public class MenuViewModel : BaseViewModel
    {
        public ICommand OpenMapCommand { get; set; }

        public MenuViewModel()
        {
            OpenMapCommand = new AsyncCommand(OpenMap);
        }

        private async Task OpenMap()
        {
            var location = new Location(-23.767375026139465, -46.584712982653414);
            var options = new MapLaunchOptions{ Name = "MTC- LOG",
            NavigationMode = NavigationMode.Default};
            try
            {
                await Map.OpenAsync(location, options);
                    
            } catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Erro!", "Não conseguimos abrir o mapa", "Ok");
            }
        }
    }
}
