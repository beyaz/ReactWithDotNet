using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace QuranAnalyzer.WebUI;

[Serializable]
public sealed class Fact
{
    public string Name { get; set; }
    public string ShortDescription { get; set; }
    public string SearchScript { get; set; }
    public string SearchCharacters { get; set; }

    public static Fact[] GetFacts()
    {

        return System.Text.Json.JsonSerializer.Deserialize<Fact[]>(readResource("QuranAnalyzer.WebUI.Facts.json"));

       

        static string readResource(string resourcePath)
        {
            using Stream stream = typeof(Fact).Assembly.GetManifestResourceStream(resourcePath);

            using StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException());

            return reader.ReadToEnd();
        }
    }
}