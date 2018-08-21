using Conversor.Api.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conversor.Api.Domain.Arguments
{
    public class ConvertRequest
    {
        public CurrencyIdentifier From { get; set; }

        public CurrencyIdentifier To { get; set; }

        public double Amount { get; set; }
    }
}
