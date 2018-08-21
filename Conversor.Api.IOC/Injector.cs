using Conversor.Api.CurrenciesByCurrencyLayer.Services;
using Conversor.Api.Domain.Services;
using System;

namespace Conversor.Api.IOC
{
    public class Injector
    {
        public static void Inject(Action<Type, Type> injectorFunction)
        {
          injectorFunction(typeof(ICurrencyService), typeof(CurrencyService));
        }
    }
}
