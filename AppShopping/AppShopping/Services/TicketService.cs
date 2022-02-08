using AppShopping.Library.Enums;
using AppShopping.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AppShopping.Services
{
    public class TicketService
    {
        private static List<Ticket> fateTickets = new List<Ticket>()
        {

            new Ticket() { Number = "109703757667", StartDate = new DateTime(2020,10,20,16,02,32), EndDate = new DateTime(2020,10,20,18,02,32), Price = 6.2m, Status = TicketStatus.paid},
            new Ticket() { Number = "209883557324", StartDate = new DateTime(2020,10,22,10,56,42), EndDate = new DateTime(2020,10,22,17,00,30), Price = 12.26m, Status = TicketStatus.paid},
            new Ticket() { Number=  "359645757456", StartDate = new DateTime(2020,10,20,20,32,01) },
             new Ticket() { Number= "359645757789", StartDate = new DateTime(2021,06,23,17,10,00) },

        };


        public List<Ticket> GetTicketsPaid()
        {
            return fateTickets.Where(a => a.Status == TicketStatus.paid).ToList();
        }

        public Ticket GetTicketToPaid(string number)
        {

            DateTime endDate = DateTime.Now;

            var ticket = fateTickets.FirstOrDefault(a => a.Number.Replace(" ","") == number.Replace(" ",""));

            if (ticket == null)
            {
                throw new Exception("Ticket não encontrado");
            }

            if (ticket.Status == TicketStatus.paid)
            {
                throw new Exception("Ticket já pago!");
            }

            if (ticket.Status == TicketStatus.paid)
            {
                throw new Exception("Ticket já pago!");
            }

            ticket.EndDate = endDate;

           
            ticket.Price = Convert.ToDecimal(PriceCalculator(ticket));
            return ticket;
        }


        public Ticket GetTicket(string number)
        {

            var ticket = fateTickets.FirstOrDefault(a => a.Number.Replace(" ", "") == number.Replace(" ", ""));

            if (ticket == null)
            {
                throw new Exception("Ticket não encontrado");
            }

            return ticket;
        }


        public void UpdateTicket(Ticket newTicket)
        {
            Ticket OldTicket = fateTickets.FirstOrDefault(a => a.Number.Replace(" ", "") 
                == newTicket.Number.Replace(" ", ""));
            OldTicket.TransactionId = newTicket.TransactionId;
            OldTicket.Price = newTicket.Price;
            OldTicket.Status = newTicket.Status;
            OldTicket.EndDate = newTicket.EndDate;
            //Envia para o servidor
        }

        private double PriceCalculator(Ticket ticket)
        {
            TimeSpan dif = ticket.EndDate.Value - ticket.StartDate;
            return Math.Round(dif.TotalMinutes * 0.3,2);
        }
    }
}
