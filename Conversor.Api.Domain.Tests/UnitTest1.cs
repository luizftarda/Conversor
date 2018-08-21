using Conversor.Api.Domain.Entities;
using Conversor.Api.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Conversor.Api.Domain.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CreateCurrency()
        {
            var currency = new Currency
            {
                DollarValue = 1,
                Identifier = new CurrencyIdentifier
                {
                    Code = "CODE",
                    Country = "COUNTRY"
                }
            };

            Assert.IsNotNull(currency);
        }

        [TestMethod]
        public void CreateWrongIdentifierCurrency1()
        {
            Assert.ThrowsException<ArgumentException>(() => {
                var currency = new Currency
                {
                    DollarValue = 1,
                    Identifier = new CurrencyIdentifier
                    {
                        Code = "",
                        Country = "COUNTRY"
                    }
                };
            });
        }
        [TestMethod]
        public void CreateWrongIdentifierCurrency2()
        {
            Assert.ThrowsException<ArgumentException>(() => {
                var currency = new Currency
                {
                    DollarValue = 1,
                    Identifier = new CurrencyIdentifier
                    {
                        Code = "asas",
                        Country = null
                    }
                };
            });
        }
        [TestMethod]
        public void CreateWrongIdentifierCurrency3()
        {
            Assert.ThrowsException<ArgumentException>(() => {
                var currency = new Currency
                {
                    DollarValue = 1,
                    Identifier = new CurrencyIdentifier
                    {
                        Code = null,
                        Country = null
                    }
                };
            });
        }
    }
}
