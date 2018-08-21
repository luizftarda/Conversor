using Conversor.Api.Domain.Arguments;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conversor.Api.Domain.Services
{
    public interface ICurrencyService
    {
        Task<ListCurrencyIdentifierResponse> ListAllCurrencyIdentifier();

        Task<FindCurrencyResponse> FindCurrency(FindCurrencyRequest request);

        Task<ConvertResponse> Convert(ConvertRequest request)
    }
}
