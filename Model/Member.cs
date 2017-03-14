using System;
using System.Collections.Generic;
using System.Xml;

namespace Azavar.Tools.XmlDocToMd.Model
{
    public abstract class Member
    {
        public XmlDocumentationModel Model { get; }

        protected Member(XmlDocumentationModel model, XmlNode node)
        {
            Model = model;
            if (node.Attributes?["name"] == null)
                throw new InvalidOperationException("Missing name attribute");
            var name = node.Attributes["name"].Value;
            if (name == null)
                throw new ArgumentNullException();
            Id = name;
        }

        protected Member(string name)
        {
            if (name == null)
                throw new ArgumentNullException();
            Id = name;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public Documentation Documentation { get; set; }
    }
}