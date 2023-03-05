using System.Globalization;
using static QuranAnalyzer.ArabicLetterIndex;

namespace QuranAnalyzer;

public enum MushafId
{
    Tanzil = 1,
    RK = 2
}

public static class QuranAnalyzerMixin
{
    static readonly CultureInfo CultureInfoArabic = new("ar-SA");
    
    public static bool EqualsArabicIgnoreCase(this string a, string b)
    {
        return string.Compare(a, b, ignoreCase: true, CultureInfoArabic) == 0;
    }
    

    public static string GetDifferencesKeyForTanzil(string verseId)
    {
        return verseId + "|" + (int)MushafId.Tanzil;
    }

    public static string GetDifferencesKeyForRK(string verseId)
    {
        return verseId + "|" + (int)MushafId.RK;
    }

    public static readonly IReadOnlyDictionary<int, Dictionary<string, int>> MushafTotalCountPerVerseDifference = new Dictionary<int, Dictionary<string, int>>
    {
        {
            Nun, new()
            {
                // Chapter 68
                { GetDifferencesKeyForTanzil("68:1"), 3 },
                { GetDifferencesKeyForRK("68:1"), 4 }
            }
        },

         {
             Waaw, new()
             {
                 // Chapter 68
                 { GetDifferencesKeyForTanzil("68:1"), 3 },
                 { GetDifferencesKeyForRK("68:1"), 4 }
             }
         },

        {
            Saad, new()
            {
                { GetDifferencesKeyForTanzil("7:69"), 1 }, // bestaten: *بَسْطَةً*
                { GetDifferencesKeyForRK("7:69"), 0 }, // bestaten: *بَصْۜطَةً*
                
                { GetDifferencesKeyForTanzil("2:245"), 1 }, // yebsut: *رِضُ ٱللَّ*
                { GetDifferencesKeyForRK("2:245"), 0 } // yebsut: *رِضُ ٱللَّ*
            }
        },

        {
            Siin, new()
            {
                { GetDifferencesKeyForTanzil("7:69"), 0 }, // bestaten: *بَسْطَةً*
                { GetDifferencesKeyForRK("7:69"), 1 }, // bestaten: *بَصْۜطَةً*
                
                { GetDifferencesKeyForTanzil("2:245"), 1 }, // yebsut: *رِضُ ٱللَّ*
                { GetDifferencesKeyForRK("2:245"), 2 } // yebsut: *رِضُ ٱللَّ*
            }
        },

        {
            Laam, new()
            {
                // Chapter 11
                { GetDifferencesKeyForTanzil("11:70"), 9 },
                { GetDifferencesKeyForRK("11:70"), 8 },
                
                
                // Chapter 30
                { GetDifferencesKeyForTanzil("30:21"), 8 },
                { GetDifferencesKeyForRK("30:21"), 7 },
            }
        },

        {
            Alif, new()
            {
                // Chapter 2
                { GetDifferencesKeyForTanzil("2:31"), 17 },
                { GetDifferencesKeyForTanzil("2:62"), 22 },
                { GetDifferencesKeyForTanzil("2:65"), 7 },
                { GetDifferencesKeyForTanzil("2:81"), 7 },
                { GetDifferencesKeyForTanzil("2:108"), 11 },
                { GetDifferencesKeyForTanzil("2:123"), 11 },
                { GetDifferencesKeyForTanzil("2:145"), 26 },
                { GetDifferencesKeyForTanzil("2:282"), 108 },

                // Chapter 3
                { GetDifferencesKeyForTanzil("3:49"), 33 },
                { GetDifferencesKeyForTanzil("3:66"), 9 },
                { GetDifferencesKeyForTanzil("3:69"), 8 },
                { GetDifferencesKeyForTanzil("3:87"), 8 },
                { GetDifferencesKeyForTanzil("3:120"), 13 },
                { GetDifferencesKeyForTanzil("3:121"), 5 },
                { GetDifferencesKeyForTanzil("3:157"), 4 },
                { GetDifferencesKeyForTanzil("3:158"), 3 },
                { GetDifferencesKeyForTanzil("3:187"), 16 },

                // Chapter 7
                { GetDifferencesKeyForTanzil("7:204"), 9 },
                { GetDifferencesKeyForTanzil("7:165"), 18 },
                { GetDifferencesKeyForTanzil("7:149"), 12 },
                { GetDifferencesKeyForTanzil("7:134"), 11 },
                { GetDifferencesKeyForTanzil("7:131"), 17 },
                { GetDifferencesKeyForTanzil("7:129"), 13 },
                { GetDifferencesKeyForTanzil("7:127"), 17 },
                { GetDifferencesKeyForTanzil("7:101"), 15 },
                { GetDifferencesKeyForTanzil("7:97"), 10 },
                { GetDifferencesKeyForTanzil("7:95"), 20 },
                { GetDifferencesKeyForTanzil("7:90"), 10 },

                // Chapter 10
                { GetDifferencesKeyForTanzil("10:49"), 22 },
                { GetDifferencesKeyForTanzil("10:41"), 9 },
                { GetDifferencesKeyForTanzil("10:28"), 13 },
                { GetDifferencesKeyForTanzil("10:27"), 22 },
                { GetDifferencesKeyForTanzil("10:18"), 18 },

                // Chapter 11
                { GetDifferencesKeyForTanzil("11:107"), 14 },
                { GetDifferencesKeyForTanzil("11:74"), 7 },
                { GetDifferencesKeyForTanzil("11:62"), 18 },
                { GetDifferencesKeyForTanzil("11:53"), 10 },
                { GetDifferencesKeyForTanzil("11:35"), 10 },
                { GetDifferencesKeyForTanzil("11:29"), 22 },
                { GetDifferencesKeyForTanzil("11:9"), 8 },

                // Chapter 12
                { GetDifferencesKeyForTanzil("12:109"), 25 },
                { GetDifferencesKeyForTanzil("12:105"), 8 },
                { GetDifferencesKeyForTanzil("12:97"), 12 },
                { GetDifferencesKeyForTanzil("12:91"), 10 },
                { GetDifferencesKeyForTanzil("12:87"), 18 },
                { GetDifferencesKeyForTanzil("12:82"), 12 },
                { GetDifferencesKeyForTanzil("12:80"), 24 },
                { GetDifferencesKeyForTanzil("12:39"), 9 },
                { GetDifferencesKeyForTanzil("12:29"), 7 },
                { GetDifferencesKeyForTanzil("12:11"), 11 },

                // Chapter 13
                { GetDifferencesKeyForTanzil("13:41"), 15 },
                { GetDifferencesKeyForTanzil("13:38"), 18 },
                { GetDifferencesKeyForTanzil("13:36"), 24 },
                { GetDifferencesKeyForTanzil("13:33"), 20 },
                { GetDifferencesKeyForTanzil("13:31"), 45 },
                { GetDifferencesKeyForTanzil("13:29"), 8 },
                { GetDifferencesKeyForTanzil("13:14"), 18 },

                // Chapter 14
                { GetDifferencesKeyForTanzil("14:38"), 13 },
                { GetDifferencesKeyForTanzil("14:37"), 16 },
                { GetDifferencesKeyForTanzil("14:21"), 29 },
                { GetDifferencesKeyForTanzil("14:18"), 13 },

                // Chapter 15
                { GetDifferencesKeyForTanzil("15:68"), 5 },
                { GetDifferencesKeyForTanzil("15:19"), 10 },

                // ------------------ RK --------------------

                // Chapter 2
                { GetDifferencesKeyForRK("2:31"), 16 },
                { GetDifferencesKeyForRK("2:62"), 21 },
                { GetDifferencesKeyForRK("2:65"), 6 },
                { GetDifferencesKeyForRK("2:81"), 8 },
                { GetDifferencesKeyForRK("2:108"), 12 },
                { GetDifferencesKeyForRK("2:123"), 10 },
                { GetDifferencesKeyForRK("2:145"), 27 },
                { GetDifferencesKeyForRK("2:282"), 107 },

                // Chapter 3

                { GetDifferencesKeyForRK("3:49"), 35 },
                { GetDifferencesKeyForRK("3:66"), 10 },
                { GetDifferencesKeyForRK("3:69"), 9 },
                { GetDifferencesKeyForRK("3:87"), 9 },
                { GetDifferencesKeyForRK("3:120"), 14 },
                { GetDifferencesKeyForRK("3:121"), 6 },
                { GetDifferencesKeyForRK("3:157"), 5 },
                { GetDifferencesKeyForRK("3:158"), 4 },
                { GetDifferencesKeyForRK("3:187"), 17 },

                // Chapter 7
                { GetDifferencesKeyForRK("7:204"), 10 },
                { GetDifferencesKeyForRK("7:165"), 17 },
                { GetDifferencesKeyForRK("7:149"), 13 },
                { GetDifferencesKeyForRK("7:134"), 12 },
                { GetDifferencesKeyForRK("7:131"), 19 },
                { GetDifferencesKeyForRK("7:129"), 14 },
                { GetDifferencesKeyForRK("7:127"), 16 },
                { GetDifferencesKeyForRK("7:101"), 16 },
                { GetDifferencesKeyForRK("7:97"), 11 },
                { GetDifferencesKeyForRK("7:95"), 21 },
                { GetDifferencesKeyForRK("7:90"), 11 },

                // Chapter 10 
                { GetDifferencesKeyForRK("10:49"), 21 },
                { GetDifferencesKeyForRK("10:41"), 7 },
                { GetDifferencesKeyForRK("10:28"), 14 },
                { GetDifferencesKeyForRK("10:27"), 21 },
                { GetDifferencesKeyForRK("10:18"), 17 },

                // Chapter 11
                { GetDifferencesKeyForRK("11:107"), 13 },
                { GetDifferencesKeyForRK("11:74"), 6 },
                { GetDifferencesKeyForRK("11:62"), 19 },
                { GetDifferencesKeyForRK("11:53"), 11 },
                { GetDifferencesKeyForRK("11:35"), 9 },
                { GetDifferencesKeyForRK("11:29"), 21 },
                { GetDifferencesKeyForRK("11:9"), 7 },

                // Chapter 12 
                { GetDifferencesKeyForRK("12:109"), 24 },
                { GetDifferencesKeyForRK("12:105"), 7 },
                { GetDifferencesKeyForRK("12:97"), 11 },
                { GetDifferencesKeyForRK("12:91"), 9 },
                { GetDifferencesKeyForRK("12:87"), 16 },
                { GetDifferencesKeyForRK("12:82"), 11 },
                { GetDifferencesKeyForRK("12:80"), 23 },
                { GetDifferencesKeyForRK("12:39"), 10 },
                { GetDifferencesKeyForRK("12:29"), 6 },
                { GetDifferencesKeyForRK("12:11"), 10 },

                // Chapter 13
                { GetDifferencesKeyForRK("13:41"), 16 },
                { GetDifferencesKeyForRK("13:38"), 17 },
                { GetDifferencesKeyForRK("13:36"), 23 },
                { GetDifferencesKeyForRK("13:33"), 19 },
                { GetDifferencesKeyForRK("13:31"), 44 },
                { GetDifferencesKeyForRK("13:29"), 7 },
                { GetDifferencesKeyForRK("13:14"), 17 },

                // Chapter 14
                { GetDifferencesKeyForRK("14:38"), 12 },
                { GetDifferencesKeyForRK("14:37"), 15 },
                { GetDifferencesKeyForRK("14:21"), 28 },
                { GetDifferencesKeyForRK("14:18"), 12 },

                // Chapter 15
                { GetDifferencesKeyForRK("15:68"), 6 },
                { GetDifferencesKeyForRK("15:19"), 9 },
                
                
                
                
                
                // Chapter 29
                { GetDifferencesKeyForTanzil("29:10"), 24 },
                { GetDifferencesKeyForTanzil("29:20"), 16 },
                { GetDifferencesKeyForTanzil("29:23"), 11 },
                { GetDifferencesKeyForTanzil("29:61"), 8 },
                { GetDifferencesKeyForTanzil("29:63"), 15 },
                
                { GetDifferencesKeyForRK("29:10"), 25 },
                { GetDifferencesKeyForRK("29:20"), 15 },
                { GetDifferencesKeyForRK("29:23"), 12 },
                { GetDifferencesKeyForRK("29:61"), 9 },
                { GetDifferencesKeyForRK("29:63"), 16 },
                
                
                // Chapter 30
                { GetDifferencesKeyForTanzil("30:53"), 9 },
                { GetDifferencesKeyForTanzil("30:58"), 14 },

                { GetDifferencesKeyForRK("30:53"), 10 },
                { GetDifferencesKeyForRK("30:58"), 15 },
                
                
                // Chapter 31
                { GetDifferencesKeyForTanzil("31:12"), 10 },
                { GetDifferencesKeyForTanzil("31:21"), 22 },
                { GetDifferencesKeyForTanzil("31:31"), 11 },
                { GetDifferencesKeyForTanzil("31:32"), 16 },
                { GetDifferencesKeyForTanzil("31:33"), 25 },

                { GetDifferencesKeyForRK("31:12"), 9 },
                { GetDifferencesKeyForRK("31:21"), 21 },
                { GetDifferencesKeyForRK("31:31"), 9 },
                { GetDifferencesKeyForRK("31:32"), 15 },
                { GetDifferencesKeyForRK("31:33"), 24 },
                
                // Chapter 32
                { GetDifferencesKeyForTanzil("32:7"), 6 },
                { GetDifferencesKeyForTanzil("32:9"), 8 },
                { GetDifferencesKeyForTanzil("32:10"), 13 },
                { GetDifferencesKeyForTanzil("32:13"), 12 },
                { GetDifferencesKeyForTanzil("32:15"), 14 },
                { GetDifferencesKeyForTanzil("32:22"), 8 },

                { GetDifferencesKeyForRK("32:7"), 5 },
                { GetDifferencesKeyForRK("32:9"), 7 },
                { GetDifferencesKeyForRK("32:10"), 11 },
                { GetDifferencesKeyForRK("32:13"), 11 },
                { GetDifferencesKeyForRK("32:15"), 13 },
                { GetDifferencesKeyForRK("32:22"), 7 },

            }
        }
    };

