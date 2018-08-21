using Conversor.Api.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conversor.Api.Domain.Arguments
{
    public class ListCurrencyIdentifierResponse
    {
        public bool Success { get; set; }

        public IEnumerable<CurrencyIdentifier> CurrencyIdentifiers { get; set; }

        public Error Error { get; set; }
    }
}
