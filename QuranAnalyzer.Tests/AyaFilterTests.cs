using System.IO;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace QuranAnalyzer;

    [TestClass]
    public class AyaFilterTests
    {
        [TestMethod]
        public void FilterWithStar()
        {
            var records = VerseFilter.GetVerseList(" 42  : * ").Value;

            records.Count.Should().Be(53);

            records[2]._text.Should().Be("كَذَٰلِكَ يُوحِىٓ إِلَيْكَ وَإِلَى ٱلَّذِينَ مِن قَبْلِكَ ٱللَّهُ ٱلْعَزِيزُ ٱلْحَكِيمُ");
            records[52]._text.Should().Be("صِرَٰطِ ٱللَّهِ ٱلَّذِى لَهُۥ مَا فِى ٱلسَّمَـٰوَٰتِ وَمَا فِى ٱلْأَرْضِ ۗ أَلَآ إِلَى ٱللَّهِ تَصِيرُ ٱلْأُمُورُ");
            
            
        }

        [TestMethod]
        public void FilterWithStarWithMany()
        {
            var records = VerseFilter.GetVerseList(" 42  : * , 114 : *").Value;

            records.Count.Should().Be(53 + 6);

            records[2]._text.Should().Be("كَذَٰلِكَ يُوحِىٓ إِلَيْكَ وَإِلَى ٱلَّذِينَ مِن قَبْلِكَ ٱللَّهُ ٱلْعَزِيزُ ٱلْحَكِيمُ");
            records[52]._text.Should().Be("صِرَٰطِ ٱللَّهِ ٱلَّذِى لَهُۥ مَا فِى ٱلسَّمَـٰوَٰتِ وَمَا فِى ٱلْأَرْضِ ۗ أَلَآ إِلَى ٱللَّهِ تَصِيرُ ٱلْأُمُورُ");
            

            records[52+ 6]._text.Should().Be("مِنَ ٱلْجِنَّةِ وَٱلنَّاسِ");
            
        }

        [TestMethod]
        public void FilterWithStarWithManyWithSpecificAyahNumber()
        {
            var records = VerseFilter.GetVerseList(" 42  : * , 114 : *, 77:50").Value;

            records.Count.Should().Be(53 + 6 + 1);

            records[2]._text.Should().Be("كَذَٰلِكَ يُوحِىٓ إِلَيْكَ وَإِلَى ٱلَّذِينَ مِن قَبْلِكَ ٱللَّهُ ٱلْعَزِيزُ ٱلْحَكِيمُ");
            records[52]._text.Should().Be("صِرَٰطِ ٱللَّهِ ٱلَّذِى لَهُۥ مَا فِى ٱلسَّمَـٰوَٰتِ وَمَا فِى ٱلْأَرْضِ ۗ أَلَآ إِلَى ٱللَّهِ تَصِيرُ ٱلْأُمُورُ");
            

            records[52+ 6]._text.Should().Be("مِنَ ٱلْجِنَّةِ وَٱلنَّاسِ");

            records[52+ 6+ 1]._text.Should().Be("فَبِأَىِّ حَدِيثٍۭ بَعْدَهُۥ يُؤْمِنُونَ");
            
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

        records[0]._index.Should().Be("4");
        records[1]._index.Should().Be("5");
        records[2]._index.Should().Be("6");
        records[3]._index.Should().Be("7");
    }


    [TestMethod]
        public void FilterWithStarWithManyWithSpecificAyahNumber_with_Error()
        {
            VerseFilter.GetVerseList(" 42  : * , 114 : *, 77:50, 115:*").IsFail.Should().BeTrue();
        }

        [TestMethod]
        public void YaSin()
        {
            var records = QuranAnalyzerMixin.SearchCharachters("36 : *", "ي , سٓ").Value;

            records.Count.Should().Be(285);

            
        }


        //[TestMethod]
        public void Export()
        {
            using StreamWriter w = File.AppendText("d:\\A.txt");

            var total = 1;
            foreach (var sura in DataAccess.AllSurahs)
            {
                
                foreach (var aya in sura.Verses)
                {
                    w.WriteLine(total++ + "|"+ sura.Index +"|"+(aya._index) +"|" + aya._text);
                }
            }
        }
}
