using System;
using System.IO;
using System.Text.Json;
using YamlDotNet.Serialization;

namespace QuranAnalyzer.WebUI;

static class ResourceHelper
{
    public static T Read<T>(string fileNameRelativelyInProject)
    {
        var fileData = readResource("QuranAnalyzer.WebUI." + fileNameRelativelyInProject);

        if (fileNameRelativelyInProject.EndsWith(".yaml"))
        {
            return new DeserializerBuilder().Build().Deserialize<T>(fileData);
        }

        return JsonSerializer.Deserialize<T>(fileData);

        static string readResource(string resourcePath)
        {
            using var stream = typeof(ResourceHelper).Assembly.GetManifestResourceStream(resourcePath);

            using var reader = new StreamReader(stream ?? throw new InvalidOperationException());

            return reader.ReadToEnd();
        }
    }

}