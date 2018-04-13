using ClassParser.Model;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ClassParser
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                var filePath = args[0];

                var code = File.ReadAllText(filePath);

                SyntaxTree tree = CSharpSyntaxTree.ParseText(code);

                var root = (CompilationUnitSyntax)tree.GetRoot();

                var members = root.Members;

                var helloWorldDeclaration = (NamespaceDeclarationSyntax)members[0];

                var classDeclaration = (ClassDeclarationSyntax)helloWorldDeclaration.Members[0];

                SyntaxList<MemberDeclarationSyntax> classMembers = classDeclaration.Members;

                var propertyList = new List<MemberDeclaration>();

                var meaningfulMembers = classMembers
                    .OfType<PropertyDeclarationSyntax>()
                    .Where(member => !HasMemberJsonIgnoreAttribute(member))
                    .ToList();

                foreach (var propertySyntax in meaningfulMembers)
                {
                    var member = new MemberDeclaration();
                    member.Name = propertySyntax.Identifier.Text.ToCamelCase();

                    if (propertySyntax.Type is GenericNameSyntax)
                    {
                        member.Type = BuildPropertyTypeFromGenericNameSyntax(propertySyntax.Type as GenericNameSyntax);
                    }
                    else if (propertySyntax.Type is NullableTypeSyntax)
                    {
                        member.Type = BuildPropertyTypeFromNullableTypeSyntax(propertySyntax.Type as NullableTypeSyntax);
                    }
                    else
                    {
                        member.Type = BuildPropertyTypeFromStandardSyntax(propertySyntax.Type);
                    }

                    propertyList.Add(member);
                }
                //foreach (var memeber in propertyList)
                //{
                //    Console.WriteLine($"{memeber.Name}, Type: {memeber?.Type?.Name}, {memeber?.Type?.IsArray}");
                //}
                var modelDeclaration = new ModelDeclaration(Path.GetFileNameWithoutExtension(filePath), propertyList);
                Console.Write(JsonConvert.SerializeObject(modelDeclaration,
                    new JsonSerializerSettings
                    {
                        DefaultValueHandling = DefaultValueHandling.Ignore,
                        ContractResolver = new CamelCasePropertyNamesContractResolver(),
                        Formatting = Formatting.Indented
                    }));
            }
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
            return type;
        }

        private static TypeDeclaration BuildPropertyTypeFromStandardSyntax(TypeSyntax typeSyntax)
        {
            if (typeSyntax is IdentifierNameSyntax)
            {
                string typeName = BuildPropertyTypeFromIdentifierNameSyntax(typeSyntax as IdentifierNameSyntax);
                return new TypeDeclaration(typeName.ToCamelCase());
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

            if (propertyAttributes.Any())
            {
                var attributes = propertyAttributes.SelectMany(x => x.Attributes);

                return attributes.Any(a => a.Name.ToString() == "JsonIgnore");
            }

            return false;
        }
    }
}