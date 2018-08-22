using Conversor.Api.CurrenciesByCurrencyLayer.Services;
using System;
using System.Linq;

namespace SandBox
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new CurrencyService();
            var r = service.ListAllCurrencyIdentifier().ContinueWith(t => {
                var n = t.Result;

                var one = n.CurrencyIdentifiers.ToList()[0];
                var two = n.CurrencyIdentifiers.ToList()[1];

                service.FindCurrency(new Conversor.Api.Domain.Arguments.FindCurrencyRequest
                {
                    Currencies = new Conversor.Api.Domain.ValueObjects.CurrencyIdentifier[]
                        {
                            one, two
                        }
                }).ContinueWith(t1 => {
                    var r2 = t1.Result;

                });
            });
            Console.ReadKey();
        }
    }
}
