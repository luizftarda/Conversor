using Conversor.Api.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conversor.Api.Domain.Entities
{
    public class Currency
    {
        public Currency()
        {

        }

        public Currency(CurrencyIdentifier currencyIdentifier, double dollarValue)
        {
            CurrencyIdentifier = currencyIdentifier;
            DollarValue = dollarValue;
        }

        private CurrencyIdentifier _currencyIdentifier;
        public CurrencyIdentifier CurrencyIdentifier
        {
            get => _currencyIdentifier;
            set
            {
                _currencyIdentifier = value ?? throw new ArgumentNullException("Currency indentifier cannot be null");
            }
        }

        private double _dollarValue;
        public double DollarValue
        {
            get => _dollarValue;
            set
            {
                if (value <= 0)
                    throw new ArgumentException("Dollar value cannot be negative or zero");
                _dollarValue = value;
            }
        }

        public double ConverterToOtherCurrency(Currency other, double? amount = null)
        {
            double newAmount = amount ?? 1;

            double amountInDollar = TransformAmountInDollar(newAmount);
            return amountInDollar * other.DollarValue;

        }

        private double TransformAmountInDollar(double amount)
        {
            if (amount < 0)
                throw new ArgumentException("amount caanot be negative or zero");
       
            return amount / DollarValue;
        }
    }
}
