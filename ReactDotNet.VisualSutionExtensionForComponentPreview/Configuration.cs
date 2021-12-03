
using System;
using System.IO;

namespace ___Module___
{
    [Serializable]
    public class ConfigurationContract
    {
        public int WaitTimeInMillisecondsForRefresh { get; set; }
    }

    static class Configuration
    {
        static Configuration()
        {
            Config= Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigurationContract>(File.ReadAllText("Config.json"));
        }

        public static ConfigurationContract Config { get; set; }

        
    }
}