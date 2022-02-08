using AppShopping.Library.Helpers.MVVM;
using AppShopping.Models;
using AppShopping.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;


namespace AppShopping.ViewModels
{
    [QueryProperty("Number", "Number")]
    public class TicketPaymentSuccessViewModel : BaseViewModel
    {



        private string _number;
        public string Number
        {
            set
            {
                SetProperty(ref _number, value);
                Ticket = _ticketService.GetTicket(value);
            }
        }

        private TicketService _ticketService;

        private Ticket _ticket;

        public Ticket Ticket
        {
            get { return _ticket; }
            set
            {

                SetProperty(ref _ticket, value);
            }
        }
        public ICommand OkCommand { get; set; }

        public TicketPaymentSuccessViewModel()
        {
            
            OkCommand = new Command(OK);
            _ticketService = new TicketService();

            int indexScreenToRemove = 1;
            /*
             * [0] - TicketScan
             * [1] - TicketPayment
             * [3] - Sucess/Failed
             * 
             * */
            Shell.Current.Navigation.RemovePage(
                Shell.Current.Navigation.NavigationStack[indexScreenToRemove]);

        }

        private void OK()
        {
            Shell.Current.Navigation.PopToRootAsync();
        }
    }
}
