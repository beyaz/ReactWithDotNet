using System;
using System.IO;

namespace QuranAnalyzer.WebUI;

static class ResourceHelper
{
    public static T Read<T>(string fileName)
    {
        return System.Text.Json.JsonSerializer.Deserialize<T>(readResource("QuranAnalyzer.WebUI."+fileName));

        static string readResource(string resourcePath)
        {
            using Stream stream = typeof(FactModel).Assembly.GetManifestResourceStream(resourcePath);

            using StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException());

            return reader.ReadToEnd();
        }
    }
}