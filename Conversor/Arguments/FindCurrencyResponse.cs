using Conversor.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conversor.Api.Domain.Arguments
{
    public class FindCurrencyResponse
    {
        public bool Success { get; set; }

        public Currency[] Currencies { get; set; }

        public Error Error { get; set; }
    }
}
