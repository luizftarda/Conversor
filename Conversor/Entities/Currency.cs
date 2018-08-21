using Conversor.Api.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conversor.Api.Domain.Entities
{
    public class Currency
    {
        private CurrencyIdentifier _identifier;
        public CurrencyIdentifier Identifier
        {
            get => _identifier;
            set
            {
                _identifier = value ?? throw new ArgumentNullException("Currency indentifier cannot be null");
            }
        }


        public double DollarValue { get; set; }

        public double? ConverterToOtherCurrency(Currency other, double? amount)
        {
            return null;
        }
    }
}
