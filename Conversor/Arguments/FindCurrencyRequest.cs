using Conversor.Api.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conversor.Api.Domain.Arguments
{
    public class FindCurrencyRequest
    {
        public CurrencyIdentifier[] Currencies { get; set; }
    }
}
