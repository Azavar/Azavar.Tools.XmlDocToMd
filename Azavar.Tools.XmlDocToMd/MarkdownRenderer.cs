using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Azavar.Tools.XmlDocToMd.Model;

namespace Azavar.Tools.XmlDocToMd
{
    /// <summary>
    /// A class that analyzes an <see cref="XmlDocumentationModel"/> and generates markdown files.
    /// </summary>
    public class MarkdownRenderer
    {
        /// <summary>
        /// Initializes with required parameters an instance for a parsed model representing XML documentation for an assembly.
        /// </summary>
        /// <param name="model">An <see cref="XmlDocumentationModel"/> loaded from XML documentation file.</param>
        /// <param name="outputFolderPath">The path to the folder where generated files will be placed.</param>
        /// <param name="rootUrl">The home page of the github repository as it appears in the browser, like: "https://github.com/repository-name/blob/master/".</param>
        public MarkdownRenderer(XmlDocumentationModel model, string outputFolderPath, string rootUrl)
        {
            OutputFolderPath = outputFolderPath;
            RootUrl = rootUrl;
            Model = model;
            var allNamespaces = model.Members.Select(p => p.Value)
                .Where(m => m is Model.Type)
                .Cast<Model.Type>()
                .Select(t => t.NamespaceStrings.Any() ? "." + string.Join(".", t.NamespaceStrings) : "").Distinct().ToArray();
            NamespacesTree = allNamespaces
                .ToDictionary(
                    n => n.TrimStart('.'),
                    n => allNamespaces.Where(n2 => IsSubNamespace(n, n2)).Select(n2 => n2.Split(new[] { "." }, StringSplitOptions.RemoveEmptyEntries)).ToArray()
                );
        }

        #region Rendering methods

        /// <summary>
        /// Runs the logic to generate markdown files for all types in the documentation model.
        /// </summary>
        public void RenderAllTypes()
        {
            RenderSelectedTypes(null);
        }

        /// <summary>
        /// Runs the logic to generate markdown files for selected types only.
        /// </summary>
        /// <param name="types">The selected types to render, passing null will generate for all types in the documentation model.</param>
        public void RenderSelectedTypes(IEnumerable<Model.Type> types)
        {
            Console.WriteLine(@"Processing documentation for assembly: {0}", Model.AssemblyName);
            var typeList = Model.Members.Values.Where(m => (m as Model.Type) != null).Cast<Model.Type>();
            if (types != null)
                typeList = typeList.Intersect(types);
            var files = typeList.GroupBy(RelativeFileFor).ToDictionary(grp => grp.Key, grp => grp.ToArray());
            foreach (var file in files)
            {
                var filePath = Path.Combine(OutputFolderPath, file.Key.TrimStart(Path.DirectorySeparatorChar));
                Console.WriteLine(@"Rendering file: {0} for types:", filePath);
                var sb = new StringBuilder();
                var namespaceString = string.Join(".", file.Value[0].NamespaceStrings);

                sb.AppendLine(string.Format(MarkdownTemplates.Title, GetFullNamespaceString(namespaceString)));
                if (NamespacesTree[namespaceString].Any())
                {
                    sb.AppendLine(MarkdownTemplates.NamspacesTitle);
                    foreach (var @namespace in NamespacesTree[namespaceString])
                    {
                        sb.AppendLine(RenderNamespace(@namespace));
                    }
                }
                sb.AppendLine(MarkdownTemplates.TOCTitle);
                foreach (var type in file.Value)
                {
                    sb.Append(RenderTocType(type));
                }
                foreach (var type in file.Value)
                {
                    Console.WriteLine(@"	 {0}", GetPresentableTypeName(type));
                    sb.AppendLine(RenderType(type));
                    sb.AppendLine(string.Format(MarkdownTemplates.Link, "\u2191 Top", "#TOC"));
                }
                var dirInfo = Directory.GetParent(filePath);
                dirInfo.Create();
                File.WriteAllText(filePath, sb.ToString());
            }
            Console.WriteLine(@"Done rendering markdown");
        }

        private string RenderNamespace(string[] ns)
        {
            return string.Format(MarkdownTemplates.NamspaceItem, GetFullNamespaceString(string.Join(".", ns)),
                RootUrl + RelativeFolderForNamespace(ns));
        }

        private string RenderTocType(Model.Type type)
        {
            var id = GetUsableId(type.Id);
            var name = GetPresentableTypeName(type);
            var result = "- Type " + string.Format(MarkdownTemplates.Link, name, "#" + id);
            if (type.Methods.Any() || type.Properties.Any())
            {
                result += " (";
                if (type.Properties.Any())
                {
                    result += string.Format(MarkdownTemplates.Link, "Properties", "#" + id + "_Properties");
                    if (type.Methods.Any())
                    {
                        result += ", ";
                    }
                }
                if (type.Methods.Any())
                {
                    result += string.Format(MarkdownTemplates.Link, "Methods", "#" + id + "_Methods");
                }
                result += ")\r\n";
            }
            return result;
        }

