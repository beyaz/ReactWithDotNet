
using System;
using System.IO;

namespace ___Module___
{
    static class FileTracer
    {
        static readonly object _syncObject = new object();

        public static void Trace(string message)
        {
            

            try
            {
                lock (_syncObject)
                {
                    var targetFilePath = Path.Combine(Path.GetDirectoryName(typeof(FileTracer).Assembly.Location), "ReactDotNet.VsComponentPreview.trace.txt");

                    using (var fs = new FileStream(targetFilePath, FileMode.CreateNew))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            sw.Write(message);
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }

        }
    }


    [Serializable]
    public class ConfigurationContract
    {
        public int WaitTimeInMillisecondsForRefresh { get; set; }
    }

    static class Configuration
    {
        static void InitializeConfig()
        {
            Config = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigurationContract>(File.ReadAllText("ReactDotNet.VsComponentPreview.Config.json"));
        }

        static Configuration()
        {
            FpExtensions.Run(InitializeConfig);
        }

        public static ConfigurationContract Config { get; private set; }

        
    }
}