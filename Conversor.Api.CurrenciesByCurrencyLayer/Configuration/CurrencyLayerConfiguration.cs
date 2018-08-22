using System;
using System.Collections.Generic;
using System.Text;

namespace Conversor.Api.CurrenciesByCurrencyLayer.Configuration
{
    public class CurrencyLayerConfiguration : IConfiguration
    {
        public string AccessKey { get; set; } =  "3411e68200ba8f329c3f1911a78cb028";

        public string BaseUrl { get; set; } = "http://apilayer.net/api/";
    }
}
