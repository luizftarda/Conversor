using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Conversor.App.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Text;
using Conversor.Api.Domain.Arguments;
using RestSharp;
using Conversor.Api.Domain.ValueObjects;

namespace Conversor.App.Controllers
{
    public class HomeController : Controller
    {
        private static List<CurrencyIdentifier> Identifiers;

        public HomeController()
        {
            if(Identifiers == null)
            {
                var client = new RestClient("http://localhost:59018/api/currency/list");
                var req = new RestRequest(Method.GET);

                var res = client.Get<ListCurrencyIdentifierResponse>(req);
                Identifiers = res.Data.CurrencyIdentifiers;

            }

        }

        public IActionResult Index(RequestConvertViewModel model)
        {
            ViewBag.ListCurrencyIdentifier1 = Identifiers.ToList();
            ViewBag.ListCurrencyIdentifier2 = Identifiers.ToList();
            
            return View(model);
        }

        [HttpPost]
        public IActionResult Convert(RequestConvertViewModel model)
        {
            var client = new RestClient("http://localhost:59018/api/currency/convert");
            var req = new RestRequest(Method.POST);


            var from = Identifiers.FirstOrDefault(x => x.Code == model.From);
            if(from == null)
            {
                from = Identifiers.FirstOrDefault(x => x.Name == model.From);
            }

            var to = Identifiers.FirstOrDefault(x => x.Code == model.To);
            if (to == null)
            {
                to = Identifiers.FirstOrDefault(x => x.Name == model.To);
            }

            var body = new ConvertRequest
            {
                Amount = model.Amount,
                From = Identifiers.FirstOrDefault(x => x.Code == model.From),
                To = Identifiers.FirstOrDefault(x => x.Code == model.To)
            };
            req.AddJsonBody(body);
           
            var res = client.Post<ConvertResponse>(req);
            model.Value = res.Data.Value.Value;
            model.From = Identifiers.FirstOrDefault(x => x.Code == model.From).Name;
            model.To = Identifiers.FirstOrDefault(x => x.Code == model.To).Name;
            ViewBag.ListCurrencyIdentifier1 = Identifiers.ToList();
            ViewBag.ListCurrencyIdentifier2 = Identifiers.ToList();

            return View(nameof(Index), model);
        }
    }
}
