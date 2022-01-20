using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json.Serialization;

namespace KoranAnalyzer
{
    



    public class sura
    {
        public int Index => int.Parse(_index);

        public aya[] aya { get; set; }
        public string _index { get; set; }
        public string  _name { get; set; }
    }

    public class aya
    {
        public string _index { get; set; }
        public string _text { get; set; }
        public string _bismillah { get; set; }
    }

    public static class DataAccess
    {
        public static readonly sura[] AllSura = System.Text.Json.JsonSerializer.Deserialize<sura[]>(File.ReadAllText(@"D:\work\git\QuranAnalyzer\QuranAnalyzer\Data.json"));

        public static string[] harfler = {
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

        public class MatchInfo
        {
            public int StartIndex { get; set; }

            public int HarfIndex { get; set; }

            public aya aya;

            public bool HasNoMatch => HarfIndex == -1;

            public override string ToString()
            {
                if (aya != null)
                {
                    if (HarfIndex >= 0)
                    {
                        return harfler[HarfIndex];
                    }

                    return aya._text[StartIndex].ToString();

                }

                return string.Empty;
            }
        }
        public static MatchInfo TryRead( aya aya, int startIndex, bool isHemzeActive)
        {
            string line = aya._text+aya._bismillah;

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
                            aya        = aya
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
                        arr = new[]{ "ٱ", "إ", "أ", "ﺍ", "ء", "ٔ" };
                    }
                    else
                    {
                        arr = new[] { "ٱ", "إ", "أ", "ﺍ" };
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

        public static IReadOnlyList<MatchInfo> Analyze(aya aya,  bool isHemzeActive = true)
        {
            var line  = aya._text + aya._bismillah;

            var items = new List<MatchInfo>();

            var cursor = 0;

            while (cursor<line.Length)
            {
                var item = TryRead(aya, cursor, isHemzeActive);
                if (item != null)
                {
                    items.Add(item);
                    cursor += harfler[item.HarfIndex].Length;
                    continue;
                }

                items.Add(new MatchInfo{ HarfIndex = -1, StartIndex = cursor, aya = aya });

                cursor++;
            }

            return items;
        }







    }
}
