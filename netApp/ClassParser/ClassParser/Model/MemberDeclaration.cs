using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassParser.Model
{
    public class MemberDeclaration
    {
        public string Name { get; set; }
        public TypeDeclaration Type { get; set; }
    }
}
