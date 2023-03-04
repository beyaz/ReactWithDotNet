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
}