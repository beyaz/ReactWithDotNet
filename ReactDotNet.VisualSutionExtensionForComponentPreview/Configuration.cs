
using System;
using System.IO;

namespace ___Module___
{
    [Serializable]
    public class ConfigurationContract
    {
        public int WaitTimeInMillisecondsForRefresh { get; set; }
        public string OutputJsFilePath { get; set; }
    }

    static class Configuration
    {
        static void InitializeConfig()
        {
            Config = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigurationContract>(File.ReadAllText(DirectoryHelper.GetFilePath("ReactDotNet.VsComponentPreview.Config.json")));
            if (Config == null)
            {
                FileTracer.Trace("Config is not found.");
                
            }
        }

        static Configuration()
        {
            FpExtensions.Run(InitializeConfig);
        }

        public static ConfigurationContract Config { get; private set; }

        
    }
}