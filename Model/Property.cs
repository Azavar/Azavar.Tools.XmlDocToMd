using System;
using System.Linq;
using System.Xml;
// ReSharper disable StringIndexOfIsCultureSpecific.1

namespace Azavar.Tools.XmlDocToMd.Model
{
    /// <summary>
    /// Represents a member element containing details about a property.
    /// </summary>
    public class Property : Member
    {
        private Type _containingType;
        /// <summary>
        /// Initializes a property with its parent model and the XML node is represents.
        /// </summary>
        /// <param name="model">The parent model containing this property.</param>
        /// <param name="node">The XML node represented by this property.</param>
        /// <exception cref="FormatException">The name attribute has an invalid format.</exception>
        public Property(XmlDocumentationModel model, XmlNode node) : base(model, node)
        {
            // ReSharper disable once PossibleNullReferenceException
            var name = node.Attributes["name"].Value;
            if (!name.StartsWith("P:"))
                throw new FormatException($"Bad property name format: {name}");
            name = name.Substring(2);
            if (!name.StartsWith(Model.AssemblyName + "."))
                throw new FormatException($"Bad property name format: {name}");
            var parentheseIdx = name.IndexOf("(");
            if (parentheseIdx > 0)
                name = name.Substring(0, parentheseIdx);
            name = name.Substring(Model.AssemblyName.Length + 1);
            var parts = name.Split(new[] { "." }, StringSplitOptions.None);
            Name = parts[parts.Length - 1];
            var containingTypeName = Model.AssemblyName + "." + string.Join(".", parts.Take(parts.Length - 1));
            var containingTypeId = "T:" + containingTypeName;
            var containingType = (Type)Model.Members[containingTypeId];
            ContainingType = containingType;
            var summary = node.SelectSingleNode("summary");
            if (summary != null)
                Documentation = new Documentation(summary);
        }

        /// <summary>
        /// Gets a reference to the type that contains the property.
        /// </summary>
        public Type ContainingType
        {
            get { return _containingType; }
            set
            {
                _containingType = value;
                if (value != null)
                    if (!value.Properties.Contains(this))
                        value.Properties.Add(this);
            }
        }
    }
}