using System.IO;

namespace ReactDotNet.ComponentPreviewVsExtension
{
    static class FileSearch
    {
        static string ConfigFileName => "ReactDotNet.ComponentPreview.json";

        public static string GetConfigurationFilePathByComponentFilePath(string componentCSharpFilePath)
        {
            // search to upper

            var isFound = false;

            var directoryName = Path.GetDirectoryName(componentCSharpFilePath);
            if (directoryName == null)
            {
                return null;
            }

            while (true)
            {
                if (File.Exists(Path.Combine(directoryName , ConfigFileName)))
                {
                    isFound = true;
                    break;
                }

                directoryName = Path.GetDirectoryName(directoryName);
                if (!Directory.Exists(directoryName))
                {
                    break;
                }
            }

            if (isFound)
            {
                return Path.Combine(directoryName, ConfigFileName);
            }

            return null;
        }

    }
}