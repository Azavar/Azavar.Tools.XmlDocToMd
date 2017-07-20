using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
// ReSharper disable StringIndexOfIsCultureSpecific.1
// ReSharper disable StringIndexOfIsCultureSpecific.2

namespace Azavar.Tools.XmlDocToMd.Model
{
    /// <summary>
    /// Represents a member element containing details about a method or constructor.
    /// </summary>
    public class Method : Member
    {
        private Type _containingType;

        /// <summary>
        /// Initializes a method with its parent model and the XML node is represents.
        /// </summary>
        /// <param name="model">The parent model containing this method.</param>
        /// <param name="node">The XML node represented by this method.</param>
        /// <exception cref="FormatException">The name attribute has an invalid format.</exception>
        public Method(XmlDocumentationModel model, XmlNode node) : base(model, node)
        {
            // ReSharper disable once PossibleNullReferenceException
            var name = node.Attributes["name"].Value;
            if (!name.StartsWith("M:"))
                throw new FormatException($"Bad method name format: {name}");
            name = name.Substring(2);
            if (!name.StartsWith(Model.AssemblyName + "."))
                throw new FormatException($"Bad method name format: {name}");
            name = name.Substring(Model.AssemblyName.Length + 1);
            var parentheseIdx = name.IndexOf("(");
            if (parentheseIdx > 0)
            {
                var parametersPart = name.Substring(parentheseIdx + 1).TrimEnd(')');
                ParseParameters(parametersPart);
                name = name.Substring(0, parentheseIdx);
            }
            var parts = name.Split(new[] { "." }, StringSplitOptions.None);
            Name = parts[parts.Length - 1];
            var containingTypeName = Model.AssemblyName + "." +
                                              string.Join(".", parts.Take(parts.Length - 1));
            var containingTypeId = "T:" + containingTypeName;
            var containingType = (Type)Model.Members[containingTypeId];
            ContainingType = containingType;
            var summary = node.SelectSingleNode("summary");
            if (summary != null)
                Documentation = new Documentation(summary);
            var paramNodes = node.SelectNodes("param");
            if (paramNodes != null)
            {
                var i = 0;
                foreach (XmlNode paramNode in paramNodes)
                {
                    if (i < Parameters.Count)
                    {
                        Parameters[i].Documentation = new Documentation(paramNode);
                        Parameters[i].Name = paramNode.Attributes?["name"]?.Value;
                    }
                    i++;
                }
            }
            var paramTypes = node.SelectNodes("typeparam");
            if (paramTypes != null)
                foreach (XmlNode paramType in paramTypes)
                    TypeParameters.Add(new TypeParameter(paramType));
            var thrownExceptionNodes = node.SelectNodes("exception");
            if (thrownExceptionNodes != null)
                foreach (XmlNode thrownExceptionNode in thrownExceptionNodes)
                {
                    ThrownExceptions.Add(new ThrownException(thrownExceptionNode));
                }
        }

        private void ParseParameters(string parametersString)
        {
            var r = Regex.Replace(parametersString, "{.+}", match => match.Value.Replace(",", "##"));
            var parts = r.Split(new[] { "," }, StringSplitOptions.None);
            foreach (var part in parts)
            {
                Parameters.Add(new MethodParameter(this, part.Replace("##", ",")));
            }
        }

        /// <summary>
        /// Gets an ordered list of <see cref="MethodParameter"/> representing the method parameters.
        /// </summary>
        public List<MethodParameter> Parameters { get; } = new List<MethodParameter>();
        /// <summary>
        /// Gets an ordered list of <see cref="TypeParameter"/> representing generic type parameters.
        /// </summary>
        public List<TypeParameter> TypeParameters { get; } = new List<TypeParameter>();
        /// <summary>
        /// Gets an ordered list of <see cref="ThrownException"/> represnting exceptions thrown by this method.
        /// </summary>
        public List<ThrownException> ThrownExceptions { get; } = new List<ThrownException>();

        /// <summary>
        /// Gets a reference to the type that contains the method.
        /// </summary>
        public Type ContainingType
        {
            get { return _containingType; }
            private set
            {
                _containingType = value;
                if (value != null)
                    if (!value.Methods.Contains(this))
                        value.Methods.Add(this);
            }
        }
    }
}