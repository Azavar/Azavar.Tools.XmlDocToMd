using System;
using System.Xml;

namespace Azavar.Tools.XmlDocToMd.Model
{
    /// <summary>
    /// Represets 
    /// </summary>
    public class TypeParameter
    {
        /// <summary>
        /// Initializes a TypeParameter instance with the XML node it represets.
        /// </summary>
        /// <param name="node">The XML node being represented.</param>
        /// <exception cref="ArgumentNullException">node is null.</exception>
        /// <exception cref="InvalidOperationException">Required XML attribute/element is missing.</exception>
        public TypeParameter(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if (node.Attributes?["name"] == null)
                throw new InvalidOperationException("Missing name attribute");
            var name = node.Attributes["name"].Value;
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("Missing name attribute");
            Name = name;
            Documentation = new Documentation(node);
        }

        /// <summary>
        /// Gets the name of the type parameter.
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// Gets the <see cref="XmlDocToMd.Model.Documentation"/> object associated with the type parameter. 
        /// </summary>
        public Documentation Documentation { get; protected set; }
    }
}