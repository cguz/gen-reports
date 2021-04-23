# Report tools #

Author: (Dr. Cesar Augusto Guzman Alvarez)[cguzman@preference.es]

## List and Label ##

The following repository was developed to test some functionalities of LL. More specifically, we show examples in C# to generate a List and Label report.


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


### Source examples ###

All the examples are in the class LLReportsExample.cs; the Main method instantiate the class LLReportsExample, which receives one input parameter that indicate the example to execute.

The different examples are:

* VariablesExample: Load some additional variable to LL.
* DataProvidersExamples: Load a JSON or XML in the datasource of LL. Load a datasource is a requirement in LL. 
* DictionaryExample: Load some variables from a dictionary.
* ObjectExample: Load an Object class. 
* ObjectExtendExample: Load a more complex Object class.
* ObjectDataProviderExample: Load an object class in the datasource.
* FullDataProvidersExample: A complete example that load at the same time some of the previous examples.
* LayoutExample: Load a predefined layout.

More examples with different layout configurations:

* LayoutSeparateRegionsAndRegionsPricesExample: Load model with two lists
* LayoutUnifiedRegionsAndRegionsPricesExample: Load model with only one region that represents either a normal and price regions
* LayoutFinalExample:


### References ###

* https://www.microway.com.au/catalog/combit/Programmers-Manual.pdf

* https://docu.combit.net/progref/en/index.html#!Documents/integratelistlabel.htm
