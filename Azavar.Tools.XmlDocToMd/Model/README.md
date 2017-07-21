## Azavar.Tools.XmlDocToMd.Model


<a name="T:Azavar.Tools.XmlDocToMd.Model.Documentation"></a>
### Documentation

  Represents a subtree of the XML documentation file that contains user comments, like  `<summary>`  element,

the tag and all its contents will be loaded in a recursive structure.



#### Properties

<a name="P:Azavar.Tools.XmlDocToMd.Model.Documentation.LeadingSpaceLength"></a>
##### LeadingSpaceLength

  The number of leading space chars.



<a name="P:Azavar.Tools.XmlDocToMd.Model.Documentation.FormattedContent"></a>
##### FormattedContent

  Gets the inner-XML of the element as a format string. Placeholders, if any, correspond to items in  [SubDocumentation](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#P:Azavar.Tools.XmlDocToMd.Model.Documentation.SubDocumentation) .



<a name="P:Azavar.Tools.XmlDocToMd.Model.Documentation.Attributes"></a>
##### Attributes

  Gets a key-value representation of the XML attributes that may be useful in rendering, 

like {"cref", "T:System.Enum"} for  `<see cref="T:System.Enum"/>` .



<a name="P:Azavar.Tools.XmlDocToMd.Model.Documentation.SubDocumentation"></a>
##### SubDocumentation

  Gets an ordered list of Documenaton objects representing XML node childs and correspond to placeholders in  [FormattedContent](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#P:Azavar.Tools.XmlDocToMd.Model.Documentation.FormattedContent) .



<a name="P:Azavar.Tools.XmlDocToMd.Model.Documentation.DocumentationType"></a>
##### DocumentationType

  Gets the tag name represented by this object.



#### Methods

<a name="M:Azavar.Tools.XmlDocToMd.Model.Documentation._ctor(System.Xml.XmlNode)"></a>
##### #ctor(System.Xml.XmlNode)

  Initializes an instance with an XML node.



##### Parameters

| Parameter | Type | Summary |
|-|-|-|
|node|System.Xml.XmlNode|The XML node representing the XML subtree to parse.|

##### Exceptions

| Exception | Condition |
|-|-|
|System.ArgumentNullException|The provided XML node is null.|
<a name="M:Azavar.Tools.XmlDocToMd.Model.Documentation._ctor(System.Xml.XmlNode,System.Int32)"></a>
##### #ctor(System.Xml.XmlNode,System.Int32)

  Initializes an instance with an XML node and a leading space.



##### Parameters

| Parameter | Type | Summary |
|-|-|-|
|node|System.Xml.XmlNode|The XML node representing the XML subtree to parse.|
|leadingSpace|System.Int32|The number of space chars leading the first line of the related documentation.|

##### Exceptions

| Exception | Condition |
|-|-|
|System.ArgumentNullException|The provided XML node is null.|

<a name="T:Azavar.Tools.XmlDocToMd.Model.Member"></a>
### Member

  Defines the base for objects represnting "member" tag in XML docuemntation.



#### Properties

<a name="P:Azavar.Tools.XmlDocToMd.Model.Member.Model"></a>
##### Model

  Gets the  [XmlDocumentationModel](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel)  containing this member.



<a name="P:Azavar.Tools.XmlDocToMd.Model.Member.Id"></a>
##### Id

  Gets the unique ID of the member, same as the value of attibute "name" in the XML documentation.



<a name="P:Azavar.Tools.XmlDocToMd.Model.Member.Name"></a>
##### Name

  Gets the name of the member.



<a name="P:Azavar.Tools.XmlDocToMd.Model.Member.Documentation"></a>
##### Documentation

  Gets the  [Documentation](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.Documentation)  object associated with the member. 



#### Methods

<a name="M:Azavar.Tools.XmlDocToMd.Model.Member._ctor(Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel,System.Xml.XmlNode)"></a>
##### #ctor(XmlDocumentationModel,System.Xml.XmlNode)

  Initializes a member with its parent model and the XML node is represents.



##### Parameters

| Parameter | Type | Summary |
|-|-|-|
|model|[XmlDocumentationModel](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel)|The parent model containing this member.|
|node|System.Xml.XmlNode|The XML node represented by this member.|

##### Exceptions

| Exception | Condition |
|-|-|
|System.ArgumentNullException|node is null.||System.InvalidOperationException|Required XML attribute/element is missing.|

<a name="T:Azavar.Tools.XmlDocToMd.Model.Method"></a>
### Method

  Represents a member element containing details about a method or constructor.



#### Properties

<a name="P:Azavar.Tools.XmlDocToMd.Model.Method.Parameters"></a>
##### Parameters

  Gets an ordered list of  [MethodParameter](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.MethodParameter)  representing the method parameters.



<a name="P:Azavar.Tools.XmlDocToMd.Model.Method.TypeParameters"></a>
##### TypeParameters

  Gets an ordered list of  [TypeParameter](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.TypeParameter)  representing generic type parameters.



<a name="P:Azavar.Tools.XmlDocToMd.Model.Method.ThrownExceptions"></a>
##### ThrownExceptions

  Gets an ordered list of  [ThrownException](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.ThrownException)  represnting exceptions thrown by this method.



<a name="P:Azavar.Tools.XmlDocToMd.Model.Method.ContainingType"></a>
##### ContainingType

  Gets a reference to the type that contains the method.



#### Methods

<a name="M:Azavar.Tools.XmlDocToMd.Model.Method._ctor(Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel,System.Xml.XmlNode)"></a>
##### #ctor(XmlDocumentationModel,System.Xml.XmlNode)

  Initializes a method with its parent model and the XML node is represents.



##### Parameters

| Parameter | Type | Summary |
|-|-|-|
|model|[XmlDocumentationModel](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel)|The parent model containing this method.|
|node|System.Xml.XmlNode|The XML node represented by this method.|

##### Exceptions

| Exception | Condition |
|-|-|
|System.FormatException|The name attribute has an invalid format.|

<a name="T:Azavar.Tools.XmlDocToMd.Model.ThrownException"></a>
### ThrownException

  Represents an exception element for a  [Method](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.Method) .



#### Properties

<a name="P:Azavar.Tools.XmlDocToMd.Model.ThrownException.ExceptionClassId"></a>
##### ExceptionClassId

  Gets the ID of the exception class



<a name="P:Azavar.Tools.XmlDocToMd.Model.ThrownException.Documentation"></a>
##### Documentation

  Gets the  [Documentation](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.Documentation)  object associated with this ThrownException.



#### Methods

<a name="M:Azavar.Tools.XmlDocToMd.Model.ThrownException._ctor(System.Xml.XmlNode)"></a>
##### #ctor(System.Xml.XmlNode)

  Initializes a ThrownException instance with the XML node it represents.



##### Parameters

| Parameter | Type | Summary |
|-|-|-|
|node|System.Xml.XmlNode|The XML node represented.|

##### Exceptions

| Exception | Condition |
|-|-|
|System.ArgumentNullException|XML node is null.||System.InvalidOperationException|Required XML attribute/element is missing.|

<a name="T:Azavar.Tools.XmlDocToMd.Model.MethodParameter"></a>
### MethodParameter

  Represents a method parameter.



#### Properties

<a name="P:Azavar.Tools.XmlDocToMd.Model.MethodParameter.Name"></a>
##### Name

  Gets or sets the name of the parameter.



<a name="P:Azavar.Tools.XmlDocToMd.Model.MethodParameter.Method"></a>
##### Method

  Gets the parent  [Method](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.Method) .



<a name="P:Azavar.Tools.XmlDocToMd.Model.MethodParameter.TypeId"></a>
##### TypeId

  Gets the ID of the type as it appears in the method ID.



<a name="P:Azavar.Tools.XmlDocToMd.Model.MethodParameter.Documentation"></a>
##### Documentation

  Gets or sets the  [Documentation](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.Documentation)  object associated with the parameter.



#### Methods

<a name="M:Azavar.Tools.XmlDocToMd.Model.MethodParameter._ctor(Azavar.Tools.XmlDocToMd.Model.Method,System.String)"></a>
##### #ctor(Method,System.String)

  Initializes a method parameter with its parent method and its type ID.



##### Parameters

| Parameter | Type | Summary |
|-|-|-|
|method|[Method](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.Method)|The parent method.|
|typeId|System.String|The ID of the type as it appears in the method ID.|

##### Exceptions

| Exception | Condition |
|-|-|
|System.ArgumentNullException|Either parameter is null.|

<a name="T:Azavar.Tools.XmlDocToMd.Model.TypeParameter"></a>
### TypeParameter

  Represets 



#### Properties

<a name="P:Azavar.Tools.XmlDocToMd.Model.TypeParameter.Name"></a>
##### Name

  Gets the name of the type parameter.



<a name="P:Azavar.Tools.XmlDocToMd.Model.TypeParameter.Documentation"></a>
##### Documentation

  Gets the  [Documentation](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.Documentation)  object associated with the type parameter. 



#### Methods

<a name="M:Azavar.Tools.XmlDocToMd.Model.TypeParameter._ctor(System.Xml.XmlNode)"></a>
##### #ctor(System.Xml.XmlNode)

  Initializes a TypeParameter instance with the XML node it represets.



##### Parameters

| Parameter | Type | Summary |
|-|-|-|
|node|System.Xml.XmlNode|The XML node being represented.|

##### Exceptions

| Exception | Condition |
|-|-|
|System.ArgumentNullException|node is null.||System.InvalidOperationException|Required XML attribute/element is missing.|

<a name="T:Azavar.Tools.XmlDocToMd.Model.Property"></a>
### Property

  Represents a member element containing details about a property.



#### Properties

<a name="P:Azavar.Tools.XmlDocToMd.Model.Property.ContainingType"></a>
##### ContainingType

  Gets a reference to the type that contains the property.



#### Methods

<a name="M:Azavar.Tools.XmlDocToMd.Model.Property._ctor(Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel,System.Xml.XmlNode)"></a>
##### #ctor(XmlDocumentationModel,System.Xml.XmlNode)

  Initializes a property with its parent model and the XML node is represents.



##### Parameters

| Parameter | Type | Summary |
|-|-|-|
|model|[XmlDocumentationModel](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel)|The parent model containing this property.|
|node|System.Xml.XmlNode|The XML node represented by this property.|

##### Exceptions

| Exception | Condition |
|-|-|
|System.FormatException|The name attribute has an invalid format.|

<a name="T:Azavar.Tools.XmlDocToMd.Model.Type"></a>
### Type

  Represents a member element containing details about a type.



#### Properties

<a name="P:Azavar.Tools.XmlDocToMd.Model.Type.ContainingType"></a>
##### ContainingType

  Gets a reference to the type that contains this type if it is a nested type, otherwise null.



<a name="P:Azavar.Tools.XmlDocToMd.Model.Type.NamespaceStrings"></a>
##### NamespaceStrings

  Gets the namespace in which this type is define, formatted as an array of strings ("Ns1.Ns2" will be {"Ns1","Ns2"}).



<a name="P:Azavar.Tools.XmlDocToMd.Model.Type.TypeParameters"></a>
##### TypeParameters

  Gets an ordered list of  [TypeParameter](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.TypeParameter)  representing generic type parameters.



<a name="P:Azavar.Tools.XmlDocToMd.Model.Type.Methods"></a>
##### Methods

  Gets a list of  [Method](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.Method)  representing all docuemnted methods and costructors of this type.



<a name="P:Azavar.Tools.XmlDocToMd.Model.Type.Properties"></a>
##### Properties

  Gets a list of  [Property](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.Property)  representing all documented properties of this type.



<a name="P:Azavar.Tools.XmlDocToMd.Model.Type.NestedTypes"></a>
##### NestedTypes

  Gets a list of nested types defined within this type.



#### Methods

<a name="M:Azavar.Tools.XmlDocToMd.Model.Type._ctor(Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel,System.Xml.XmlNode)"></a>
##### #ctor(XmlDocumentationModel,System.Xml.XmlNode)

  Initializes a type with its parent model and the XML node is represents.



##### Parameters

| Parameter | Type | Summary |
|-|-|-|
|model|[XmlDocumentationModel](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel)|The parent model containing this type.|
|node|System.Xml.XmlNode|The XML node represented by this type.|

##### Exceptions

| Exception | Condition |
|-|-|
|System.FormatException|The name attribute has an invalid format.|
<a name="M:Azavar.Tools.XmlDocToMd.Model.Type.ToString"></a>
##### ToString




<a name="T:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel"></a>
### XmlDocumentationModel

  Represents a parsed model of the contents of an XML documentation file generated by Visual Studio as desribed here: https://msdn.microsoft.com/en-us/library/b2s063f7.aspx.

This class doesn't fully make use of all capabilities of XML documentation.



#### Properties

<a name="P:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel.AssemblyName"></a>
##### AssemblyName

  Gets the assembly name for which the XML docuemntation file was parsed.



<a name="P:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel.Members"></a>
##### Members

  Gets a dictionary of all  [Member](https://github.com/Azavar/Azavar.Tools.XmlDocToMd/blob/master/Azavar.Tools.XmlDocToMd/Model/README.md#T:Azavar.Tools.XmlDocToMd.Model.Member)  instances within the model where the key is the member ID and he value is the object reference.



#### Methods

<a name="M:Azavar.Tools.XmlDocToMd.Model.XmlDocumentationModel._ctor(System.String)"></a>
##### #ctor(System.String)

  Loads and parses an XML documentation file by it's local path.



##### Parameters

| Parameter | Type | Summary |
|-|-|-|
|inputFilePath|System.String||

##### Exceptions

| Exception | Condition |
|-|-|
|System.InvalidOperationException||

