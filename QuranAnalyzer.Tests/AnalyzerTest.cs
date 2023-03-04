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
    }
}