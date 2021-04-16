# Report tools #

## List and Label ## 

Example in C# to generate a List and Label report.

### Requirements ###

* NuGet 4.3.0+

* Visual Studio 2017 version 15.3+

* Visual Studio 2015 with NuGet VSIX v3.6.0

* dotnet: dotnetcore.exe (.NET SDK 2.0.0+)

* List and Label combit software

### Configuration steps ###

* Install List and Label from [here](https://www.combit.com/)
* Get a licence (this is a requirement).
* Install combit.ListLabel26 package from NuGet packages. They can also be found from [here](https://www.nuget.org/profiles/combit).
* Note that in the same domain we can not have two versions of List & Label.


### Source example ###

Note. The code was developed only to tests some functionalities of LL.

The example can be found in the folder 'LLTests/'. The solution project is in "ReportingReportsTests.sln" and it can be openned from VS 2019.

All the examples are in the class LLTests.cs; The Main method instantiate the class LLTests, which receives one input parameter that indicate the example to execute.

The different examples are:

* DataProvidersExamples: Load a JSON or XML in the datasource of LL. Load a datasource is a requirement in LL. 
* VariablesExample: Load some additional variable to LL.
* DictionaryExample: Load some variables from a dictionary.
* ObjectExample: Load an Object class. 
* ObjectExtendExample: Load a more complex Object class.
* ObjectDataProviderExample: Load an object class in the datasource.
* FullDataProvidersExample: A complete example that load at the same time some of the previous examples.
* LayoutExample: Load a predefined layout.

More examples with different layout configurations:

* LayoutListModelsExample: Load a list in the layout.
* LayoutAddTwoModelsExample: Load two models in the layout.
* LayoutSerializeObjectToJsonExample: Load a model from a serialize object to JSON
* LayoutSeparateRegionsAndRegionsPricesExample: Load model with two lists
* LayoutUnifiedRegionsAndRegionsPricesExample: Load model with only one region that represents either a normal and price regions
* LayoutFinalExample:


### References ###

* https://www.microway.com.au/catalog/combit/Programmers-Manual.pdf

* https://docu.combit.net/progref/en/index.html#!Documents/integratelistlabel.htm
