using Conversor.Api.CurrenciesByCurrencyLayer.Configuration;
using Conversor.Api.CurrenciesByCurrencyLayer.Messages;
using Conversor.Api.Domain.Arguments;
using Conversor.Api.Domain.Services;
using Conversor.Api.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Conversor.Api.CurrenciesByCurrencyLayer.Services
{
    public class CurrencyIdentifierService : HttpBaseService, ICurrencyIdentifierService
    {
    
        public CurrencyIdentifierService(IConfiguration configuration)
            :base(configuration)
        {
    
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
                    }).ToList(),
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

    }
}
