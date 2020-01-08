using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MvcUdemy.Utils
{
    public class RegexValidation
    {
        public bool ValidateCnpj(string cnpj)
        {
            var regex = new Regex(@"^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$");
            return regex.Match(cnpj).Success;
        }

        public bool ValidatePhone(string phone)
        {
            var regex = new Regex(@"[(]\d{2}[)]\d{1}\d{4}[-]\d{4}$");
            return regex.Match(phone).Success;
        }
    }
}
