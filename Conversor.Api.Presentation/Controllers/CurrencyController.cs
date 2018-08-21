using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Conversor.Api.Domain.Arguments;
using Conversor.Api.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Conversor.Api.Presentation.Controllers
{
    [Route("api/[controller]")]
    public class CurrencyController : Controller
    {
        private readonly ICurrencyService currencyService;

        public CurrencyController(ICurrencyService currencyService)
        {
            this.currencyService = currencyService;
        }

        // GET api/currency/list
        [HttpGet("list")]
        public async Task<IActionResult> Get()
        {
            var listCurrencyIdentifierResponse = await currencyService.ListAllCurrencyIdentifier();
            if (listCurrencyIdentifierResponse.Success)
                return Ok(listCurrencyIdentifierResponse);
            return NotFound(listCurrencyIdentifierResponse);

        }

        [HttpPost("convert")]
        public async Task<IActionResult> Post(ConvertRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(request);
            }
            var response = await currencyService.Convert(request);
            if (response.Success)
                return Ok(response);
            return NotFound(response);

        }
       
    }
}
