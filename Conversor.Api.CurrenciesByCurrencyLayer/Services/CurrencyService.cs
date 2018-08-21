using Conversor.Api.CurrenciesByCurrencyLayer.Configuration;
using Conversor.Api.CurrenciesByCurrencyLayer.Messages;
using Conversor.Api.Domain.Arguments;
using Conversor.Api.Domain.Services;
using Conversor.Api.Domain.ValueObjects;
using System;
using System.Collections.Generic;
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

        public async Task<ConvertResponse> Convert(ConvertRequest request)
        {
            FindCurrencyRequest requestFrom = new FindCurrencyRequest
            {
                Currency = request.From
            };
            FindCurrencyRequest requestTo = new FindCurrencyRequest
            {
                Currency = request.To
            };

            var responseFrom = await FindCurrency(requestFrom);
            var responseTo = await FindCurrency(requestTo);

            var convertedAmount = responseFrom.Currency.ConverterToOtherCurrency(responseTo.Currency, request.Amount);

            return new ConvertResponse
            {
                Success = responseFrom.Success && responseTo.Success,
                Amount = request.Amount,
                From = responseFrom.Currency,
                To = responseTo.Currency,
                Value = convertedAmount,
                
            };

        }

        public Task<FindCurrencyResponse> FindCurrency(FindCurrencyRequest request)
        {


            return null;

        }
        
        public async Task<ListCurrencyIdentifierResponse> ListAllCurrencyIdentifier()
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.BaseAddress = new Uri("http://apilayer.net/api/");

                var query = HttpUtility.ParseQueryString(string.Empty);
                query["access_key"] = configuration.AccessKey;

                string queryString = query.ToString();

                var httpRequest = new HttpRequestMessage(HttpMethod.Get, $"list?{queryString}");

                var response = await client.SendAsync(httpRequest);

                return await GetBodyContent(response);
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

        private async Task<ListCurrencyIdentifierResponse> GetBodyContent(HttpResponseMessage response)
        {
            var serializer = new DataContractJsonSerializer(typeof(CurrencyLayerListCurrencyResponse),
                new DataContractJsonSerializerSettings()
                {
                    UseSimpleDictionaryFormat = true
                });

            var encoding = Encoding.UTF8;
            var stream = await response.Content.ReadAsStreamAsync();
            var body = serializer.ReadObject(stream) as CurrencyLayerListCurrencyResponse;

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
    }
}
