﻿using System;
using Azavar.Tools.XmlDocToMd.Model;


namespace Azavar.Tools.XmlDocToMd
{
    /// <summary>
    /// This tool allows developers to generate github-flavored markdown files out of C# XML documentation.
    /// How to use:
    /// 1. First use the C# compiler to generate and XML documentation fil for your code, in
    /// Visual Studio this can be done by enabling "XML documentation file" option under "Build" tab of the
    /// project properties.
    /// 2. Download/clone and build this tool
    /// 3. Run this tool in the command line with the following arguments:
    ///     1. The path to the XML documentation file
    ///     2. The folder path where you want the generated files to be stored, namespaces will generate 
    ///         subfolders starting after the default namespace
    ///     3. The home page of the github repository as it appears in the browser,
    ///         like: "https://github.com/repository-name/blob/master/"
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            var model = new XmlDocumentationModel(args[0]);
            var renderer = new MarkdownRenderer(model, args[1], args[2]);
            renderer.Render();
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}