using Conversor.Api.CurrenciesByCurrencyLayer.Configuration;
using Conversor.Api.CurrenciesByCurrencyLayer.Services;
using Conversor.Api.Domain.Arguments;
using Conversor.Api.Domain.Services;
using Conversor.Api.Domain.ValueObjects;
using Conversor.Api.Presentation.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Conversor.Api.Domain.Tests
{
    [TestClass]
    public class ControllerTests
    {
        [TestMethod]
        public void ConvertTestOk()
        {
            var currencies = new CurrencyIdentifier[]
                {
                    new CurrencyIdentifier
                    {
                        Code = "BRL",
                        Name = "Brazilian Real"
                    },
                     new CurrencyIdentifier
                    {
                        Code = "EUR",
                        Name = "Euro"
                    }
                };
            
            var service = new ConvertService(new CurrencyLayerConfiguration(),
                new CurrencyService(new CurrencyLayerConfiguration()));
            var result = service.Convert(new ConvertRequest
            {
                Amount = 1,
                From = currencies[0],
                To = currencies[1]
            }).Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ConvertTestError()
        {
            var currencies = new CurrencyIdentifier[]
                {
                    new CurrencyIdentifier
                    {
                        Code = "BRL",
                        Name = "Brazilian Real"
                    },
                     new CurrencyIdentifier
                    {
                        Code = "EUR",
                        Name = "Euro"
                    }
                };

            var service = new ConvertService(new CurrencyLayerConfiguration(),
                new CurrencyService(new CurrencyLayerConfiguration()));
            var result = service.Convert(new ConvertRequest
            {
                Amount = 1,
                From = currencies[0]
            }).Result;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void ListOk()
        {
            
            var service = new CurrencyIdentifierService(new CurrencyLayerConfiguration());
            var result = service.ListAllCurrencyIdentifier().Result;

            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void ListError()
        {

            var service = new CurrencyIdentifierService(new CurrencyLayerConfiguration() { AccessKey = "sdasdsad"});
            var result = service.ListAllCurrencyIdentifier().Result;

            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
        }

    }
}
