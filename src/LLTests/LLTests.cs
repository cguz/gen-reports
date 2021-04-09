using System;
using combit.Reporting.DataProviders;
using combit.Reporting;
using System.IO;
using System.Collections.Generic;
using Preference.IntelliText;
using Preference.IntelliText.Template;

namespace LLTests
{
    /**
     * Enumerate to identify the different examples. 
     */
    public enum Options
    {
        LayoutExample,
        VariablesExample,
        DictionaryExample,
        ObjectExample,
        ObjectExtendExample,
        ObjectDataProviderExample,
        FullDataProvidersExample,
        DataProvidersExamples,
        IntelliTextExample,
        IntelliTextListModelsExample,
        IntelliTextAddTwoModelsExample
    }


    /**
     * Helper classes for some examples
     */
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

    public class Car
    {
        public string Brand { get; set; }
        public string Model { get; set; }
    }

    public class ModelClass
    {
        public IDescriptionModel model1 { get; internal set; }
        public IDescriptionModel model2 { get; internal set; }
    }

    /**
     * Class with the code of the different examples
     */
    public class LLTests
    {
        // to communicate with list and label
        private ListLabel LL = new();

        // path to the datasource
        private string _appDataPath;

        /**
         * Constructor of the class. 
         * @param selected example
         * */
        public LLTests(Options selectedOption)
        {
            switch (selectedOption)
            {
                case Options.VariablesExample:
                    VariablesExample();
                    break;
                case Options.DataProvidersExamples:
                    DataProvidersExamples();
                    break;
                case Options.DictionaryExample:
                    DictionaryExample();
                    break;
                case Options.FullDataProvidersExample:
                    FullDataProvidersExample();
                    break;
                case Options.LayoutExample:
                    LayoutExample();
                    break;
                case Options.ObjectDataProviderExample:
                    ObjectDataProviderExample();
                    break;
                case Options.ObjectExample:
                    ObjectExample();
                    break;
                case Options.ObjectExtendExample:
                    ObjectExtendExample();
                    break;
                case Options.IntelliTextExample:
                    IntelliTextExample();
                    break;
                case Options.IntelliTextListModelsExample:
                    IntelliTextListModelsExample();
                    break;
                case Options.IntelliTextAddTwoModelsExample:
                    IntelliTextAddTwoModelsExample();
                    break;
            }
        }

        private PreferenceIntelliTextExecutor GenerateIntelliText()
        {
            string OutputFile = "WB-ES-SeveralDescriptionsPriceGrouped-HTML-UseCase5.txt";
            string template = "WB-SeveralDescriptionPriceGrouped-UseCase5";
            string automaticTextGeneratorProvider = "FILE";
            string audience = "Consumer";
            string dataProvider = "FILE";
            string pathDataProvider = "./data-model/xml-descriptive/WB-SeveralDescriptionsPriceGrouped-UseCase4.xml";
            string render = "LL";
            string language = "es-ES";

            TextSettings settings = new();

            // set the template id, audience and language
            settings.Template = template;

            settings.TextGeneratorProvider = automaticTextGeneratorProvider;

            settings.PathTextGeneratorProvider = Path.GetFullPath("./text-generator-files/");

            settings.Audience = audience;

            settings.Culture = new(language, false);

            settings.PathTemplates = Path.GetFullPath("./catalogs/company/");

            settings.DataProvider = dataProvider;

            settings.PathDataProvider = Path.GetFullPath(pathDataProvider);

            settings.Render = render;

            settings.SaveTo = OutputFile;

            // IntelliText creator receives the catalog constructor, the render to use, the data provider to use
            PreferenceIntelliText intelliText = new(settings);

            // build the required information: template, render, and structure
            intelliText.Build();

            // get the executor
            return intelliText.GetExecutor();
        }

        private void IntelliTextExample()
        {
            string pathDataLayout = Path.GetFullPath("./layout/");

            PreferenceIntelliTextExecutor executor = GenerateIntelliText();

            // generate the model description
            IDescriptionModel modelDescription = executor.GenerateDescriptions();

            LL.DataSource = new ObjectDataProvider(modelDescription);

            // Sets the file name of the List & Label project file for the methods Print and Design in databound mode.
            LL.AutoProjectFile = Path.Combine(pathDataLayout, "layout-example.lst");

            LL.Design();

            LL.Dispose();
        }

        private void IntelliTextListModelsExample()
        {
            string pathDataLayout = Path.GetFullPath("./layout/");

            PreferenceIntelliTextExecutor executor = GenerateIntelliText();

            // generate the model description
            IDescriptionModel modelDescription = executor.GenerateDescriptions();

            List<IDescriptionModel> listModels = new();
            listModels.Add(modelDescription);
            listModels.Add(modelDescription);

            LL.DataSource = new ObjectDataProvider(listModels);

            // Sets the file name of the List & Label project file for the methods Print and Design in databound mode.
            LL.AutoProjectFile = Path.Combine(pathDataLayout, "layout-listmodels-example.lst");

            LL.Design();

            LL.Dispose();
        }
        private void IntelliTextAddTwoModelsExample()
        {
            string pathDataLayout = Path.GetFullPath("./layout/");

            PreferenceIntelliTextExecutor executor = GenerateIntelliText();

            // generate the model description
            IDescriptionModel modelDescription = executor.GenerateDescriptions();

            ModelClass listModels = new();
            listModels.model1 = modelDescription;
            listModels.model2 = modelDescription;

            LL.DataSource = new ObjectDataProvider(listModels);

            // Sets the file name of the List & Label project file for the methods Print and Design in databound mode.
            LL.AutoProjectFile = Path.Combine(pathDataLayout, "layout-listmodels-example.lst");

            LL.Design();

            LL.Dispose();
        }

