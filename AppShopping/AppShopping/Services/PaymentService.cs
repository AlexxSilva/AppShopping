using AppShopping.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppShopping.Services
{
    public class PaymentService
    {
        public string SendPayment(CreditCard creditCard, Ticket ticket)
        {
            //ticket.Price;
            //ticket.Number;
            if (creditCard.SecurityCode == "111")
            {
                throw new Exception("Codigo de segurança inválido!");
            }

            return "1";
        }
    }
}
