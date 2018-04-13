using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassParser.Model
{
    public class TypeDeclaration
    {
        public string Name { get; set; }
        public bool IsArray { get; set; }
        public bool IsNullable { get; set; }

        public TypeDeclaration(string name, bool isArray = false, bool isNullable = false)
        {
            Name = name;
            IsArray = isArray;
        }
    }
}
