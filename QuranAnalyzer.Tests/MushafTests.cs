using System.Net;
using System.Net.Http;

namespace QuranAnalyzer;

[TestClass]
 [Ignore]
public class MushafTests
{
    class Data
    {
        public string verse_text_arabic { get; set; }
    }
    class Response
    {
        public bool success { get; set; }
        public Data[] data { get; set; }
    }
    
    [TestMethod]
    public void AnalyzeVerseTest()
    {
        var a = GetArabicText(2, 3);
        var response = Newtonsoft.Json.JsonConvert.DeserializeObject<Response>(a);
        var text = response.data[0].verse_text_arabic;
    }

    static string GetArabicText(int chapter, int verseNumber)
    {
        var webClient = new WebClient();

        return webClient.DownloadString($"https://api.quraniclabs.com/quran/{chapter}/{verseNumber}-200");
    }
}