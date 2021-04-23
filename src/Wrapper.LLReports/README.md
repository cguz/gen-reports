# Report tools #

Author: (Dr. Cesar Augusto Guzman Alvarez)[cguzman@preference.es]

## List and Label ##

The following repository was developed:

* To implement a wrapper to export with LL. The main reason was to separate the library dependency and encapsulate some codes.


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


### Source Wrapper ###

The Wrapper is in the class LLReportsWrapper.cs

It can be used as follow:

```C#
IWrapper reports = new LLReportsWrapperImp(Json, PathToExport, ProjectFileLL);
bool status = reports.Export();
```

Where:

* Json is a json text
* PathToExport is a full path file (including extension)
* ProjectFileLL is a layout in LL. Since we are exporting it is a requirement that this file exist.

More examples of the use of LL can be found in the Test project Preference.Wrapper.LLReports.Test



### References ###

* https://www.microway.com.au/catalog/combit/Programmers-Manual.pdf

* https://docu.combit.net/progref/en/index.html#!Documents/integratelistlabel.htm
