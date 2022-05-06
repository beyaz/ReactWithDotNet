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
            var records = AyaFilter.Filter(" 42  : * ").Value;

            records.Count.Should().Be(53);

            records[2]._text.Should().Be("كَذَٰلِكَ يُوحِىٓ إِلَيْكَ وَإِلَى ٱلَّذِينَ مِن قَبْلِكَ ٱللَّهُ ٱلْعَزِيزُ ٱلْحَكِيمُ");
            records[52]._text.Should().Be("صِرَٰطِ ٱللَّهِ ٱلَّذِى لَهُۥ مَا فِى ٱلسَّمَـٰوَٰتِ وَمَا فِى ٱلْأَرْضِ ۗ أَلَآ إِلَى ٱللَّهِ تَصِيرُ ٱلْأُمُورُ");
            
            
        }

        [TestMethod]
        public void FilterWithStarWithMany()
        {
            var records = AyaFilter.Filter(" 42  : * , 114 : *").Value;

            records.Count.Should().Be(53 + 6);

            records[2]._text.Should().Be("كَذَٰلِكَ يُوحِىٓ إِلَيْكَ وَإِلَى ٱلَّذِينَ مِن قَبْلِكَ ٱللَّهُ ٱلْعَزِيزُ ٱلْحَكِيمُ");
            records[52]._text.Should().Be("صِرَٰطِ ٱللَّهِ ٱلَّذِى لَهُۥ مَا فِى ٱلسَّمَـٰوَٰتِ وَمَا فِى ٱلْأَرْضِ ۗ أَلَآ إِلَى ٱللَّهِ تَصِيرُ ٱلْأُمُورُ");
            

            records[52+ 6]._text.Should().Be("مِنَ ٱلْجِنَّةِ وَٱلنَّاسِ");
            
        }

        [TestMethod]
        public void FilterWithStarWithManyWithSpecificAyahNumber()
        {
            var records = AyaFilter.Filter(" 42  : * , 114 : *, 77:50").Value;

            records.Count.Should().Be(53 + 6 + 1);

            records[2]._text.Should().Be("كَذَٰلِكَ يُوحِىٓ إِلَيْكَ وَإِلَى ٱلَّذِينَ مِن قَبْلِكَ ٱللَّهُ ٱلْعَزِيزُ ٱلْحَكِيمُ");
            records[52]._text.Should().Be("صِرَٰطِ ٱللَّهِ ٱلَّذِى لَهُۥ مَا فِى ٱلسَّمَـٰوَٰتِ وَمَا فِى ٱلْأَرْضِ ۗ أَلَآ إِلَى ٱللَّهِ تَصِيرُ ٱلْأُمُورُ");
            

            records[52+ 6]._text.Should().Be("مِنَ ٱلْجِنَّةِ وَٱلنَّاسِ");

            records[52+ 6+ 1]._text.Should().Be("فَبِأَىِّ حَدِيثٍۭ بَعْدَهُۥ يُؤْمِنُونَ");
            
        }


        [TestMethod]
        public void FilterWithStarWithManyWithSpecificAyahNumber_with_Error()
        {
            AyaFilter.Filter(" 42  : * , 114 : *, 77:50, 115:*").IsFail.Should().BeTrue();
        }

        [TestMethod]
        public void YaSin()
        {
            var records = Mixin.SearchCharachters("36 : *", "ي , سٓ").Value;

            records.Count.Should().Be(285);

            CharachterSearchResultColorizer.ColorizeCharachterSearchResults(records);
        }


        //[TestMethod]
        public void Export()
        {
            using StreamWriter w = File.AppendText("d:\\A.txt");

            var total = 1;
            foreach (var sura in DataAccess.AllSura)
            {
                
                foreach (var aya in sura.aya)
                {
                    w.WriteLine(total++ + "|"+ sura.Index +"|"+(aya._index) +"|" + aya._text);
                }
            }
        }
}
