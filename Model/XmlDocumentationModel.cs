using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace Azavar.Tools.XmlDocToMd.Model
{
    public class XmlDocumentationModel
    {
        public string AssemblyName { get; }
        public Dictionary<string, Member> Members { get; } = new Dictionary<string, Member>();

        public XmlDocumentationModel(string inputFilePath)
        {
            var doc = new XmlDocument();
            doc.Load(inputFilePath);
            if (doc.DocumentElement == null)
                throw new InvalidOperationException();
            AssemblyName = doc.DocumentElement.SelectSingleNode("/doc/assembly/name")?.InnerText;
            var types = doc.DocumentElement.SelectNodes("/doc/members/member[starts-with(@name, 'T:')]");
            if (types == null)
                throw new InvalidOperationException();
            foreach (XmlNode member in types)
            {
                var t = new Type(this, member);
                Members.Add(t.Id, t);
            }
            var methods = doc.DocumentElement.SelectNodes("/doc/members/member[starts-with(@name, 'M:')]");
            if (methods == null)
                throw new InvalidOperationException();
            foreach (XmlNode member in methods)
            {
                var t = new Method(this, member);
                Members.Add(t.Id, t);
            }
            var properties = doc.DocumentElement.SelectNodes("/doc/members/member[starts-with(@name, 'P:')]");
            if (properties == null)
                throw new InvalidOperationException();
            foreach (XmlNode member in properties)
            {
                var t = new Property(this, member);
                Members.Add(t.Id, t);
            }
        }
    }
}