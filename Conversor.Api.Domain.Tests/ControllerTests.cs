//using Conversor.Api.CurrenciesByCurrencyLayer.Services;
//using Conversor.Api.Domain.Arguments;
//using Conversor.Api.Domain.Services;
//using Conversor.Api.Domain.ValueObjects;
//using Conversor.Api.Presentation.Controllers;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Text;
//using System.Threading.Tasks;

//namespace Conversor.Api.Domain.Tests
//{
//    [TestClass]
//    public class ControllerTests
//    {
//        [TestMethod]
//        public async void ConvertTestOk()
//        {

//            var service = new CurrencyService();

//            var controller = new CurrencyController(service);

//            var result = await controller.Post(new ConvertRequest
//            {
//                Amount = 10,
//                From = new ValueObjects.CurrencyIdentifier
//                {
//                    Code = "BRL",
//                    Name = "Brazilian Real"
//                },
//                To = new ValueObjects.CurrencyIdentifier
//                {
//                    Code = "EUR",
//                    Name = "Euro"
//                }

//            });

//        }


//    }
//}
