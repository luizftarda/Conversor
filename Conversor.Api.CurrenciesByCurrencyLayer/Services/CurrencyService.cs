﻿using Conversor.Api.CurrenciesByCurrencyLayer.Configuration;
using Conversor.Api.CurrenciesByCurrencyLayer.Messages;
using Conversor.Api.Domain.Arguments;
using Conversor.Api.Domain.Entities;
using Conversor.Api.Domain.Services;
using Conversor.Api.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Conversor.Api.CurrenciesByCurrencyLayer.Services
{
    public class CurrencyService : HttpBaseService, ICurrencyService
    {
        public CurrencyService(IConfiguration configuration)
            :base(configuration)
        {

        }
        
       
        public async Task<FindCurrencyResponse> FindCurrency(FindCurrencyRequest request)
        {
            try
            {
                var client = CreateHttpClient();

                var query = HttpUtility.ParseQueryString(string.Empty);
                query["access_key"] = configuration.AccessKey;
                query["currencies"] = string.Join(@",", request.Currencies.Select(c => c.Code));
                var queryString = query.ToString();
                
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"live?{queryString}");

                var response = await client.SendAsync(httpRequest);

                var body = await GetBodyContent<CurrencyLayerLiveCurrencyResponse>(response);

                return new FindCurrencyResponse
                {
                    Success = body.Success,
                    Currencies = body.Error != null ? null :
                        body.Quotes.Select(q => CreateCurrencyByQuoteAndIdentifiers(q, request.Currencies)).ToArray(),
                    Error = body.Error == null ? null : new Error
                    {
                        Type = body.Error.Type,
                        Message = body.Error.Info
                    },
                };
            }
            catch (Exception e)
            {
                return new FindCurrencyResponse
                {
                    Success = false,
                    Error = new Error
                    {
                        Type = "Unspected",
                        Message = e.Message
                    },
                };
            }
        }

       
        private Currency CreateCurrencyByQuoteAndIdentifiers(KeyValuePair<string, string> quote, CurrencyIdentifier[] currencies)
        {
            var currentIdentifier = currencies.FirstOrDefault(c => quote.Key.EndsWith(c.Code));
            var dollarValue = double.Parse(quote.Value, NumberFormatInfo.InvariantInfo);
            return new Currency(currentIdentifier, dollarValue);
        }
    }
}
