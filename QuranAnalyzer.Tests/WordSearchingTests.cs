global using static QuranAnalyzer.QuranQuery;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static QuranAnalyzer.DataAccess;



namespace QuranAnalyzer
{


    [TestClass]
    public class WordSearchingTests
    {
        


        static int GetCountOfWord(string searchWord)
        {
            var searchList = WordSearcher.AsClearArabicCharacterList(searchWord);

            var total = 0;
            foreach (var sura in AllSurahs)
            {
                foreach (var aya in sura.Verses)
                {
                    foreach (var word in aya._text.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    {
                        var isSame = WordSearcher.AsClearArabicCharacterList(word).HasValueAndSame(searchList);

                        if (isSame)
                        {
                            total++;
                        }
                    }
                }
            }

            return total;
        }

        [TestMethod]
        public void Adem_İsa()
        {


            {
                var adem = "اادم";

                GetCountOfWord(adem).Should().Be(15);
            }

            {
                var ya_adem = "يادم";

                GetCountOfWord(ya_adem).Should().Be(4);
            }

            {

                var li_adem = "لِـَٔادَمَ";

                GetCountOfWord(li_adem).Should().Be(5);
            }

            {
                var veya_adem = "و يادم";

                GetCountOfWord(veya_adem).Should().Be(1);
            }

            // isa

            {
                var isa = "عيسي";

                GetCountOfWord(isa).Should().Be(12);
            }

            {
                var ve_isa = "وعيسي";

                GetCountOfWord(ve_isa).Should().Be(7);
            }

            {
                var ya_isa = "يعيسي";

                GetCountOfWord(ya_isa).Should().Be(4);
            }

            {
                var bi_isa = "بعيسي";

                GetCountOfWord(bi_isa).Should().Be(2);
            }

        }


        [TestMethod]
        public void Cibril()
        {
            var searchText = "جبريل";


            ListOfVerseContains(searchText).Count().Should().Be(3);
            ListOfVerseContains(searchText).Select(x=>x.count).Sum().Should().Be(3);


        }


        [TestMethod]
        public void Kelime()
        {
            var text = "وَلِسُلَيْمَٰنَ ٱلرِّيحَ غُدُوُّهَا شَهْرٌ وَرَوَاحُهَا شَهْرٌ وَأَسَلْنَا لَهُۥ عَيْنَ ٱلْقِطْرِ وَمِنَ ٱلْجِنِّ مَن يَعْمَلُ بَيْنَ يَدَيْهِ بِإِذْنِ رَبِّهِۦ وَمَن يَزِغْ مِنْهُمْ عَنْ أَمْرِنَا نُذِقْهُ";

            var searchText = "شَهْرٌ";


            var kelime_arindirilmis_array = WordSearcher.AsClearArabicCharacterList(text);

            string.Join(Environment.NewLine, kelime_arindirilmis_array);


            var searchItems = new List<string>
            {
                searchText
            };

            foreach (var searchItem in searchItems)
            {
                var search = WordSearcher.AsClearArabicCharacterList(searchItem);

                var count = ListExtensions.Contains(kelime_arindirilmis_array, search);
            }


        }


        [TestMethod]
        public void TestMethod1()
        {
            AllSurahs.Length.Should().Be(114);
            AllSurahs[0].Index.Should().Be(1);
            AllSurahs[113].Index.Should().Be(114);


            var notMatchedList = new List<string>();

            foreach (var sura in AllSurahs)
            {
                foreach (var aya in sura.Verses)
                {
                    var matchInfoList = Analyzer.AnalyzeVerse(aya);


                    foreach (var matchInfo in matchInfoList.Where(x => x.HasNoMatch))
                    {
                        File.WriteAllText($"d:\\QuranAnalyzer\\{sura.Index}-{aya._index}.txt", System.Text.Json.JsonSerializer.Serialize(matchInfo,new JsonSerializerOptions{ WriteIndented = true}));
                    }

                    var notMatched = matchInfoList.Where(x => x.HasNoMatch).ToList();

                    notMatchedList.AddRange(notMatched.Select(x => x.ToString()).Distinct());
                }
            }

            notMatchedList = notMatchedList.Distinct().ToList();

            var aloha = string.Join(Environment.NewLine, notMatchedList);

            File.WriteAllText("d:\\QuranAnalyzer\\aa.txt", aloha);

        }



        [TestMethod]
        public void EndsWithNunVavNun()
        {
            const string nunVavNun = "نون";

            ListOfVerseEndsWith(nunVavNun).Count().Should().Be(133);
            ListOfVerseContains(nunVavNun).Count().Should().Be(103);
            ListOfVerseContains(nunVavNun).Select(x => x.count).Sum().Should().Be(113);

            //const string nunNun = "نن";
            //ListOfVerseContains(nunNun).Select(x=>x.count).Sum().Should().Be(19*19);
        }
    }
}
