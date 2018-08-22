using Conversor.Api.Domain.Arguments;
using System.Threading.Tasks;

namespace Conversor.Api.Domain.Services
{
    public interface ICurrencyIdentifierService
    {
        Task<ListCurrencyIdentifierResponse> ListAllCurrencyIdentifier();
    }
}