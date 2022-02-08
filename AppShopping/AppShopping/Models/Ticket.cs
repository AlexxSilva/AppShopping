using AppShopping.Library.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppShopping.Models
{
    public class Ticket
    {
        public string Number { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public TicketStatus Status { get; set; }
        public decimal Price { get; set; }

        public string TransactionId { get; set; }
    }
}
