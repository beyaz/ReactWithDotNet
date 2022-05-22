using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace QuranAnalyzer;

[Serializable]
public sealed class Surah
{
    public int Index => int.Parse(_index);
    public IReadOnlyList<Verse> Verses => aya;

    public Verse[] aya { get; set; }
    public string _index { get; set; }
    public string _name { get; set; }
}

[Serializable]
public sealed class Verse
{
    public string _index { get; set; }
    public string _text { get; set; }
    public string _bismillah { get; set; }
    public int SurahNumber { get; set; }
}

public static class DataAccess
{
    public static readonly Surah[] AllSurahs = JsonSerializer.Deserialize<Surah[]>(File.ReadAllText(@"D:\work\git\QuranAnalyzer\QuranAnalyzer\Data.json"));

    public static string[] harfler =
    {
        "ا",
        "ب",
        "ت",
        "ث",
        "ج‎",
        "ح",
        "خ",
        "د",
        "ذ",
        "ر",
        "ز‎",
        "س",
        "ش",
        "ص",
        "ض",
        "ط",
        "ظ",
        "ع",
        "غ",
        "ف",
        "ق",
        "ك",
        "ل",
        "م",
        "ن",
        "ه",
        "و",
        "ي"
    };

    public static int[] harflerebced =
    {
        1,
        2,
        400,
        500,
        3,
        8,
        600,
        4,
        700,
        200,
        7,
        60,
        300,
        90,
        800,
        9,
        900,
        70,
        1000,
        80,
        100,
        20,
        30,
        40,
        50,
        5,
        6,
        10
    };

    public static MatchInfo TryRead(Verse verse, int startIndex, bool isHemzeActive)
    {
        string line = verse._bismillah + verse._text;

        for (var i = 0; i < harfler.Length; i++)
        {
            MatchInfo tryMatch(string searchCharacter)
            {
                if (startIndex + searchCharacter.Length >= line.Length)
                {
                    return null;
                }

                var value = line.Substring(startIndex, searchCharacter.Length);

                var isMatch = value == searchCharacter;
                if (isMatch)
                {
                    return new MatchInfo
                    {
                        StartIndex = startIndex,
                        HarfIndex  = i,
                        verse      = verse
                    };
                }

                return null;
            }

            //Elif harfi için ayrıca tarama (+)
            if (i == 0)
            {
                string[] arr = null;

                //Hemze dahil ediliyor. Elif olarak
                if (isHemzeActive)
                {
                    arr = new[] {"ٱ", "إ", "أ", "ﺍ", "ء", "ٔ"};
                }
                else
                {
                    arr = new[] {"ٱ", "إ", "أ", "ﺍ"};
                }

                foreach (var item in arr)
                {
                    var matchInfo = tryMatch(item);
                    if (matchInfo != null)
                    {
                        return matchInfo;
                    }
                }
            }

            //harfi için ayrıca tarama (+)"ز"
            if (i == 10)
            {
                var matchInfo = tryMatch("ز");
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }

            // harfi için ayrıca tarama (+) "ج"
            if (i == 4)
            {
                var matchInfo = tryMatch("ج");
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }

            // harfi için ayrıca tarama (+) "ة"
            if (i == 25)
            {
                var matchInfo = tryMatch("ة");
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }

            // harfi için ayrıca tarama (+) "ى" ve "ئ"
            if (i == 27)
            {
                var matchInfo = tryMatch("ى");
                if (matchInfo != null)
                {
                    return matchInfo;
                }

                matchInfo = tryMatch("ئ");
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }

            // harfi için ayrıca tarama (+) "ٯ" ve "ؤ"
            if (i == 26)
            {
                var matchInfo = tryMatch("ٯ");
                if (matchInfo != null)
                {
                    return matchInfo;
                }

                matchInfo = tryMatch("ؤ");
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }

            // normal match
            {
                var matchInfo = tryMatch(harfler[i]);
                if (matchInfo != null)
                {
                    return matchInfo;
                }
            }
        }

        return null;
    }

    public static IReadOnlyList<MatchInfo> Analyze(Verse verse, bool isHemzeActive = true)
    {
        var line = verse._bismillah + verse._text;

        var items = new List<MatchInfo>();

        var cursor = 0;

        while (cursor < line.Length)
        {
            var item = TryRead(verse, cursor, isHemzeActive);
            if (item != null)
            {
                items.Add(item);
                cursor += harfler[item.HarfIndex].Length;
                continue;
            }

            items.Add(new MatchInfo {HarfIndex = -1, StartIndex = cursor, verse = verse});

            cursor++;
        }

        return items;
    }
}