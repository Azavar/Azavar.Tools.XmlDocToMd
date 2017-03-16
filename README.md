## Azavar.Tools.XmlDocToMd


### Namespaces


- [Azavar.Tools.XmlDocToMd.Model](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Model)


<a name="T:Azavar.Tools.XmlDocToMd.MarkdownRenderer"></a>
### MarkdownRenderer

A class that analyzes an [XmlDocumentationModel](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel) and generates markdown files.

#### Properties

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownRenderer.OutputFolderPath"></a>
##### OutputFolderPath

Gets the path to the folder where generated files will be placed.

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownRenderer.RootUrl"></a>
##### RootUrl

Gets the home page of the github repository as it appears in the browser, like: "https://github.com/repository-name/blob/master/".

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownRenderer.Model"></a>
##### Model

A representation of the XML documentation of type [XmlDocumentationModel](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel) .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownRenderer.NamespacesTree"></a>
##### NamespacesTree

Gets a dictionary with all namespaces, key represents the namspace string (without the default namespace) and value represents an array of sub-namespaces formatted as an array of strings ("Ns1.Ns2" will be {"Ns1","Ns2"}).

#### Methods

<a name="M:Azavar.Tools.XmlDocToMd.MarkdownRenderer._ctor(Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel,System.String,System.String)"></a>
##### #ctor(XmlDocumentationModel,System.String,System.String)

Initializes with required parameters an instance for a parsed model representing XML documentation for an assembly.

##### Parameters

| Parameter | Type | Summary |
|-|-|-|
|model|[XmlDocumentationModel](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel)|An [XmlDocumentationModel](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel) loaded from XML documentation file.|
|outputFolderPath|System.String|The path to the folder where generated files will be placed.|
|rootUrl|System.String|The home page of the github repository as it appears in the browser, like: "https://github.com/repository-name/blob/master/".|

<a name="M:Azavar.Tools.XmlDocToMd.MarkdownRenderer.Render"></a>
##### Render

Runs the logic to generate markdown files.


<a name="T:Azavar.Tools.XmlDocToMd.MarkdownTemplates"></a>
### MarkdownTemplates

A strongly-typed resource class, for looking up localized strings, etc.

#### Properties

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.ResourceManager"></a>
##### ResourceManager

Returns the cached ResourceManager instance used by this class.

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.Culture"></a>
##### Culture

Overrides the current thread's CurrentUICulture property for all resource lookups using this strongly typed resource class.

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.CodeBlock"></a>
##### CodeBlock

Looks up a localized string similar to `{0}`.

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.ExceptionItem"></a>
##### ExceptionItem

Looks up a localized string similar to |{0}|{1}|.

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.ExceptionsFooter"></a>
##### ExceptionsFooter

Looks up a localized string similar to .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.ExceptionsHeader"></a>
##### ExceptionsHeader

Looks up a localized string similar to ##### Exceptions | Exception | Condition | |-|-| .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.Link"></a>
##### Link

Looks up a localized string similar to [{0}]({1}).

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.MethodItem"></a>
##### MethodItem

Looks up a localized string similar to <a name="{0}"></a> ##### {1} {2} {3}{4}{5}.

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.MethodParameterItem"></a>
##### MethodParameterItem

Looks up a localized string similar to |{0}|{1}|{2}| .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.MethodParametersFooter"></a>
##### MethodParametersFooter

Looks up a localized string similar to .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.MethodParametersHeader"></a>
##### MethodParametersHeader

Looks up a localized string similar to ##### Parameters | Parameter | Type | Summary | |-|-|-| .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.MethodsFooter"></a>
##### MethodsFooter

Looks up a localized string similar to .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.MethodsHeader"></a>
##### MethodsHeader

Looks up a localized string similar to #### Methods .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.NamspaceItem"></a>
##### NamspaceItem

Looks up a localized string similar to - [{0}]({1}) .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.NamspacesTitle"></a>
##### NamspacesTitle

Looks up a localized string similar to ### Namespaces .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.PropertiesFooter"></a>
##### PropertiesFooter

Looks up a localized string similar to .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.PropertiesHeader"></a>
##### PropertiesHeader

Looks up a localized string similar to #### Properties .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.PropertyItem"></a>
##### PropertyItem

Looks up a localized string similar to <a name="{0}"></a> ##### {1} {2} .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.Title"></a>
##### Title

Looks up a localized string similar to ## {0} .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.TypeFormat"></a>
##### TypeFormat

Looks up a localized string similar to <a name="{0}"></a> ### {1} {2} {3}{4}{5}.

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.TypeParameterItem"></a>
##### TypeParameterItem

Looks up a localized string similar to |{0}|{1}| .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.TypeParametersFooter"></a>
##### TypeParametersFooter

Looks up a localized string similar to .

<a name="P:Azavar.Tools.XmlDocToMd.MarkdownTemplates.TypeParametersHeader"></a>
##### TypeParametersHeader

Looks up a localized string similar to ##### Generic Type Parameters | Type Parameter |Summary | |-|-| .


<a name="T:Azavar.Tools.XmlDocToMd.Program"></a>
### Program

This tool allows developers to generate github-flavored markdown files out of C# XML documentation. How to use: 1. First use the C# compiler to generate and XML documentation fil for your code, in Visual Studio this can be done by enabling "XML documentation file" option under "Build" tab of the project properties. 2. Download/clone and build this tool 3. Run this tool in the command line with the following arguments: 1. The path to the XML documentation file 2. The folder path where you want the generated files to be stored, namespaces will generate subfolders starting after the default namespace 3. The home page of the github repository as it appears in the browser, like: "https://github.com/repository-name/blob/master/"


