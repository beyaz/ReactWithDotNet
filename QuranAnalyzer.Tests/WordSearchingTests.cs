global using static QuranAnalyzer.QuranQuery;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static QuranAnalyzer.Analyzer;
using static QuranAnalyzer.DataAccess;



namespace QuranAnalyzer
{


    [TestClass]
    public class WordSearchingTests
    {


        [TestMethod]
        public void Day_365()
        {
            const string yevm    = "يوم";
            const string ve_yevm = "ويوم";

            const string el_yevm = "اليوم";
            const string vel_yevm = "واليوم";
            const string yevmen = "يوما";
            const string li_yevm = "ليوم";
            const string fel_yevm = "فاليوم";

            const string bi_yevm = "بيوم";
            const string bil_yevm = "باليوم";
            const string vebil_yevm = "وباليوم";
            

            getCountInAllChapter(yevm).Should().Be(217);
            getCountInAllChapter(ve_yevm).Should().Be(44);
            getCountInAllChapter(el_yevm).Should().Be(41);
            getCountInAllChapter(vel_yevm).Should().Be(23);
            getCountInAllChapter(yevmen).Should().Be(16);
            getCountInAllChapter(li_yevm).Should().Be(8);
            getCountInAllChapter(fel_yevm).Should().Be(8);
            getCountInAllChapter(bi_yevm).Should().Be(5);
            getCountInAllChapter(bil_yevm).Should().Be(2);
            getCountInAllChapter(vebil_yevm).Should().Be(1);




            //static int getCountInAllChapter(string searchWord)
            //{
            //    return VerseFilter.GetVerseList("*").Value.Sum(x => ToWordList(x._text).Count(word => isSame(word, searchWord))).Value;
            //}

            static int getCountInAllChapter(string searchWord)
            {
                var search = AnalyzeText(searchWord);

                return VerseFilter.GetVerseList("*").Value.Sum(x => ToWordList(x._text).Count(word => isSame(word, searchWord))).Value;
            }

            static IReadOnlyList<string> ToWordList(string sentence)
            {
                return sentence.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            }
            
            static bool isSame(string word, string search)
            {
                return AnalyzeText(word).HasValueAndSame(AnalyzeText(search));
            }
        }


        [TestMethod]
        public void AnalyzerTest()
        {
            var nunVavNun = AnalyzeText("نون");

            var total = 0;
            foreach (var sura in AllSurahs)
            {
                foreach (var aya in sura.Verses)
                {
                    var lastWord  = aya._text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Last();

                    var isEndsWith = AnalyzeText(lastWord).EndsWith(nunVavNun);
                    if (isEndsWith)
                    {
                        total++;
                    }
                }
            }

            total.Should().Be(133);

            var source = "ءَادَمَ";
            var adem   = "اادم";

            AnalyzeText(source).HasValueAndSame(AnalyzeText(adem)).Should().BeTrue();


            source  = "يَـٰٓـَٔادَمُ";
            var ya_adem = "يادم";


            var arr = WordSearcher_old.AsClearArabicCharacterList(source);


            AnalyzeText(source).HasValueAndSame(AnalyzeText(ya_adem)).Should().BeTrue();

        }

       

        static int GetCountOfWord(string searchWord)
        {
            var searchList = WordSearcher_old.AsClearArabicCharacterList(searchWord);

            var total = 0;
            foreach (var sura in AllSurahs)
            {
                foreach (var aya in sura.Verses)
                {
                    foreach (var word in aya._text.Split(' ', StringSplitOptions.RemoveEmptyEntries))
                    {
                        var isSame = WordSearcher_old.AsClearArabicCharacterList(word).HasValueAndSame(searchList);
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


            var kelime_arindirilmis_array = WordSearcher_old.AsClearArabicCharacterList(text);

            string.Join(Environment.NewLine, kelime_arindirilmis_array);


            var searchItems = new List<string>
            {
                searchText
            };

            foreach (var searchItem in searchItems)
            {
                var search = WordSearcher_old.AsClearArabicCharacterList(searchItem);

                var count = ListExtensions.Contains(kelime_arindirilmis_array, search);
            }


        }


        [TestMethod]
        public void TestMethod1()
        {
            AllSurahs.Count.Should().Be(114);
            AllSurahs[0].Index.Should().Be(1);
            AllSurahs[113].Index.Should().Be(114);


            var notMatchedList = new List<string>();

            foreach (var sura in AllSurahs)
            {
                foreach (var aya in sura.Verses)
                {
                    var matchInfoList = AnalyzeVerse(aya);


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
