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
        #endregion
    }

    static class Configuration
    {
        #region Public Methods
        public static Response<string> GetOutputJsFilePath()
        {
            string readConfig() => File.ReadAllText(DirectoryHelper.GetFilePath("ReactDotNet.VsComponentPreview.Config.json"));

            return Try(readConfig)
                  .Bind( /*deserialize*/JsonConvert.DeserializeObject<ConfigurationContract>)
                  .Bind(config => config.OutputJsFilePath);
        }
        #endregion
    }
}