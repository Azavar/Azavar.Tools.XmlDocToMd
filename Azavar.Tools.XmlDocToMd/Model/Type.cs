using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Azavar.Tools.XmlDocToMd.Model
{
    /// <summary>
    /// Represents a member element containing details about a type.
    /// </summary>
    public class Type : Member
    {
        /// <summary>
        /// Initializes a type with its parent model and the XML node is represents.
        /// </summary>
        /// <param name="model">The parent model containing this type.</param>
        /// <param name="node">The XML node represented by this type.</param>
        /// <exception cref="FormatException">The name attribute has an invalid format.</exception>
        public Type(XmlDocumentationModel model, XmlNode node) : base(model, node)
        {
            // ReSharper disable once PossibleNullReferenceException
            var name = node.Attributes["name"].Value;
            if (!name.StartsWith("T:"))
                throw new FormatException($"Bad type name format: {name}");
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
                    TypeParameters.Add(new TypeParameter(paramType));
                }
        }
        /// <summary>
        /// Gets a reference to the type that contains this type if it is a nested type, otherwise null.
        /// </summary>
        public Type ContainingType { get; }
        /// <summary>
        /// Gets the namespace in which this type is define, formatted as an array of strings ("Ns1.Ns2" will be {"Ns1","Ns2"}).
        /// </summary>
        public string[] NamespaceStrings { get; }
        /// <summary>
        /// Gets an ordered list of <see cref="TypeParameter"/> representing generic type parameters.
        /// </summary>
        public List<TypeParameter> TypeParameters { get; } = new List<TypeParameter>();
        /// <summary>
        /// Gets a list of <see cref="Method"/> representing all docuemnted methods and costructors of this type.
        /// </summary>
        public List<Method> Methods { get; } = new List<Method>();
        /// <summary>
        /// Gets a list of <see cref="Property"/> representing all documented properties of this type.
        /// </summary>
        public List<Property> Properties { get; } = new List<Property>();
        /// <summary>
        /// Gets a list of nested types defined within this type.
        /// </summary>
        public List<Type> NestedTypes { get; } = new List<Type>();

        /// <inheritdoc />
        public override string ToString()
        {
            return Model.AssemblyName + "." + string.Join(".", NamespaceStrings.Concat(new[] { Name }));
        }
    }
}