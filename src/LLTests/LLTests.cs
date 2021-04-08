using System;
using combit.Reporting.DataProviders;
using combit.Reporting;
using System.IO;
using System.Collections.Generic;

// install System.Drawing.Common from NuGet

// Following the page 21 in https://www.microway.com.au/catalog/combit/Programmers-Manual.pdf

// Following the next URL for the last version: https://docu.combit.net/progref/en/index.html#!Documents/integratelistlabel.htm

// En el mismo dominio no se pueden tener dos versiones de List & Labal.
// Se necesita una licencia. 

// Para poder usar sus librerías se necesita tener instalado el programa.
namespace LLTests
{
    public interface IModelObject
    {
        public string ModelVariable1 { get; set; }
        public string ModelVariable2 { get; set; }
    }

    public class ModelObject : IModelObject
    {
        public string ModelVariable1 { get; set; }
        public string ModelVariable2 { get; set; }

    }


    public interface IModelObjectExtend : IModelObject
    {
        public IModelObject ModelObjectVariable { get; set; }
    }

    public class ModelObjectExtend : ModelObject, IModelObjectExtend
    {
        public ModelObjectExtend()
        {
        }

        public IModelObject ModelObjectVariable { get; set; }
    }

    public class LLTests
    {
        private ListLabel LL = new();
        private string _appDataPath;

        public LLTests()
        {
            ObjectDataProviderExample();
        }

        class Car
        {
            public string Brand { get; set; }
            public string Model { get; set; }
        }

        private void ObjectDataProviderExample()
        {
            List<Car> cars = new List<Car>();
            cars.Add(new Car { Brand = "VW", Model = "Passat"});
            cars.Add(new Car { Brand = "Porsche", Model = "Cayenne"});
            LL.DataSource = new ObjectDataProvider(cars);
            
            // Execute main function: Design
            LL.Design();

        }

        private void Function1()
        {
            _appDataPath = Path.GetFullPath("./data-model");

            // Identify the data source
            LL.DataSource = CreateDataSet("JSON");
            // LL.DataMember = "Customers";

            /* Define additional data fields */
            // We add the folders with a point. For instance: AdditionalData.UserName
            LL.Variables.Add("AdditionalData.UserName", "Usuario");
            LL.Variables.Add("AdditionalData.ProjectName ", "Name of project");

            /* using a dictionary */
            // Same as before we add the folders with a point
            IDictionary<string, string> modelDictionary = new Dictionary<string, string>
            {
                { "Model.variable1", "value1" },
                { "Model.variable2", "value2" },
            }; 
            LL.Variables.AddFromDictionary((System.Collections.IDictionary)modelDictionary);

            /* using a model */
            // it is not possible to add folders
            IModelObject modelObject = new ModelObject()
            {
                ModelVariable1 = "ModelValue1", 
                ModelVariable2 = "ModelValue2"
            };
            LL.Variables.AddFromObject(modelObject);

            // using a model extended
            // We add the folders with a variable of type of an object
            IModelObjectExtend modelObjectExtended = new ModelObjectExtend()
            {
                ModelObjectVariable = modelObject,
                ModelVariable1 = "ModelExtendedValue1",
                ModelVariable2 = "ModelExtendedValue2"
            };
            LL.Variables.AddFromObject(modelObjectExtended);

            // Note that the value of the created variable "ModelVariable1"
            // will be override from "ModelValue1" to "ModelExtendedValue1"

            // defining a list can be done with the example of function ObjectDataProviderExample



            // Define project type
            // LL.AutoProjectType = LlProject.Label;

            // LL.ExportOptions.Add(LlExportOption.ExportTarget, "PDF");

            // LL.Print();

            LL.Design();

            LL.Dispose();
        }

        private object CreateDataSet(string dataSourceId)
        {

            IDataProvider provider = null;
            // string dataMember = string.Empty;

            switch (dataSourceId.ToUpperInvariant())
            {
                case "XML1":
                    provider = new XmlDataProvider(Path.Combine(_appDataPath, "customers.xml"));
                    // dataMember = "book";
                    break;
                case "XML2":
                    provider = new XmlDataProvider(Path.Combine(_appDataPath, "data2.xml"));
                    // dataMember = "Companies";
                    break;
                case "JSON":
                    provider = new JsonDataProvider(File.ReadAllText(Path.Combine(_appDataPath, "data.json")));
                    break;
                /* case "OBJECT":
                    provider = new ObjectDataProvider(combit.Service.GenericList.GetGenericList());
                    break;*/
                case "CSV":
                    provider = new CsvDataProvider(Path.Combine(_appDataPath, "iislog.csv"), true, "Log", '\t', 0, false);
                    break;
            }

            return provider;
        }

        [STAThread]
        public static int Main()
        {
            LLTests ll = new();

            return 0;
        }
    }
}