        private string RenderType(Model.Type type)
        {
            return string.Format(MarkdownTemplates.TypeFormat,
                GetUsableId(type.Id),
                GetPresentableTypeName(type),
                RenderDocumentation(type.Documentation),
                RenderTypeParameters(type.TypeParameters),
                RenderProperties(type.Properties),
                RenderMethods(type.Methods)
            );
        }

        private string RenderTypeParameters(List<TypeParameter> typeParameters)
        {
            var result = string.Empty;
            if (typeParameters.Any())
            {
                result += MarkdownTemplates.TypeParametersHeader;
                foreach (var typeParameter in typeParameters)
                {
                    result += string.Format(MarkdownTemplates.TypeParameterItem, typeParameter.Name,
                        RenderDocumentation(typeParameter.Documentation));
                }
                result += MarkdownTemplates.TypeParametersFooter;
            }
            return result;
        }

        private string RenderProperties(List<Property> properties)
        {
            var result = string.Empty;
            if (properties.Any())
            {
                result += string.Format(MarkdownTemplates.PropertiesHeader, GetUsableId(properties.First().ContainingType.Id));
                foreach (var property in properties)
                {
                    result += string.Format(MarkdownTemplates.PropertyItem,
                        GetUsableId(property.Id),
                        property.Name,
                        RenderDocumentation(property.Documentation)
                    );
                }
                result += MarkdownTemplates.PropertiesFooter;
            }
            return result;
        }

        private string RenderMethods(List<Method> methods)
        {
            var result = string.Empty;
            if (methods.Any())
            {
                result += string.Format(MarkdownTemplates.MethodsHeader, GetUsableId(methods.First().ContainingType.Id));
                foreach (var method in methods)
                {
                    result += RenderMethod(method);
                }
                result += MarkdownTemplates.MethodsFooter;
            }
            return result;
        }

        private string RenderMethod(Method method)
        {
            return string.Format(MarkdownTemplates.MethodItem,
                GetUsableId(method.Id),
                GetPresentableMethodName(method),
                RenderDocumentation(method.Documentation),
                RenderTypeParameters(method.TypeParameters),
                RenderMethodParameters(method.Parameters),
                RenderExceptions(method.ThrownExceptions)
            );
        }

        private string RenderMethodParameters(List<MethodParameter> methodParameters)
        {
            var result = string.Empty;
            if (methodParameters.Any())
            {
                result += MarkdownTemplates.MethodParametersHeader;
                foreach (var parameter in methodParameters)
                {
                    var typeName = GetMethodParameterTypeName(parameter);
                    var typeText = GetMethodParameterPresentableTypeName(parameter);
                    if (typeName.StartsWith(Model.AssemblyName))
                    {
                        var id = $"T:{typeName}";
                        if (Model.Members.ContainsKey(id))
                        {
                            var type = Model.Members[id] as Model.Type;
                            if (type != null)
                                typeText = string.Format(MarkdownTemplates.Link,
                                    typeText,
                                    RootUrl + RelativeLocationFor(type).Replace(Path.DirectorySeparatorChar, '/')
                                );
                        }
                    }
                    result += string.Format(MarkdownTemplates.MethodParameterItem, parameter.Name, typeText,
                        RenderDocumentation(parameter.Documentation));
                }
                result += MarkdownTemplates.MethodParametersFooter;
            }
            return result;
        }

        private string RenderDocumentation(Documentation d, bool cleanText = true)
        {
            if (d == null)
                return string.Empty;
            if (!d.SubDocumentation.Any())
            {
                if (d.DocumentationType == null)
                    return cleanText ? CleanText(d.FormattedContent) : d.FormattedContent;
                if (d.DocumentationType == "see")
                {
                    var referenced = d.Attributes["cref"];
                    if (Model.Members.ContainsKey(referenced))
                    {
                        var member = Model.Members[referenced];
                        var url = RootUrl + RelativeLocationFor(member).Replace(Path.DirectorySeparatorChar, '/');
                        var text = member.Name;
                        var type = member as Model.Type;
                        if (type != null)
                            text = GetPresentableTypeName(type);
                        var method = member as Method;
                        if (method != null)
                            text = GetPresentableMethodName(method);
                        return string.Format(MarkdownTemplates.Link, text, url);
                    }
                    var typeName = d.Attributes["cref"].Substring(2); // remove "T:" prefix
                    return GetPresentableUnknownTypeName(typeName);
                }
            }
            string result;
            if (d.DocumentationType == "example")
            {
                result = string.Format(d.FormattedContent, d.SubDocumentation.Select(sub => RenderDocumentation(sub, false)).Cast<object>().ToArray());
                result = string.Format(MarkdownTemplates.CodeBlock, result);
            }
            else
            {
                result = string.Format(d.FormattedContent, d.SubDocumentation.Select(sub => RenderDocumentation(sub)).Cast<object>().ToArray());
            }
            return result;
        }

