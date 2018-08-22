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
        private readonly ICurrencyIdentifierService currencyIdentifierService;
        private readonly IConvertService convertService;

        public CurrencyController(ICurrencyService currencyService, 
            ICurrencyIdentifierService currencyIdentifierService, IConvertService convertService)
        {
            this.currencyService = currencyService;
            this.currencyIdentifierService = currencyIdentifierService;
            this.convertService = convertService;
        }

        // GET api/currency/list
        [HttpGet("list")]
        public async Task<IActionResult> Get()
        {
            var listCurrencyIdentifierResponse = await currencyIdentifierService.ListAllCurrencyIdentifier();
            if (listCurrencyIdentifierResponse.Success)
                return Ok(listCurrencyIdentifierResponse);
            return NotFound(listCurrencyIdentifierResponse);

        }

        // POST api/currency/convert
        [HttpPost("convert")]
        public async Task<IActionResult> Post([FromBody]ConvertRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ConvertResponse {
                    Success = false,
                    Error = new Error
                    {
                        Message = "Request Arguments are invalids",
                        Type = "Invalid Arguments"
                    }
                });
            }
            var response = await convertService.Convert(request);
            if (response.Success)
                return Ok(response);
            return NotFound(response);

        }
       
    }
}
