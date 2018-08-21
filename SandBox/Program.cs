using Conversor.Api.CurrenciesByCurrencyLayer.Services;
using System;

namespace SandBox
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new CurrencyService();
            var r = service.ListAllCurrencyIdentifier().ContinueWith(t => {
                var n = t.Result;
            });
            Console.ReadKey();
        }
    }
}
