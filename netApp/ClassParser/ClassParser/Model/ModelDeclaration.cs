using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassParser.Model
{
    public class ModelDeclaration
    {
        public string Name { get; set; }
        public List<MemberDeclaration> Members { get; set; }

        public ModelDeclaration(string name, List<MemberDeclaration> members)
        {
            Name = name;
            Members = members;
        }
    }
}
