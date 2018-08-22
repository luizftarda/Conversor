using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Conversor.Api.CurrenciesByCurrencyLayer.Messages
{
    [DataContract]
    public class CurrencyLayerListCurrencyResponse
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "currencies")]
        public Dictionary<string, string> Currencies { get; set; }

        [DataMember(Name = "error")]
        public CurrencyLayerError Error { get; set; }
    }

    [DataContract]
    public class CurrencyLayerLiveCurrencyResponse
    {
        [DataMember(Name = "success")]
        public bool Success { get; set; }

        [DataMember(Name = "quotes")]
        public Dictionary<string, string> Quotes { get; set; }

        [DataMember(Name = "error")]
        public CurrencyLayerError Error { get; set; }
    }

    [DataContract]
    public class CurrencyLayerError
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }

        [DataMember(Name = "type")]
        public string Type { get; set; }

        [DataMember(Name = "info")]
        public string Info { get; set; }
    }     
  
}
