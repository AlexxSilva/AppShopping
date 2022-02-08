using AppShopping.Models;
using AppShopping.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using AppShopping.Library.Enums;
using AppShopping.Library.Helpers.MVVM;
using Newtonsoft.Json;

namespace AppShopping.ViewModels
{
    public class StoresViewModel : EstablishmentViewModel
    {
        public StoresViewModel(EstablishmentType establishmentType) : base(establishmentType)
        {
        }
    }
}
