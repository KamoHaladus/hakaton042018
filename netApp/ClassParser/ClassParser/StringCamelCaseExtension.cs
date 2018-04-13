using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ClassParser
{
    public static class StringCamelCaseExtension
    {
        public static string ToCamelCase(this string stringValue)
        {
            return stringValue.Substring(0, 1).ToLower() + stringValue.Substring(1);
        }
    }
}
