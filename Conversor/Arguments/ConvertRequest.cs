using Conversor.Api.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Conversor.Api.Domain.Arguments
{
    [DataContract]
    public class ConvertRequest
    {
        [DataMember(IsRequired = true)]
        public CurrencyIdentifier From { get; set; }

        [DataMember(IsRequired = true)]
        public CurrencyIdentifier To { get; set; }

        [DataMember(IsRequired = true)]
        public double Amount { get; set; }
    }
}
