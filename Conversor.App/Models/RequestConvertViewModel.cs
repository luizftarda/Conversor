using Conversor.Api.Domain.ValueObjects;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Conversor.App.Models
{
    public class RequestConvertViewModel
    {

        public double Amount { get; set; }
        
        public string From { get; set; }
        
        public String To { get; set; }
        
        public double Value { get; set; }

    }
}
