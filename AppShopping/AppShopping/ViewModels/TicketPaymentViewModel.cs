using AppShopping.Library.Helpers.MVVM;
using AppShopping.Library.Validatiors;
using AppShopping.Models;
using AppShopping.Services;
using Plugin.PayCards;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace AppShopping.ViewModels
{
    [QueryProperty("Number", "Number")]
    public class TicketPaymentViewModel : BaseViewModel
    {

        private string _messages;

        public string Messages
        {
            get { return _messages; }
            set
            {
                SetProperty(ref _messages, value);
            }
        }


        private string _number;
        public string Number
        {
            set
            {
                SetProperty(ref _number, value);
                Ticket = _ticketService.GetTicketToPaid(value);
            }
        }

        private Ticket _ticket;

        public Ticket Ticket
        {
            get { return _ticket; }
            set {
                SetProperty(ref _ticket, value);

            }
        }

        private CreditCard _cfreditCard;

        public CreditCard CreditCard
        {
            get { return _cfreditCard; }
            set {
                SetProperty(ref _cfreditCard, value);
            }
        }


        public ICommand PaymentCommand { get; set; }
        public ICommand CreditCardScanCommand { get; set; }

        private TicketService _ticketService;
        private PaymentService _paymentService;

        public TicketPaymentViewModel()
        {
            _ticketService = new TicketService();
            _paymentService = new PaymentService();
            CreditCard = new CreditCard();

            PaymentCommand = new MvvmHelpers.Commands.AsyncCommand(Payment);
            CreditCardScanCommand = new MvvmHelpers.Commands.AsyncCommand(CreditCardScan);
        }

        private async Task Payment()
        {
            try
            //Verificar o Fluent Validation
            {
                Messages = string.Empty;

                string messages = Valid(CreditCard);

                if (string.IsNullOrEmpty(messages))
                {
                    //PagSeguro, Pagar.me, Paypal. - Pede dados do Cliente, Cartão de crédito/ Produto ou serviço
                    string transactionId = _paymentService.SendPayment(CreditCard, Ticket);
                    Ticket.TransactionId = transactionId;
                    Ticket.Status = Library.Enums.TicketStatus.paid;

                    _ticketService.UpdateTicket(Ticket);

                    await Shell.Current.GoToAsync($"ticket/payment/success?Number={Ticket.Number}");

                    //var x = _ticketService.GetTicketsPaid();
                }
                else
                {
                   Messages = messages;
                }
                //TODO-tela de sucesso
            }
            catch(Exception e)
            {
                await Shell.Current.GoToAsync($"ticket/payment/failed?Number={Ticket.Number}&message={e.Message}");
            }
        }


        private async Task CreditCardScan()
        {
            var cardInfo = await CrossPayCards.Current.ScanAsync();
            await Shell.Current.DisplayAlert("Mensagem:", $"{cardInfo.HolderName}\n{cardInfo.CardNumber}\n{cardInfo.ExpirationDate}", "Ok");

            //TODO - Atribuir aos campos da Tela

            if (string.IsNullOrEmpty(cardInfo.CardNumber))
            {

                CreditCard.Number = cardInfo.CardNumber;
            }

            if (string.IsNullOrEmpty(cardInfo.HolderName))
            {
                CreditCard.Name = cardInfo.HolderName;
            }


            if (string.IsNullOrEmpty(cardInfo.ExpirationDate))
            {
                CreditCard.DateExpired = cardInfo.ExpirationDate;

            }

            OnPropertyChanged(nameof(CreditCard));
        }

        private string Valid(CreditCard creditCard)
        {
            StringBuilder messages = new StringBuilder();

            if (string.IsNullOrEmpty(creditCard.Name))
            {
                messages.Append("O nome não foi preenchido!" + Environment.NewLine);
            }

            if (string.IsNullOrEmpty(creditCard.Number))
            {
                messages.Append("O numero do cartão não foi preenchido!" + Environment.NewLine);
            }
            else if (creditCard.Number.Length < 19)
            {
                messages.Append("O numero do cartão está incompleto" + Environment.NewLine);
            }

            try
            {
                var expiredString = creditCard.DateExpired.Split('/');
                var month = int.Parse(expiredString[0]);
                var year = int.Parse(expiredString[1]);
                new DateTime(year, month, 01);

                var expiredDate = new DateTime(year, month, 01);
                var now = DateTime.Now;

                if ((expiredDate.Year < now.Year) || 
                    (expiredDate.Month < now.Month && expiredDate.Year == now.Year))
                {
                    messages.Append("Cartão expirado!" + Environment.NewLine);
                }
            }
            catch (Exception e)
            {
                string ex = e.ToString();
                messages.Append("Validade do cartão não é valida!" + Environment.NewLine);
            }

            if (string.IsNullOrEmpty(creditCard.SecurityCode))
            {
                messages.Append("O código de segurança não foi preenchido" + Environment.NewLine);
            }
            else if (creditCard.SecurityCode.Length < 3)
            {
                messages.Append("O código de segurança está incompleto!" + Environment.NewLine);
            }


            if (string.IsNullOrEmpty(creditCard.Document))
            {
                messages.Append("O CPF não foi preenchido" + Environment.NewLine);
            }
            else if (creditCard.Document.Length < 14)
            {
                messages.Append("O CPF está incompleto!" + Environment.NewLine);
            }
            else if (!CPFValidator.IsCpf(CreditCard.Document))
            {
                messages.Append("O CPF é invalido!" + Environment.NewLine);
            }

            return messages.ToString();
        }

      
    }
}