        private string RenderExceptions(List<ThrownException> thrownExceptions)
        {
            var result = string.Empty;
            if (thrownExceptions.Any())
            {
                result += MarkdownTemplates.ExceptionsHeader;
                foreach (var thrownException in thrownExceptions)
                {
                    string typeText;
                    var typeId = $"T:{thrownException.ExceptionClassId}";
                    if (Model.Members.ContainsKey(typeId))
                    {
                        var type = Model.Members[typeId] as Model.Type;
                        typeText = string.Format(MarkdownTemplates.Link, GetPresentableTypeName(type),
                            RootUrl + RelativeLocationFor(type));
                    }
                    else
                    {
                        typeText = GetPresentableUnknownTypeName(thrownException.ExceptionClassId);
                    }
                    result += string.Format(MarkdownTemplates.ExceptionItem,
                        typeText,
                        RenderDocumentation(thrownException.Documentation)
                    );
                }
                result += MarkdownTemplates.ExceptionsFooter;
            }
            return result;
        }
        #endregion

        #region String cleaning
        private string GetFullNamespaceString(string namespaceString)
        {
            return $"{Model.AssemblyName}{(string.IsNullOrWhiteSpace(namespaceString) ? string.Empty : ".")}{namespaceString}";
        }

        private string GetUsableId(string id)
        {
            return id.Replace("`", "_").Replace("#", "_");
        }

        private string CleanText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            return text;
        }

        private string GetPresentableTypeName(Model.Type type)
        {
            var match = Regex.Match(type.Name, "`+(?<genericParams>\\d)");
            if (!match.Success) return type.Name;
            var paramTypes = new List<string>();
            var genericParamsCount = int.Parse(match.Groups["genericParams"].Value);
            for (int i = 0; i < genericParamsCount; i++)
            {
                if (i < type.TypeParameters.Count)
                {
                    paramTypes.Add(type.TypeParameters[i].Name);
                }
                else
                {
                    paramTypes.Add("T" + (i + 1));
                }
            }
            return type.Name.Replace(match.Value, $"<{string.Join(",", paramTypes)}>");
        }

        private string GetPresentableUnknownTypeName(string name)
        {
            if (name.StartsWith("T:"))
                name = name.Substring(2);
            var match = Regex.Match(name, "`+(?<genericParams>\\d)");
            if (!match.Success) return name;
            var paramTypes = new List<string>();
            var genericParamsCount = int.Parse(match.Groups["genericParams"].Value);
            if (genericParamsCount > 1)
                for (var i = 0; i < genericParamsCount; i++)
                    paramTypes.Add("T" + (i + 1));
            else
                paramTypes.Add("T");
            return name.Replace(match.Value, $"<{string.Join(",", paramTypes)}>");
        }
        private string GetPresentableMethodName(Method method)
        {
            var result = method.Name;
            var c = 1;
            var match = Regex.Match(method.Name, "``+(?<genericParams>\\d)");
            if (match.Success)
            {
                var paramTypes = new List<string>();
                var genericParamsCount = int.Parse(match.Groups["genericParams"].Value);
                for (var i = 0; i < genericParamsCount; i++)
                {
                    if (i < method.TypeParameters.Count)
                    {
                        paramTypes.Add(method.TypeParameters[i].Name);
                    }
                    else
                    {
                        paramTypes.Add("T" + (c + 1));
                    }
                    c++;
                }
                result = method.Name.Replace(match.Value, $"<{string.Join(",", paramTypes)}>");
            }
            match = Regex.Match(method.Name, "`+(?<genericParams>\\d)");
            if (match.Success)
            {
                var paramTypes = new List<string>();
                var genericParamsCount = int.Parse(match.Groups["genericParams"].Value);
                for (var i = 0; i < genericParamsCount; i++)
                {
                    if (i < method.ContainingType.TypeParameters.Count)
                    {
                        paramTypes.Add(method.ContainingType.TypeParameters[i].Name);
                    }
                    else
                    {
                        paramTypes.Add("T" + (c + 1));
                    }
                    c++;
                }
                result = method.Name.Replace(match.Value, $"<{string.Join(",", paramTypes)}>");
            }

            if (method.Parameters.Any())
            {
                result += "(";
                var types = new List<string>();
                foreach (var parameter in method.Parameters)
                    types.Add(GetMethodParameterPresentableTypeName(parameter));
                result += string.Join(",", types);
                result += ")";
            }
            return result;
        }

