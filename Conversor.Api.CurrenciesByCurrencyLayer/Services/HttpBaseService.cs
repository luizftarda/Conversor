using Conversor.Api.CurrenciesByCurrencyLayer.Configuration;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Conversor.Api.CurrenciesByCurrencyLayer.Services
{
    public abstract class HttpBaseService
    {
        protected IConfiguration configuration;

        public HttpBaseService(IConfiguration configuration)
        {
            this.configuration = configuration; 
        }

        protected HttpClient CreateHttpClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.BaseAddress = new Uri(configuration.BaseUrl);
            return client;
        }

        protected async Task<T> GetBodyContent<T>(HttpResponseMessage response)
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
    }
}
