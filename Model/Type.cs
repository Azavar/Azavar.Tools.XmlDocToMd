using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Azavar.Tools.XmlDocToMd.Model
{
    public class Type : Member
    {
        public Type(string name) : base(name)
        {

        }
        public Type(XmlDocumentationModel model, XmlNode node) : base(model, node)
        {
            // ReSharper disable once PossibleNullReferenceException
            var name = node.Attributes["name"].Value;
            if (!name.StartsWith("T:"))
                throw new InvalidOperationException("Not a valid type name");
            name = name.Substring(2);
            if (name.StartsWith(Model.AssemblyName + "."))
                name = name.Substring(Model.AssemblyName.Length + 1);
            var parts = name.Split(new[] { "." }, StringSplitOptions.None);
            Name = parts[parts.Length - 1];
            var potentialContainingTypeName =
                $"T:{Model.AssemblyName}.{string.Join(".", parts.Take(parts.Length - 1))}";
            var potentialContainingTypeId = potentialContainingTypeName;
            if (Model.Members.ContainsKey(potentialContainingTypeId))
            {
                var containingType = (Type)Model.Members[potentialContainingTypeId];
                NamespaceStrings = containingType.NamespaceStrings;
                containingType.NestedTypes.Add(this);
                ContainingType = containingType;
            }
            else
            {
                NamespaceStrings = parts.Take(parts.Length - 1).Select(p => Regex.Replace(p, "`(\\d+)", "")).ToArray();
            }
            var summary = node.SelectSingleNode("summary");
            if (summary != null)
                Documentation = new Documentation(summary);
            var paramTypes = node.SelectNodes("typeparam");
            if (paramTypes != null)
                foreach (XmlNode paramType in paramTypes)
                {
                    TypeParameters.Add(new TypeParameter(model, paramType));
                }
        }
        public Type ContainingType { get; set; }
        public string[] NamespaceStrings { get; set; }
        public List<TypeParameter> TypeParameters { get; } = new List<TypeParameter>();
        public List<Method> Methods { get; } = new List<Method>();
        public List<Property> Properties { get; } = new List<Property>();
        public List<Type> NestedTypes { get; } = new List<Type>();

    }
}