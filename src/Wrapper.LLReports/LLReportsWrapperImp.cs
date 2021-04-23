/**
* @file LLReportsWrapper.cs
*
* @brief This file is a Wrapper to execute LL from source code. 
*
* @author Cesar Augusto Guzman Alvarez
* Contact: cguzman@preference.es
*/
namespace Wrapper.LLReports
{
    using combit.Reporting;
    using combit.Reporting.DataProviders;
    using System;

    public class LLReportsWrapperImp : IWrapper
    {
        private string ModelJson { get; }
        private string FileToExport { get; }
        private string ProjectFile { get; }
        private bool ShowResult { get; }

        private ListLabel LL;
        private ExportConfiguration expConfig;

        public LLReportsWrapperImp(string ModelJson, string FileToExport, string ProjectFile, bool ShowResult = false)
        {
            this.ModelJson = ModelJson;
            this.FileToExport = FileToExport;
            this.ProjectFile = ProjectFile;
            this.ShowResult = ShowResult;

            Init();
        }

        /**
         * Configurate the LL
         */
        private void Init()
        {
            LL = new();
            LL.LicensingInfo = "MuCXGg";

            // add the data source with the objectdataprovider
            LL.DataSource = new JsonDataProvider(ModelJson);

            LlExportTarget option = GetExportTargetByExtension(FileToExport);

            // Set target and path (here: PDF) and project file
            expConfig = new ExportConfiguration(option, FileToExport, ProjectFile);

            // Show result
            expConfig.ShowResult = ShowResult;
        }

        /**
         * We retrieve the export target by the extension of the file to export
         * 
         * @param file to export (string)
         * @return export target with the LlExportTarget structure
         */
        private LlExportTarget GetExportTargetByExtension(string fileToExport)
        {
            string extension = FileToExport.Split(".")[1].ToLower().Trim();
            switch (extension)
            {
                case "pdf":
                    return LlExportTarget.Pdf;
                case "html":
                    return LlExportTarget.Html;
                case "jpg":
                    return LlExportTarget.Jpeg;
                case "svg":
                    return LlExportTarget.Svg;
                case "rtf":
                    return LlExportTarget.Rtf;
                default:
                    return LlExportTarget.Text;
            }
        }

        /**
         * Execute the export method to generate the file
         */
        public bool Export()
        {
            try
            {
                LL.Export(expConfig);

                // execute the main function: Design
                LL.Dispose();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
