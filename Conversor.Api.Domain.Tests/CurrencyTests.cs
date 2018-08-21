using Conversor.Api.Domain.Entities;
using Conversor.Api.Domain.ValueObjects;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Conversor.Api.Domain.Tests
{
    [TestClass]
    public class CurrencyTests
    {
        [TestMethod]
        public void CreateCurrency()
        {
            var currency = new Currency
            {
                DollarValue = 1,
                CurrencyIdentifier = new CurrencyIdentifier
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
                    CurrencyIdentifier = new CurrencyIdentifier
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
                    CurrencyIdentifier = new CurrencyIdentifier
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
                    CurrencyIdentifier = new CurrencyIdentifier
                    {
                        Code = null,
                        Country = null
                    }
                };
            });
        }
        [TestMethod]
        public void CreateWrongCurrency1()
        {
            Assert.ThrowsException<ArgumentNullException>(() => {
                var currency = new Currency(null, 1);
            });
        }

        [TestMethod]
        public void CreateWrongCurrency2()
        {
            Assert.ThrowsException<ArgumentException>(() => {
                var currency = new Currency(new CurrencyIdentifier {

                    Code = "CODE",
                    Country = "COUNTRY"
                }, -1);
            });
        }


        [TestMethod]
        public void ConvertTestOneByOne()
        {
            var currency = new Currency(new CurrencyIdentifier
            {
                Code = "CODE",
                Country = "BRZ"
            }, 1);

            var other = new Currency(new CurrencyIdentifier
            {
                Code = "OtCODE",
                Country = "OtBRZ"
            }, 1);

            var result = currency.ConverterToOtherCurrency(other, 1);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ConvertTestOneByOneHidingAmountArg()
        {
            var currency = new Currency(new CurrencyIdentifier
            {
                Code = "CODE",
                Country = "BRZ"
            }, 1);

            var other = new Currency(new CurrencyIdentifier
            {
                Code = "OtCODE",
                Country = "OtBRZ"
            }, 1);

            var result = currency.ConverterToOtherCurrency(other);
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void ConvertTestOneByOneWithNegativeArgs()
        {
            var currency = new Currency(new CurrencyIdentifier
            {
                Code = "CODE",
                Country = "BRZ"
            }, 1);

            var other = new Currency(new CurrencyIdentifier
            {
                Code = "OtCODE",
                Country = "OtBRZ"
            }, 1);

            Assert.ThrowsException<ArgumentException>(() => {
                currency.ConverterToOtherCurrency(other, -1);
            });

        }

        [TestMethod]
        public void ConvertTestOneByTen()
        {
            var currency = new Currency(new CurrencyIdentifier
            {
                Code = "CODE",
                Country = "BRZ"
            }, 1);

            var other = new Currency(new CurrencyIdentifier
            {
                Code = "OtCODE",
                Country = "OtBRZ"
            }, 10);

            var result = currency.ConverterToOtherCurrency(other, 100);
            Assert.AreEqual(1000, result);

        }

        [TestMethod]
        public void ConvertTestTenByOne()
        {
            var currency = new Currency(new CurrencyIdentifier
            {
                Code = "CODE",
                Country = "BRZ"
            }, 10);

            var other = new Currency(new CurrencyIdentifier
            {
                Code = "OtCODE",
                Country = "OtBRZ"
            }, 1);

            var result = currency.ConverterToOtherCurrency(other, 100);
            Assert.AreEqual(10, result);

        }

        [TestMethod]
        public void ConvertTestTwoByThree()
        {
            var currency = new Currency(new CurrencyIdentifier
            {
                Code = "CODE",
                Country = "BRZ"
            }, 2);

            var other = new Currency(new CurrencyIdentifier
            {
                Code = "OtCODE",
                Country = "OtBRZ"
            }, 3);

            var result = currency.ConverterToOtherCurrency(other, 200);
            Assert.AreEqual(300, result);
        }

        [TestMethod]
        public void ConvertTestTwoByThreeHidingArgs()
        {
            var currency = new Currency(new CurrencyIdentifier
            {
                Code = "CODE",
                Country = "BRZ"
            }, 2);

            var other = new Currency(new CurrencyIdentifier
            {
                Code = "OtCODE",
                Country = "OtBRZ"
            }, 3);

            var result = currency.ConverterToOtherCurrency(other);
            Assert.AreEqual(1.5, result);
        }

    }
}
