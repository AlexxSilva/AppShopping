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
    public abstract class EstablishmentViewModel : BaseViewModel
    {
        /* 
      * MVVM - VIEW <--> ViewModel <--> Model
      * 
      *  -comands - Açoes e métodos
         -Bindings - vinculos > propriedades
         -Notifications - INotifyPropertyChanged(Ouve mudança em uma propriedade),
         MessageCenter

         view(Lojas- Stores) : Entry (text= binding SearchWord, Mode=TwoWay)
         viewModel: string SearchWord            
      */
        public string SearchWord { get; set; }
        public ICommand SearchCommand { get; set; }
        public ICommand DetailCommand { get; set; }

        private List<Establishment> _establishments;

        public List<Establishment> Establishments
        {
            get { return _establishments; }
            set
            {
                SetProperty(ref _establishments, value);
                //OnPropertyChanged("Establishments");
            }
        }

        private List<Establishment> _allEstablishments;

        private EstablishmentType _establishmentType { get; set; }

        public EstablishmentViewModel(EstablishmentType establishmentType)
        {
            _establishmentType = establishmentType;
            SearchCommand = new Command(Search);
            DetailCommand = new Command<Establishment>(Detail);


            var allEstablishment = new EstablishmentServices().GetEstablishments();
            var allStores = allEstablishment.Where(a => a.Type == _establishmentType).ToList();
            Establishments = allStores;
            _allEstablishments = allStores;
        }

        private void Search()
        {
            Establishments = _allEstablishments.Where
                    (a => a.Name.ToLower().Contains(SearchWord.ToLower())).ToList();
        }

        private void Detail(Establishment e)
        {
            string establishmentSerialized = JsonConvert.SerializeObject(e);
            //URI para converter para uma formatação de URL
            Shell.Current.GoToAsync($"establishment/detail?es={Uri.EscapeDataString(establishmentSerialized)}");
        }

    }
}
