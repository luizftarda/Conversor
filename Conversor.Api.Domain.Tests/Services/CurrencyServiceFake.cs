using Conversor.Api.Domain.Arguments;
using Conversor.Api.Domain.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conversor.Api.Domain.Tests.Services
{
    public class CurrencyServiceFake : ICurrencyService
    {
        private Func<FindCurrencyRequest, Task<FindCurrencyResponse>> findCurrencyFake;
        private Func<Task<ListCurrencyIdentifierResponse>> listAllCurrencyIdentifierFake;

        public CurrencyServiceFake(Func<FindCurrencyRequest, Task<FindCurrencyResponse>> findCurrencyFake = null, 
            Func<Task<ListCurrencyIdentifierResponse>> listAllCurrencyIdentifierFake = null)
        {
            this.findCurrencyFake = findCurrencyFake;
            this.listAllCurrencyIdentifierFake = listAllCurrencyIdentifierFake;
        }
        public Task<FindCurrencyResponse> FindCurrency(FindCurrencyRequest request)
        {
            return findCurrencyFake?.Invoke(request);
        }

        public Task<ListCurrencyIdentifierResponse> ListAllCurrencyIdentifier()
        {
            return listAllCurrencyIdentifierFake?.Invoke();
        }
    }
}
