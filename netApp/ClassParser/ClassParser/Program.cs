using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.IO;

namespace ClassParser
{
    public class Program
    {
        static void Main(string[] args)
        {
            if(args.Length > 0)
            {
                var code = new StreamReader(args[0]).ReadToEnd();

                SyntaxTree tree = CSharpSyntaxTree.ParseText(code);

                var root = (CompilationUnitSyntax)tree.GetRoot();

                var members = root.Members;

                var helloWorldDeclaration = (NamespaceDeclarationSyntax)members[0];

                var classDeclaration = (ClassDeclarationSyntax)helloWorldDeclaration.Members[0];

                SyntaxList<MemberDeclarationSyntax> classMembers = classDeclaration.Members;
                var propertyList = new List<MemberDeclaration>();

                foreach (var memberSyntax in classMembers)
                {
                    if (memberSyntax is PropertyDeclarationSyntax)
                    {
                        PropertyDeclarationSyntax propertySyntax = memberSyntax as PropertyDeclarationSyntax;
                        if (!HasMemberJsonIgnoreAttribute(propertySyntax))
                        {
                            var memeber = new MemberDeclaration();
                            memeber.Name = propertySyntax.Identifier.Text;

                            if (propertySyntax.Type is GenericNameSyntax)
                            {
                                memeber.Type = BuildPropertyTypeFromGenericNameSyntax(propertySyntax.Type as GenericNameSyntax);
                            }
                            else if (propertySyntax.Type is NullableTypeSyntax)
                            {
                                memeber.Type = BuildPropertyTypeFromNullableTypeSyntax(propertySyntax.Type as NullableTypeSyntax);
                            }
                            else
                            {
                                memeber.Type = BuildPropertyTypeFromStandardSyntax(propertySyntax.Type);
                            }

                            propertyList.Add(memeber);
                        }
                    }
                }
                //foreach (var memeber in propertyList)
                //{
                //    Console.WriteLine($"{memeber.Name}, Type: {memeber?.Type?.Name}, {memeber?.Type?.IsArray}");
                //}
                Console.Write(Newtonsoft.Json.JsonConvert.SerializeObject(propertyList));
            }
            Console.ReadKey();
        }

        private static TypeDeclaration BuildPropertyTypeFromGenericNameSyntax(GenericNameSyntax genericNameSyntax)
        {
            TypeArgumentListSyntax agrumentListSyntax = genericNameSyntax.TypeArgumentList;
            TypeDeclaration type = BuildPropertyTypeFromStandardSyntax(agrumentListSyntax.Arguments.First());
            type.IsArray = true;
            return type;
        }

        private static string BuildPropertyTypeFromIdentifierNameSyntax(IdentifierNameSyntax identifierNameSyntax)
        {
            return TypeMapper.MapType(identifierNameSyntax.Identifier.Text);
        }

        private static string BuildPropertyTypeFromPredefinedTypeSyntax(PredefinedTypeSyntax predefinedNameSyntax)
        {
            return TypeMapper.MapType(predefinedNameSyntax.Keyword.Text);
        }

        private static TypeDeclaration BuildPropertyTypeFromNullableTypeSyntax(NullableTypeSyntax nullableTypeSyntax)
        {
            TypeDeclaration type = BuildPropertyTypeFromStandardSyntax(nullableTypeSyntax.ElementType);
            type.Name = $"{type.Name}?";
            return type;
        }

        private static TypeDeclaration BuildPropertyTypeFromStandardSyntax(TypeSyntax typeSyntax)
        {
            if (typeSyntax is IdentifierNameSyntax)
            {
                string typeName = BuildPropertyTypeFromIdentifierNameSyntax(typeSyntax as IdentifierNameSyntax);
                return new TypeDeclaration(typeName);
            }

            if (typeSyntax is PredefinedTypeSyntax)
            {
                string typeName = BuildPropertyTypeFromPredefinedTypeSyntax(typeSyntax as PredefinedTypeSyntax);
                return new TypeDeclaration(typeName);
            }

            if (typeSyntax is NullableTypeSyntax)
            {
                var typeDeclaration = BuildPropertyTypeFromNullableTypeSyntax(typeSyntax as NullableTypeSyntax);
                return typeDeclaration;
            }
            return null;
        }

        private static bool HasMemberJsonIgnoreAttribute(PropertyDeclarationSyntax propertySyntax)
        {
            SyntaxList<AttributeListSyntax> propertyAttributes = propertySyntax.AttributeLists;

            if (propertyAttributes.Any()) { 
                foreach(var attributesList in propertyAttributes)
                {
                    if (attributesList.Attributes.Any())
                    {
                        foreach (var attribute in attributesList.Attributes)
                        {
                            if(attribute.Name.ToString() == "JsonIgnore")
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }

    public class ModelDeclaration
    {
        public string Name { get; set; }
        public List<MemberDeclaration> Members { get; set; }
    }

    public class MemberDeclaration
    {
        public string Name { get; set; }
        public TypeDeclaration Type { get; set; }
    }

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