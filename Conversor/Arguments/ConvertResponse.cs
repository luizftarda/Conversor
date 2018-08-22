using Conversor.Api.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Conversor.Api.Domain.Arguments
{
    public class ConvertResponse
    {
        public bool Success { get; set; }

        public Currency From { get; set; }

        public Currency To { get; set; }

        public double? Amount { get; set; }

        public double? Value { get; set; }

        public Error Error { get; set; }
    }
}
