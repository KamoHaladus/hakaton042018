using System.Collections.Generic;

namespace ClassParser
{
    public class TypeMapper
    {
        private static readonly Dictionary<string, string> ValuesDictionary
            = new Dictionary<string, string>
        {
            { "Guid", "string" },
            { "DateTime", "Date" },
            { "DateTimeOffset", "Date" },
            { "int", "number" },
            { "decimal", "number" },
            { "float", "number" },
        };

        public string MapType(string type)
        {
            return null;
        }
    }
}