        /**
         * Example to load a JSON or XML in LL
         */
        private void DataProvidersExamples()
        {
            _appDataPath = Path.GetFullPath("./data-model");

            // Identify the data source
            LL.DataSource = CreateDataSet("JSON");
            // LL.DataMember = "Customers";

            LL.Design();

            LL.Dispose();
        }

        /**
         * Example to load son additional variables to LL
         */
        private void VariablesExample()
        {
            _appDataPath = Path.GetFullPath("./data-model");

            // Identify the data source
            LL.DataSource = CreateDataSet("JSON");

            /* Define additional data fields */
            // We add the folders with a point. For instance: AdditionalData.UserName
            LL.Variables.Add("AdditionalData.UserName", "Usuario");
            LL.Variables.Add("AdditionalData.ProjectName ", "Name of project");

            // Note that the variables can be assigned in a simple List to the DataSource 

            LL.Design();

            LL.Dispose();
        }

        /**
         * Example to load some variables in the dictionary
         */
        private void DictionaryExample()
        {
            _appDataPath = Path.GetFullPath("./data-model");

            // Identify the data source
            LL.DataSource = CreateDataSet("JSON");

            /* using a dictionary */
            // Same as before we add the folders with a point
            IDictionary<string, string> modelDictionary = new Dictionary<string, string>
            {
                { "Model.variable1", "value1" },
                { "Model.variable2", "value2" },
            };
            LL.Variables.AddFromDictionary((System.Collections.IDictionary)modelDictionary);

            LL.Design();

            LL.Dispose();

        }

        /**
         * Example to load an object class 
         */
        private void ObjectExample()
        {
            _appDataPath = Path.GetFullPath("./data-model");

            // Identify the data source
            LL.DataSource = CreateDataSet("JSON");

            /* using a model */
            // it is not possible to add folders
            IModelObject modelObject = new ModelObject()
            {
                ModelVariable1 = "ModelValue1",
                ModelVariable2 = "ModelValue2"
            };
            LL.Variables.AddFromObject(modelObject);

            LL.Design();

            LL.Dispose();
        }

        /**
         * Example to load an extended object
         */
        private void ObjectExtendExample()
        {
            _appDataPath = Path.GetFullPath("./data-model");

            // Identify the data source
            LL.DataSource = CreateDataSet("JSON");

            /* using a model */
            // it is not possible to add folders
            IModelObject modelObject = new ModelObject()
            {
                ModelVariable1 = "ModelValue1",
                ModelVariable2 = "ModelValue2"
            };

            // using a model extended
            // We add the folders with a variable of type of an object
            IModelObjectExtend modelObjectExtended = new ModelObjectExtend()
            {
                ModelObjectVariable = modelObject,
                ModelVariable1 = "ModelExtendedValue1",
                ModelVariable2 = "ModelExtendedValue2"
            };
            LL.Variables.AddFromObject(modelObjectExtended);

            LL.Design();

            LL.Dispose();
        }

        /**
         * Example to use an ObjectDataProvider as a DataSource
         */
        private void ObjectDataProviderExample()
        {
            List<Car> cars = new List<Car>();
            cars.Add(new Car { Brand = "VW", Model = "Passat"});
            cars.Add(new Car { Brand = "Porsche", Model = "Cayenne"});
            LL.DataSource = new ObjectDataProvider(cars);
            
            // Execute main function: Design
            LL.Design();

        }

        /**
         * Example loading different data
         */
        private void FullDataProvidersExample()
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

            // LL.Print();

            LL.Design();

            LL.Dispose();
        }

        /**
         * Example to load a predefined layout
         * */
        private void LayoutExample()
        {
            _appDataPath = Path.GetFullPath("./data-model");

            string DataLayoutPath = Path.GetFullPath("./layout");

            // Identify the data source
            LL.DataSource = CreateDataSet("JSON");

            /* using a dictionary */
            // Same as before we add the folders with a point
            IDictionary<string, string> modelDictionary = new Dictionary<string, string>
            {
                { "Model.variable1", "value1" },
                { "Model.variable2", "value2" },
            };
            LL.Variables.AddFromDictionary((System.Collections.IDictionary)modelDictionary);

            // Sets the file name of the List & Label project file for the methods Print and Design in databound mode.
            LL.AutoProjectFile = Path.Combine(DataLayoutPath, "layout1.lst");

            LL.Design();
        }

        /**
         * It create a dataset in XML, JSON, CSV
         */
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
            LLTests ll = new(Options.IntelliTextAddTwoModelsExample);

            return 0;
        }
    }
}