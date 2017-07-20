using System;
using System.Xml;

namespace Azavar.Tools.XmlDocToMd.Model
{
    /// <summary>
    /// Represents an exception element for a <see cref="Method"/>.
    /// </summary>
    public class ThrownException
    {
        /// <summary>
        /// Initializes a ThrownException instance with the XML node it represents.
        /// </summary>
        /// <param name="node">The XML node represented.</param>
        /// <exception cref="ArgumentNullException">XML node is null.</exception>
        /// <exception cref="InvalidOperationException">Required XML attribute/element is missing.</exception>
        public ThrownException(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if (node.Attributes?["cref"] == null)
                throw new InvalidOperationException("Missing cref attribute");

            ExceptionClassId = node.Attributes["cref"].Value;
            Documentation = new Documentation(node);
        }
        /// <summary>
        /// Gets the ID of the exception class
        /// </summary>
        public string ExceptionClassId { get; }
        /// <summary>
        /// Gets the <see cref="Model.Documentation"/> object associated with this ThrownException.
        /// </summary>
        public Documentation Documentation { get; }
    }
}