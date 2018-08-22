using Conversor.Api.CurrenciesByCurrencyLayer.Configuration;
using Conversor.Api.Domain.Arguments;
using Conversor.Api.Domain.Services;
using Conversor.Api.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conversor.Api.CurrenciesByCurrencyLayer.Services
{
    public class ConvertService : HttpBaseService, IConvertService
    {
        private readonly ICurrencyService currencyService;

        public ConvertService(IConfiguration configuration, ICurrencyService currencyService)
            :base(configuration)
        {
            this.currencyService = currencyService;
        }

        public async Task<ConvertResponse> Convert(ConvertRequest request)
        {
            try
            {
                FindCurrencyRequest finCurrencyRequest = new FindCurrencyRequest
                {
                    Currencies = new CurrencyIdentifier[] { request.From, request.To }
                };

                var findCurrencyResponse = await currencyService.FindCurrency(finCurrencyRequest);

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



    }
}
