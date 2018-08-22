using Conversor.Api.CurrenciesByCurrencyLayer.Configuration;
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
    public class CurrencyService : ICurrencyService
    {
        private readonly CurrencyLayerConfiguration configuration;

        public CurrencyService()
        {
            this.configuration = new CurrencyLayerConfiguration();
        }
        
        public async Task<ListCurrencyIdentifierResponse> ListAllCurrencyIdentifier()
        {
            try
            {
                var client = CreateHttpClient();

                var query = HttpUtility.ParseQueryString(string.Empty);
                query["access_key"] = configuration.AccessKey;
                var queryString = query.ToString();
                
                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"list?{queryString}");

                var response = await client.SendAsync(httpRequest);

                var body = await GetBodyContent<CurrencyLayerListCurrencyResponse>(response);

                return new ListCurrencyIdentifierResponse
                {
                    Success = body.Success,
                    Error = body.Error == null ? null : new Error
                    {
                        Type = body.Error.Type,
                        Message = body.Error.Info
                    },
                    CurrencyIdentifiers = body.Currencies?.Select(e => new CurrencyIdentifier
                    {
                        Code = e.Key,
                        Name = e.Value
                    }),
                };
            }
            catch (Exception e)
            {
                return new ListCurrencyIdentifierResponse
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

        public async Task<ConvertResponse> Convert(ConvertRequest request)
        {
            try
            {
                FindCurrencyRequest finCurrencyRequest = new FindCurrencyRequest
                {
                    Currencies = new CurrencyIdentifier[] { request.From, request.To }
                };

                var findCurrencyResponse = await FindCurrency(finCurrencyRequest);

                if (findCurrencyResponse.Success)
                {
                    var from = findCurrencyResponse.Currencies[0];
                    var to = findCurrencyResponse.Currencies[1];

                    var value = from.ConverterToOtherCurrency(to, request.Amount);

                    return new ConvertResponse
                    {
                        Success = true,
                        Amount = request.Amount,
                        From = from,
                        To = to,
                        Value = value
                    };
                }
                return new ConvertResponse
                {
                    Success = false,
                    Error = findCurrencyResponse.Error
                };

            }
            catch (Exception e)
            {
                return new ConvertResponse
                {
                    Success = false,
                    Error = new Error
                    {
                        Message = e.Message,
                        Type = "Unspected"
                    }
                };
            }
        }
        
        private HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri("http://apilayer.net/api/");
            return client;
        }
        
        private async Task<T> GetBodyContent<T>(HttpResponseMessage response)
        {
            var serializer = new DataContractJsonSerializer(typeof(T),
                new DataContractJsonSerializerSettings()
                {
                    UseSimpleDictionaryFormat = true
                });

            var encoding = Encoding.UTF8;
            var stream = await response.Content.ReadAsStreamAsync();
            return (T)serializer.ReadObject(stream);
        }

        private Currency CreateCurrencyByQuoteAndIdentifiers(KeyValuePair<string, string> quote, CurrencyIdentifier[] currencies)
        {
            var currentIdentifier = currencies.FirstOrDefault(c => quote.Key.EndsWith(c.Code));
            var dollarValue = double.Parse(quote.Value, NumberFormatInfo.InvariantInfo);
            return new Currency(currentIdentifier, dollarValue);
        }
    }
}
