using System.Runtime.Serialization;

namespace Conversor.Api.CurrenciesByCurrencyLayer.Configuration
{
    public interface IConfiguration
    {
        string AccessKey { get; set; }
        string BaseUrl { get; }
    }
}