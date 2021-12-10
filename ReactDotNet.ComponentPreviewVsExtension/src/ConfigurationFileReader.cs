using System;
using System.IO;
using FP;
using Newtonsoft.Json;
using ReactDotNet.ComponentPreviewVsExtension;
using static FP.FpExtensions;

namespace ___Module___
{
    [Serializable]
    public class ConfigurationContract
    {
        #region Public Properties
        public string OutputJsFilePath { get; set; }
        public string HtmlFilePath { get; set; }
        public string HtmlElementId { get; set; }
        #endregion
    }

    static class ConfigurationFileReader
    {
       

        #region Public Methods
        
        public static Response<ConfigurationContract> GetConfigurationByComponentFilePath(string componentCSharpFilePath)
        {
            var configFilePath = FileSearch.GetConfigurationFilePathByComponentFilePath(componentCSharpFilePath);
            if (configFilePath == null)
            {
                return new Exception("ConfigFileNotFound." + componentCSharpFilePath);
            }

            string readConfig() => File.ReadAllText(configFilePath);

            return Try(readConfig).Bind( /*deserialize*/JsonConvert.DeserializeObject<ConfigurationContract>);
        }

        #endregion
    }
}