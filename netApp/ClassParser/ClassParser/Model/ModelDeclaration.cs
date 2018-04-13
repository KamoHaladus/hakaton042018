using System.Collections.Generic;

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
