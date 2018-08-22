using Conversor.Api.CurrenciesByCurrencyLayer.Configuration;
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
            injectorFunction(typeof(IConvertService), typeof(ConvertService));
            injectorFunction(typeof(ICurrencyIdentifierService), typeof(CurrencyIdentifierService));
            injectorFunction(typeof(IConfiguration), typeof(CurrencyLayerConfiguration));
            
        }
    }
}