    #region Public Methods
    public static int GetCountOfLetter(IReadOnlyList<Verse> verseList, int arabicLetterIndex, MushafOption option = null, bool includeBismillah = true)
    {
        option ??= new MushafOption();

        return verseList.Select(x => GetCountOfLetterInVerse(x, arabicLetterIndex, option, includeBismillah)).Sum();
    }

    public static int GetCountOfLetterInVerse(Verse verse, int arabicLetterIndex, MushafOption option, bool includeBismillah)
    {
        if (verse == null)
        {
            throw new ArgumentNullException(nameof(verse));
        }

        if (option == null)
        {
            throw new ArgumentNullException(nameof(option));
        }

        if (arabicLetterIndex == Alif)
        {
            if (option.UseElifReferencesFromTanzil == false)
            {
                if (MushafTotalCountPerVerseDifference[Alif].TryGetValue(GetDifferencesKeyForRK(verse.Id), out var totalCount))
                {
                    return totalCount;
                }
            }
        }

        if (arabicLetterIndex == Saad)
        {
            if (option.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten == false)
            {
                if (MushafTotalCountPerVerseDifference[Saad].TryGetValue(GetDifferencesKeyForRK(verse.Id), out var totalCount))
                {
                    return totalCount;
                }
            }
        }

        if (arabicLetterIndex == Siin)
        {
            if (option.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten == false)
            {
                if (MushafTotalCountPerVerseDifference[Siin].TryGetValue(GetDifferencesKeyForRK(verse.Id), out var totalCount))
                {
                    return totalCount;
                }
            }
        }

        if (arabicLetterIndex == Laam)
        {
            if (option.Use_Laam_SpecifiedByTanzil == false)
            {
                if (MushafTotalCountPerVerseDifference[Laam].TryGetValue(GetDifferencesKeyForRK(verse.Id), out var totalCount))
                {
                    return totalCount;
                }
            }
        }

        if (arabicLetterIndex == Nun)
        {
            if (option.Chapter_68_Should_Single_Nun == false)
            {
                if (MushafTotalCountPerVerseDifference[Nun].TryGetValue(GetDifferencesKeyForRK(verse.Id), out var totalCount))
                {
                    return totalCount;
                }
            }
            else
            {
                if (MushafTotalCountPerVerseDifference[Nun].TryGetValue(GetDifferencesKeyForTanzil(verse.Id), out var totalCount))
                {
                    return totalCount;
                }
            }
        }

        if (arabicLetterIndex == Waaw)
        {
            if (option.Chapter_68_Should_Single_Nun == false)
            {
                if (MushafTotalCountPerVerseDifference[Waaw].TryGetValue(GetDifferencesKeyForRK(verse.Id), out var totalCount))
                {
                    return totalCount;
                }
            }
            else
            {
                if (MushafTotalCountPerVerseDifference[Waaw].TryGetValue(GetDifferencesKeyForTanzil(verse.Id), out var totalCount))
                {
                    return totalCount;
                }
            }

            if (!option.Enba_u_Should_Contains_one_waw)
            {
                // [enba'u] Tanzil.net counts extra waw char in these verses
                if (verse.Id == "6:5")
                {
                    return 4;
                }

                if (verse.Id == "26:6")
                {
                    return 3;
                }
            }

            if (!option._75_13_yunebbeu_Should_Contains_1_waw)
            {
                if (verse.Id == "75:13")
                {
                    return 2;
                }
            }
        }

        if (arabicLetterIndex == Yaa)
        {
            // Tanzil.net has a bug here. There mush be extra ye here according to utmaine mushaf
            if (!option.Ya_sahibeyi_Should_Contains_2_ya)
            {
                // [ ya sahibeyi ] - [يَا صَاحِبَيِ]
                if (verse.Id == "12:39")
                {
                    return 4;
                }

                if (verse.Id == "12:41")
                {
                    return 11;
                }
            }
            
        }

        


        return (includeBismillah ? verse.TextWithBismillahAnalyzed : verse.TextAnalyzed).Count(x => x.ArabicLetterIndex == arabicLetterIndex);
    }
    #endregion



    
}