## Azavar.Tools.XmlDocToMd


### Namespaces


- [Azavar.Tools.XmlDocToMd.Model](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model)


<a name="TOC"></a>
### Table of contents
- Type [MarkdownRenderer](#T:Azavar.Tools.XmlDocToMd.MarkdownRenderer) ([Properties](#T:Azavar.Tools.XmlDocToMd.MarkdownRenderer_Properties), [Methods](#T:Azavar.Tools.XmlDocToMd.MarkdownRenderer_Methods))
- Type [Program](#T:Azavar.Tools.XmlDocToMd.Program)<a name="T:Azavar.Tools.XmlDocToMd.MarkdownRenderer"></a>
### MarkdownRenderer

  A class that analyzes an  [XmlDocumentationModel](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/Classes.md#T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel)  and generates markdown files.



<a name="T:Azavar.Tools.XmlDocToMd.MarkdownRenderer_Properties"></a>
#### Properties

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownRenderer.OutputFolderPath"></a>
##### OutputFolderPath

  Gets the path to the folder where generated files will be placed.



<a name="P:Azavar.Tools.XmlDocToMd.MarkdownRenderer.RootUrl"></a>
##### RootUrl

  Gets the home page of the github repository as it appears in the browser, like: "https://github.com/repository-name/blob/master/".



<a name="P:Azavar.Tools.XmlDocToMd.MarkdownRenderer.Model"></a>
##### Model

  A representation of the XML documentation of type  [XmlDocumentationModel](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/Classes.md#T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel) .



<a name="P:Azavar.Tools.XmlDocToMd.MarkdownRenderer.NamespacesTree"></a>
##### NamespacesTree

  Gets a dictionary with all namespaces, key represents the namspace string (without the default namespace)

and value represents an array of sub-namespaces formatted as an array of strings ("Ns1.Ns2" will be {"Ns1","Ns2"}).



<a name="T:Azavar.Tools.XmlDocToMd.MarkdownRenderer_Methods"></a>
#### Methods

<a name="M:Azavar.Tools.XmlDocToMd.MarkdownRenderer._ctor(Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel,System.String,System.String)"></a>
##### #ctor(XmlDocumentationModel,System.String,System.String)

  Initializes with required parameters an instance for a parsed model representing XML documentation for an assembly.



##### Parameters

| Parameter | Type | Summary |
|-|-|-|
|model|[XmlDocumentationModel](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/Classes.md#T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel)|An  [XmlDocumentationModel](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/Classes.md#T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel)  loaded from XML documentation file.|
|outputFolderPath|System.String|The path to the folder where generated files will be placed.|
|rootUrl|System.String|The home page of the github repository as it appears in the browser, like: "https://github.com/repository-name/blob/master/".|

<a name="M:Azavar.Tools.XmlDocToMd.MarkdownRenderer.RenderAllTypes"></a>
##### RenderAllTypes

  Runs the logic to generate markdown files for all types in the documentation model.



<a name="M:Azavar.Tools.XmlDocToMd.MarkdownRenderer.RenderSelectedTypes(System.Collections.Generic.IEnumerable{Azavar.Tools.XmlDocToMd.Model.Type})"></a>
##### RenderSelectedTypes(System.Collections.Generic.IEnumerable<Azavar.Tools.XmlDocToMd.Model.Type>)

  Runs the logic to generate markdown files for selected types only.



##### Parameters

| Parameter | Type | Summary |
|-|-|-|
|types|System.Collections.Generic.IEnumerable<Azavar.Tools.XmlDocToMd.Model.Type>|The selected types to render, passing null will generate for all types in the documentation model.|


[↑ Top](#TOC)
<a name="T:Azavar.Tools.XmlDocToMd.Program"></a>
### Program

  This tool allows developers to generate github-flavored markdown files out of C# XML documentation.

How to use:

1. First use the C# compiler to generate and XML documentation file for your code, this can be done in Visual Studio by enabling "XML documentation file" option under "Build" tab of the project properties.

2. Download/clone and build this tool

3. Run this tool in the command line with the following arguments:

    - The path to the XML documentation file

    - The folder path where you want the generated files to be stored, namespaces will generate subfolders starting after the default namespace

    - The home page of the github repository as it appears in the browser, like: "https://github.com/repository-name/blob/master/"




[↑ Top](#TOC)
