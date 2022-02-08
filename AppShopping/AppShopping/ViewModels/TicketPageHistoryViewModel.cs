using AppShopping.Library.Helpers.MVVM;
using AppShopping.Models;
using AppShopping.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppShopping.ViewModels
{
    public class TicketPageHistoryViewModel : BaseViewModel
    {
        public List<Ticket> Tickets { get; set; }
        public TicketPageHistoryViewModel()
        {
            Tickets = new TicketService().GetTicketsPaid();
        }

    }
}
