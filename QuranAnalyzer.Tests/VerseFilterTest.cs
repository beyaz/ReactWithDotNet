
namespace QuranAnalyzer;

[TestClass]
public class VerseFilterTest
{
    [TestMethod]
    public void FilterWithStar()
    {
        var records = VerseFilter.GetVerseList(" 42  : * ").Value;

        records.Count.Should().Be(53);

        records[2].Text.Should().Be("كَذَٰلِكَ يُوحِىٓ إِلَيْكَ وَإِلَى ٱلَّذِينَ مِن قَبْلِكَ ٱللَّهُ ٱلْعَزِيزُ ٱلْحَكِيمُ");
        records[52].Text.Should().Be("صِرَٰطِ ٱللَّهِ ٱلَّذِى لَهُۥ مَا فِى ٱلسَّمَـٰوَٰتِ وَمَا فِى ٱلْأَرْضِ ۗ أَلَآ إِلَى ٱللَّهِ تَصِيرُ ٱلْأُمُورُ");
    }

    [TestMethod]
    public void FilterWithStarWithMany()
    {
        var records = VerseFilter.GetVerseList(" 42  : * , 114 : *").Value;

        records.Count.Should().Be(53 + 6);

        records[2].Text.Should().Be("كَذَٰلِكَ يُوحِىٓ إِلَيْكَ وَإِلَى ٱلَّذِينَ مِن قَبْلِكَ ٱللَّهُ ٱلْعَزِيزُ ٱلْحَكِيمُ");
        records[52].Text.Should().Be("صِرَٰطِ ٱللَّهِ ٱلَّذِى لَهُۥ مَا فِى ٱلسَّمَـٰوَٰتِ وَمَا فِى ٱلْأَرْضِ ۗ أَلَآ إِلَى ٱللَّهِ تَصِيرُ ٱلْأُمُورُ");

        records[52 + 6].Text.Should().Be("مِنَ ٱلْجِنَّةِ وَٱلنَّاسِ");
    }

    [TestMethod]
    public void FilterWithStarWithManyWithSpecificAyahNumber()
    {
        var records = VerseFilter.GetVerseList(" 42  : * , 114 : *, 77:50").Value;

        records.Count.Should().Be(53 + 6 + 1);

        records[2].Text.Should().Be("كَذَٰلِكَ يُوحِىٓ إِلَيْكَ وَإِلَى ٱلَّذِينَ مِن قَبْلِكَ ٱللَّهُ ٱلْعَزِيزُ ٱلْحَكِيمُ");
        records[52].Text.Should().Be("صِرَٰطِ ٱللَّهِ ٱلَّذِى لَهُۥ مَا فِى ٱلسَّمَـٰوَٰتِ وَمَا فِى ٱلْأَرْضِ ۗ أَلَآ إِلَى ٱللَّهِ تَصِيرُ ٱلْأُمُورُ");

        records[52 + 6].Text.Should().Be("مِنَ ٱلْجِنَّةِ وَٱلنَّاسِ");

        records[52 + 6 + 1].Text.Should().Be("فَبِأَىِّ حَدِيثٍۭ بَعْدَهُۥ يُؤْمِنُونَ");
    }

    [TestMethod]
    public void SpecifiedWithRange()
    {
        var records = VerseFilter.GetVerseList(" 20  : 4- 7").Value;

        records.Count.Should().Be(4);
        records[0].ChapterNumber.Should().Be(20);
        records[1].ChapterNumber.Should().Be(20);
        records[2].ChapterNumber.Should().Be(20);
        records[3].ChapterNumber.Should().Be(20);

        records[0].Index.Should().Be("4");
        records[1].Index.Should().Be("5");
        records[2].Index.Should().Be("6");
        records[3].Index.Should().Be("7");
    }

    [TestMethod]
    public void FilterWithStarWithManyWithSpecificAyahNumber_with_Error()
    {
        VerseFilter.GetVerseList(" 42  : * , 114 : *, 77:50, 115:*").IsFail.Should().BeTrue();
    }
}