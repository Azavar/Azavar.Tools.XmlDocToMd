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
        }
        
        /// <summary>
        /// Runs the logic to generate markdown files.
        /// </summary>
        public void Render()
        {
            Console.WriteLine("Processing documentation for assembly: {0}", Model.AssemblyName);
            var files = Model.Members.Values.Where(m => (m as Model.Type) != null).Cast<Model.Type>().GroupBy(RelativeFileFor).ToDictionary(
                grp => grp.Key, grp => grp.ToArray());
            foreach (var file in files)
            {
                var filePath = Path.Combine(OutputFolderPath, file.Key.TrimStart(Path.DirectorySeparatorChar));
                Console.WriteLine("Rendering file: {0} for types:", filePath);
                var sb = new StringBuilder();
                sb.AppendLine($"## {string.Join(".", new[] { Model.AssemblyName }.Concat(file.Value[0].NamespaceStrings))}\r\n\r\n");
                foreach (var type in file.Value)
                {
                    Console.WriteLine("\t {0}", GetPresentableTypeName(type));
                    sb.AppendLine(RenderType(type));
                }
                var dirInfo = Directory.GetParent(filePath);
                dirInfo.Create();
                File.WriteAllText(filePath, sb.ToString());
            }
            Console.WriteLine("Done rendering markdown");
        }

        private string RenderType(Model.Type type)
        {
            var result = $"<a name=\"{GetUsableId(type.Id)}\"></a>\r\n" +
                         //$"### [{GetPresentableTypeName(type)}]({RootUrl + RelativeCodeFileFor(type)})\r\n\r\n" +
                         $"### {GetPresentableTypeName(type)}\r\n\r\n" +
                         $"{RenderDocumentation(type.Documentation)}\r\n\r\n";
            if (type.TypeParameters.Count > 0)
            {
                result += "| Type Parameter |Summary |\r\n";
                result += "|-|-|\r\n";
                foreach (var typeParameter in type.TypeParameters)
                {
                    result += $"|{typeParameter.Name}|{RenderDocumentation(typeParameter.Documentation)}|\r\n";
                }
                result += "\r\n\r\n";
            }
            if (type.Properties.Count > 0)
            {
                result += "#### Properties\r\n\r\n";
                foreach (var property in type.Properties)
                {
                    result += RenderProperty(property);
                }
            }
            if (type.Methods.Count > 0)
            {
                result += "#### Methods\r\n\r\n";
                foreach (var method in type.Methods)
                {
                    result += RenderMethod(method);
                }
            }
            return result;
        }

        private string RenderProperty(Property property)
        {
            var result = $"<a name=\"{GetUsableId(property.Id)}\"></a>\r\n" +
                         $"##### {property.Name}\r\n\r\n" +
                         $"{RenderDocumentation(property.Documentation)}\r\n\r\n";
            return result;
        }

        private string RenderMethod(Method method)
        {
            var result = $"<a name=\"{GetUsableId(method.Id)}\"></a>\r\n" +
                         $"##### {GetPresentableMethodName(method)}\r\n\r\n" +
                         $"{RenderDocumentation(method.Documentation)}\r\n\r\n";
            if (method.TypeParameters.Count > 0)
            {
                result += "| Type Parameter |Summary |\r\n";
                result += "|-|-|\r\n";
                foreach (var typeParameter in method.TypeParameters)
                {
                    result += $"|{typeParameter.Name}|{RenderDocumentation(typeParameter.Documentation)}|\r\n";
                }
                result += "\r\n\r\n";
            }
            if (method.Parameters.Count > 0)
            {
                result += "| Parameter | Type | Summary |\r\n";
                result += "|-|-|-|\r\n";
                foreach (var parameter in method.Parameters)
                {
                    var typeName = GetMethodParameterTypeName(parameter);
                    var typeText = GetMethodParameterPresentableTypeName(parameter);
                    if (typeName.StartsWith(method.Model.AssemblyName))
                    {
                        var id = $"T:{typeName}";
                        if (method.Model.Members.ContainsKey(id))
                        {
                            var type = method.Model.Members[id] as Model.Type;
                            if (type != null)
                                typeText = $"[{typeText}]({RootUrl + RelativeLocationFor(type).Replace(Path.DirectorySeparatorChar, '/')})";
                        }
                    }
                    result += $"|{parameter.Name}|{typeText}|{RenderDocumentation(parameter.Documentation)}|\r\n";
                }
                result += "\r\n\r\n";
            }
            return result;
        }


        private string GetUsableId(string id)
        {
            return id.Replace("`", "_").Replace("#", "_");
        }

        private string RenderDocumentation(Documentation d, bool cleanText = true)
        {
            if (d == null)
                return string.Empty;
            if (!d.SubDocumentation.Any())
            {
                if (d.DocumentationType == null)
                    return cleanText ? CleanText(d.FormattedContent) : d.FormattedContent.Trim();
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
                        return $"[{text}]({url})";
                    }
                    return "Refer to " + d.Attributes["cref"];
                }
            }
            string result;
            if (d.DocumentationType == "example")
            {
                result = string.Format(d.FormattedContent, d.SubDocumentation.Select(sub => RenderDocumentation(sub, false)).Cast<object>().ToArray());
                result = $"\r\n```{result}```";
            }
            else
            {
                result = string.Format(d.FormattedContent, d.SubDocumentation.Select(sub => RenderDocumentation(sub)).Cast<object>().ToArray());
            }
            return result;
        }

        private string CleanText(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            return Regex.Replace(text.Trim(), "\\s+", " ");
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
            return Regex.Replace(parameter.TypeName, "{.+}",
                match => "`" + match.Value.Split(new[] { "," }, StringSplitOptions.None).Length);

        }

        private string GetMethodParameterPresentableTypeName(MethodParameter parameter)
        {
            var methodLevelGenerics = Regex.Matches(parameter.TypeName, "``+(?<genericParams>\\d)");
            var typeText = parameter.TypeName.Replace("{", "<").Replace("}", ">");
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
            var classLevelGenerics = Regex.Matches(parameter.TypeName, "`+(?<genericParams>\\d)");
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

        #region Url helpers
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
            return $"{RelativeFolderFor(type)}{Path.DirectorySeparatorChar}README.md";
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
    }
}
