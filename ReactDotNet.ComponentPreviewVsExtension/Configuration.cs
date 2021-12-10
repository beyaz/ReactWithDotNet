using System;
using System.IO;
using FP;
using Newtonsoft.Json;
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

    static class Configuration
    {
        #region Public Methods
        public static Response<string> GetOutputJsFilePath()
        {
            return GetConfiguration().Bind(config => config.OutputJsFilePath);
        }

        public static Response<ConfigurationContract> GetConfiguration()
        {
            string readConfig() => File.ReadAllText(DirectoryHelper.GetFilePath("ReactDotNet.VsComponentPreview.Config.json"));

            return Try(readConfig).Bind( /*deserialize*/JsonConvert.DeserializeObject<ConfigurationContract>);
        }
        #endregion
    }
}