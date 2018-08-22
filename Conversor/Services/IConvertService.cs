using Conversor.Api.Domain.Arguments;
using System.Threading.Tasks;

namespace Conversor.Api.Domain.Services
{
    public interface IConvertService
    {
        Task<ConvertResponse> Convert(ConvertRequest request);
    }
}