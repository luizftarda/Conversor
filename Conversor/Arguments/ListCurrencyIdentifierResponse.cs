using Conversor.Api.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Conversor.Api.Domain.Arguments
{
    [DataContract]
    public class ListCurrencyIdentifierResponse
    {
        [DataMember]
        public bool Success { get; set; }

        [DataMember]
        public List<CurrencyIdentifier> CurrencyIdentifiers { get; set; }

        [DataMember]
        public Error Error { get; set; }
    }
}
