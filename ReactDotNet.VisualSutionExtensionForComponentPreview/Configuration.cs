
using System;
using System.IO;
using static FP.FpExtensions;

namespace ___Module___
{
    [Serializable]
    public class ConfigurationContract
    {
        public string OutputJsFilePath { get; set; }
    }

    static class Configuration
    {
        static void InitializeConfig()
        {

            string readConfig() => File.ReadAllText(DirectoryHelper.GetFilePath("ReactDotNet.VsComponentPreview.Config.json"));

            var result = Try(readConfig).Bind(Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigurationContract>);

            result.Match(success: config =>
                         {
                             FileTracer.Trace("Config successfully loaded.");
                             Config = config;
                         },
                         fail: exceptions =>
                         {
                             FileTracer.Trace("Config is not found.");
                         });

            
            
        }

        static Configuration()
        {
            FpExtensions.Run(InitializeConfig);
        }

        public static ConfigurationContract Config { get; private set; }

        
    }
}