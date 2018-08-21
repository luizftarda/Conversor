using System;
using System.Collections.Generic;
using System.Text;

namespace Conversor.Api.Domain.ValueObjects
{
    public class CurrencyIdentifier
    {
        private string _code;
        public string Code
        {
            get => _code;
            set
            {
                if (string.IsNullOrEmpty(value) &&
                    string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("code cannot be num or Empty");
                _code = value;
            }
        }

        private string _country;
        public string Name
        {
            get => _country;
            set
            {
                if (string.IsNullOrEmpty(value) &&
                   string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("country cannot be num or Empty");
                _country = value;
            }
        }
    }
}
