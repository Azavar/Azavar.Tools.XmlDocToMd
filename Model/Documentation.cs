using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Azavar.Tools.XmlDocToMd.Model
{
    public class Documentation
    {
        public Documentation(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException();
            if (node.Attributes != null)
            {
                foreach (XmlAttribute att in node.Attributes)
                {
                    Attributes.Add(att.Name, att.Value);
                }
            }
            if (node.NodeType == XmlNodeType.Text)
                FormattedContent = node.Value.Trim();
            else
            {
                FormattedContent = string.Join(" ",
                    Enumerable.Range(0, node.ChildNodes.Count).Select(i => $"{{{i}}}"));
                DocumentationType = node.Name;
                foreach (XmlNode childNode in node.ChildNodes)
                {
                    SubDocumentation.Add(new Documentation(childNode));
                }
            }
        }
        public string FormattedContent { get; set; }
        public Dictionary<string, string> Attributes { get; } = new Dictionary<string, string>();
        public List<Documentation> SubDocumentation { get; } = new List<Documentation>();
        public string DocumentationType { get; set; }
    }
}