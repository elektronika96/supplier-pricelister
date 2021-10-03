using buildxact_supplies.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace SuppliesPriceLister
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string audUsdExchangeRate;
                IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", true, true)
                .Build();
                var exchangeRate = configuration.GetSection(nameof(audUsdExchangeRate));
                Products result = new Products(Decimal.Parse(exchangeRate.Value.Replace('.',',')));
                result.products.Sort((x, y) => x.price.CompareTo(y.price));
                result.products.Reverse();
                foreach (var product in result.products)
                    Console.WriteLine(product.ID + ", " + product.name + ", " + Math.Round(product.price).ToString("C2", new CultureInfo("en-US")));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
