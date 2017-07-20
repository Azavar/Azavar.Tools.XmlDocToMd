using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace Azavar.Tools.XmlDocToMd.Model
{
    /// <summary>
    /// Represents a subtree of the XML documentation file that contains user comments, like <example>&lt;summary&gt;</example> element,
    /// the tag and all its contents will be loaded in a recursive structure.
    /// </summary>
    public class Documentation
    {
        /// <summary>
        /// The number of leading space chars.
        /// </summary>
        public int LeadingSpaceLength { get; }

        /// <summary>
        /// Initializes an instance with an XML node.
        /// </summary>
        /// <param name="node">The XML node representing the XML subtree to parse.</param>
        /// <exception cref="ArgumentNullException">The provided XML node is null.</exception>
        public Documentation(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException();
            var n = node;
            LeadingSpaceLength = 0;
            while (n != null && n.NodeType != XmlNodeType.Text)
                n = n.FirstChild;
            if (n != null)
                LeadingSpaceLength = n.Value.TakeWhile(char.IsWhiteSpace).Count(c=>c.Equals(' '));
            ParseNode(node);
        }

        /// <summary>
        /// Initializes an instance with an XML node and a leading space.
        /// </summary>
        /// <param name="node">The XML node representing the XML subtree to parse.</param>
        /// <param name="leadingSpace">The number of space chars leading the first line of the related documentation.</param>
        /// <exception cref="ArgumentNullException">The provided XML node is null.</exception>
        protected Documentation(XmlNode node, int leadingSpace)
        {
            LeadingSpaceLength = leadingSpace;
            ParseNode(node);
        }

        private void ParseNode(XmlNode node)
        {
            if (node.Attributes != null)
                foreach (XmlAttribute att in node.Attributes)
                    Attributes.Add(att.Name, att.Value);
            if (node.NodeType == XmlNodeType.Text)
            {
                FormattedContent = node.Value;
                FormattedContent = Regex.Replace(FormattedContent, @"^[\s\r\n]{" + LeadingSpaceLength + "}", string.Empty);
                FormattedContent = Regex.Replace(FormattedContent, @"\r\n[\s\r\n]{" + LeadingSpaceLength + "}", "\r\n");
                FormattedContent = FormattedContent.Replace(Environment.NewLine, Environment.NewLine + Environment.NewLine);
            }
            else
            {
                FormattedContent = string.Join(" ",
                    Enumerable.Range(0, node.ChildNodes.Count).Select(i => $"{{{i}}}"));
                DocumentationType = node.Name;
                foreach (XmlNode childNode in node.ChildNodes)
                    SubDocumentation.Add(new Documentation(childNode, LeadingSpaceLength));
            }
        }
        /// <summary>
        /// Gets the inner-XML of the element as a format string. Placeholders, if any, correspond to items in <see cref="SubDocumentation"/>.
        /// </summary>
        public string FormattedContent { get; private set; }
        /// <summary>
        /// Gets a key-value representation of the XML attributes that may be useful in rendering, 
        /// like {"cref", "T:System.Enum"} for <example>&lt;see cref="T:System.Enum"/&gt;</example>.
        /// </summary>
        public Dictionary<string, string> Attributes { get; } = new Dictionary<string, string>();
        /// <summary>
        /// Gets an ordered list of Documenaton objects representing XML node childs and correspond to placeholders in <see cref="FormattedContent"/>.
        /// </summary>
        public List<Documentation> SubDocumentation { get; } = new List<Documentation>();
        /// <summary>
        /// Gets the tag name represented by this object.
        /// </summary>
        public string DocumentationType { get; private set; }
    }
}