        private string GetMethodParameterTypeName(MethodParameter parameter)
        {
            return Regex.Replace(parameter.TypeId, "{.+}",
                match => "`" + match.Value.Split(new[] { "," }, StringSplitOptions.None).Length);

        }

        private string GetMethodParameterPresentableTypeName(MethodParameter parameter)
        {
            var methodLevelGenerics = Regex.Matches(parameter.TypeId, "``+(?<genericParams>\\d)");
            var typeText = parameter.TypeId.Replace("{", "<").Replace("}", ">");
            var c = 1;
            if (methodLevelGenerics.Count > 0)
            {
                foreach (Match methodLevelGeneric in methodLevelGenerics)
                {
                    var idx = int.Parse(methodLevelGeneric.Groups["genericParams"].Value);
                    if (idx < parameter.Method.TypeParameters.Count)
                        typeText = typeText.Replace(methodLevelGeneric.Value,
                            parameter.Method.TypeParameters[idx].Name);
                    else
                        typeText = typeText.Replace(methodLevelGeneric.Value, "T" + c);
                    c++;
                }
            }
            var classLevelGenerics = Regex.Matches(parameter.TypeId, "`+(?<genericParams>\\d)");
            if (classLevelGenerics.Count > 0)
            {
                foreach (Match classLevelGeneric in classLevelGenerics)
                {
                    var idx = int.Parse(classLevelGeneric.Groups["genericParams"].Value);
                    if (idx < parameter.Method.ContainingType.TypeParameters.Count)
                        typeText = typeText.Replace(classLevelGeneric.Value,
                            parameter.Method.ContainingType.TypeParameters[idx].Name);
                    else
                        typeText = typeText.Replace(classLevelGeneric.Value, "T" + c);
                    c++;
                }
            }
            if (typeText.StartsWith(parameter.Method.Model.AssemblyName))
                typeText = typeText.Split(new[] { "." }, StringSplitOptions.None).Last();
            return typeText;
        }
        #endregion

        #region Url helpers
        private string RelativeFolderForNamespace(string[] ns)
        {
            return string.Join("/", ns);
        }


        private string RelativeFolderFor(Model.Type type)
        {
            return string.Join(Path.DirectorySeparatorChar.ToString(), type.NamespaceStrings);
        }

        private string RelativeLocationFor(Member member)
        {
            var type = member as Model.Type;
            if (type != null)
                return RelativeLocationFor(type);
            var method = member as Method;
            if (method != null)
                return RelativeLocationFor(method);
            var property = member as Property;
            if (property != null)
                return RelativeLocationFor(property);
            throw new InvalidOperationException();
        }

        private string RelativeFileFor(Model.Type type)
        {
            return $"{RelativeFolderFor(type)}{Path.DirectorySeparatorChar}Classes.md";
        }

        //private string RelativeCodeFileFor(Type type)
        //{
        //    return $"{RelativeFolderFor(type)}/{Regex.Replace(type.Name, "`\\d+", "")}.cs";
        //}
        private string RelativeLocationFor(Model.Type type)
        {
            return $"{RelativeFileFor(type)}#{GetUsableId(type.Id)}";
        }

        private string RelativeLocationFor(Method method)
        {
            return $"{RelativeFileFor(method.ContainingType)}#{GetUsableId(method.Id)}";
        }

        private string RelativeLocationFor(Property property)
        {
            return $"{RelativeFileFor(property.ContainingType)}#{GetUsableId(property.Id)}";
        }
        #endregion

        #region Other helper methods
        private bool IsSubNamespace(string n1, string n2)
        {
            return n2.StartsWith(n1) && (n2.Count(c => c.Equals('.')) == n1.Count(c => c.Equals('.')) + 1);
        }

        #endregion

        #region Properties
        /// <summary>
        /// Gets the path to the folder where generated files will be placed.
        /// </summary>
        public string OutputFolderPath { get; }
        /// <summary>
        /// Gets the home page of the github repository as it appears in the browser, like: "https://github.com/repository-name/blob/master/".
        /// </summary>
        public string RootUrl { get; }
        /// <summary>
        /// A representation of the XML documentation of type <see cref="XmlDocumentationModel"/>.
        /// </summary>
        public XmlDocumentationModel Model { get; }
        /// <summary>
        /// Gets a dictionary with all namespaces, key represents the namspace string (without the default namespace)
        /// and value represents an array of sub-namespaces formatted as an array of strings ("Ns1.Ns2" will be {"Ns1","Ns2"}).
        /// </summary>
        public Dictionary<string, string[][]> NamespacesTree { get; }
        #endregion
    }
}
