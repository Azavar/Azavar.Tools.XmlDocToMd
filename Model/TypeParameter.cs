using System;
using System.Xml;

namespace Azavar.Tools.XmlDocToMd.Model
{
    public class TypeParameter : Member
    {
        public TypeParameter(XmlDocumentationModel model, XmlNode node) : base(model, node)
        {
            // ReSharper disable once PossibleNullReferenceException
            Name = node.Attributes["name"].Value;
            Documentation = new Documentation(node);
        }
    }
}