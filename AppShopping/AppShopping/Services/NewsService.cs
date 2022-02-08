using AppShopping.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppShopping.Services
{
    public class NewsService
    {
        private static List<News> fakeNews = new List<News>()
        {

            new News() { Title = "Dia das mães" , Description = "A cada 100 reais em compras você ganha um cupom para concorrer a 5 Hondas Fit"},
            new News() { Title = "Natal" , Description = "A cada 100 reais em compras você ganha um cupom para concorrer a 5 HRV "},
        };


        public List<News> GetNews()
        {
            return fakeNews;
        }

    }
}
