using AppShopping.Library.Helpers.MVVM;
using AppShopping.Models;
using AppShopping.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace AppShopping.ViewModels
{
    [QueryProperty("establishmentSerialized", "es")]

    public class EstablishmentDetailViewModel : BaseViewModel
    {
        public  Establishment Establishment { get; set; }
        public  string establishmentSerialized
        {
            set
            {
                Establishment = JsonConvert.DeserializeObject<Establishment>(Uri.UnescapeDataString(value));
                OnPropertyChanged(nameof(Establishment));
            }
        }

        public EstablishmentDetailViewModel()
        {
          
        }
    }
}
