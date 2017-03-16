using System;
using System.Xml;

namespace Azavar.Tools.XmlDocToMd.Model
{
    /// <summary>
    /// Defines the base for objects represnting "member" tag in XML docuemntation.
    /// </summary>
    public abstract class Member
    {
        /// <summary>
        /// Gets the <see cref="XmlDocumentationModel"/> containing this member.
        /// </summary>
        public XmlDocumentationModel Model { get; }

        /// <summary>
        /// Initializes a member with its parent model and the XML node is represents.
        /// </summary>
        /// <param name="model">The parent model containing this member.</param>
        /// <param name="node">The XML node represented by this member.</param>
        /// <exception cref="ArgumentNullException">node is null.</exception>
        /// <exception cref="InvalidOperationException">Required XML attribute/element is missing.</exception>
        protected Member(XmlDocumentationModel model, XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            Model = model;
            if (node.Attributes?["name"] == null)
                throw new InvalidOperationException("Missing name attribute");
            var name = node.Attributes["name"].Value;
            if (string.IsNullOrEmpty(name))
                throw new InvalidOperationException("Missing name attribute");
            Id = name;
        }

        /// <summary>
        /// Gets the unique ID of the member, same as the value of attibute "name" in the XML documentation.
        /// </summary>
        public string Id { get; }
        /// <summary>
        /// Gets the name of the member.
        /// </summary>
        public string Name { get; protected set; }
        /// <summary>
        /// Gets the <see cref="XmlDocToMd.Model.Documentation"/> object associated with the member. 
        /// </summary>
        public Documentation Documentation { get; protected set; }
    }
}