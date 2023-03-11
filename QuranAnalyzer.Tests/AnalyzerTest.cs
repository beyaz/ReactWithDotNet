//using System.IO;
//using Newtonsoft.Json;

namespace QuranAnalyzer;

[TestClass]
public class AnalyzerTest
{
    [TestMethod]
    public void _1_()
    {
        Analyzer.AnalyzeText("يَٰصَىٰحِبَىِ")
                .Count(x => x.ArabicLetterIndex == ArabicLetterIndex.Yaa)
                .Should().Be(3);

        Analyzer.AnalyzeText("يَٰصَىٰحِبَىِ ٱلسِّجْنِ أَمَّآ أَحَدُكُمَا فَيَسْقِى رَبَّهُۥ خَمْرًا وَأَمَّا ٱلْءَاخَرُ فَيُصْلَبُ فَتَأْكُلُ ٱلطَّيْرُ مِن رَّأْسِهِۦ قُضِىَ ٱلْأَمْرُ ٱلَّذِى فِيهِ تَسْتَفْتِيَانِ")
                .Count(x => x.ArabicLetterIndex == ArabicLetterIndex.Yaa)
                .Should().Be(11);
    }

    //[TestMethod]
    //public void _compare_mushaf_between_tanzil_and_kuran_mucizeler_com()
    //{
    //    var differentList = new List<string>();

    //    foreach (var line in File.ReadAllLines("d:\\a.txt"))
    //    {
    //        var (chapterNumber, verseNumber, verseText) = ParseLine(line);

    //        var tanzil = VerseFilter.GetVerseList(chapterNumber + ":" + verseNumber).Value[0];

    //        var analyzedText = Analyzer.AnalyzeText(verseText);

    //        var a = JsonConvert.SerializeObject(tanzil.TextAnalyzed.Where(l => l.ArabicLetterIndex >= 0).Select(l=>l.ArabicLetterIndex));
    //        var b = JsonConvert.SerializeObject(analyzedText.Where(l => l.ArabicLetterIndex >= 0).Select(l => l.ArabicLetterIndex));
    //        if ( a!= b)
    //        {
    //            differentList.Add(tanzil.Id);
    //        }

    //    }

    //    differentList.ToList();

    //}

    //static (int chapterNumber, int verseNumber, string verseText) ParseLine(string line)
    //{
    //    var arr = line.Split('|');

    //    return (int.Parse(arr[1]), int.Parse(arr[2]), arr[3]);

    //}
}