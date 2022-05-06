using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace QuranAnalyzer
{
    public static class WordSearcher
    {
        public static IReadOnlyList<string> AsClearArabicCharacterList(this string data)
        {
            //Arapça tüm harfler. Faklı yazılımları ile
            var harfler = new string[]{
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
 "ي",


//Ek harfler
 "ٱ",
 "إ",
 "أ",
 "ﺍ",
 "ز",
 "ج",
 "ة",
 "ى",
 "ئ",
 "ٯ",
 "ؤ",
 "ء",


                };

            var replaceMap   = new[]
            {
                //Elifler tek elif haline getiriliyor
                //Hemzeler elif harfine dönüştürülüyor
                new
                {
                    arr = new[]
                    {
                    "ٱ",
                    "إ",
                    "أ",
                    "ﺍ",
                    "ء"
                    },
                    target ="ا"
                },

                new
                {
                    arr = new[]
                    {
                        "ز"
                    },
                    target = "ز‎"
                },

                new
                {
                    arr = new[]
                    {
                        "ج"
                    },
                    target = "ج"
                },

                new
                {
                    arr = new[]
                    {
                        "ة"
                    },
                    target = "ه"
                },

                new
                {
                    arr = new[]
                    {
                        "ى","ئ"
                    },
                    target = "ي"
                },

                new
                {
                    arr = new[]
                    {
                        "ٯ","ؤ"
                    },
                    target = "و"
                },
            };

            var notRecognized = new List<char>();
            var items = new List<string>();

            var position = 0;
            while (position < data.Length)
            {
                bool tryAdd()
                {
                    foreach (var part in replaceMap)
                    {
                        foreach (var item in part.arr)
                        {
                            if (position + item.Length <= data.Length)
                            {
                                var isMatched = data.Substring(position, item.Length) == item;
                                if (isMatched)
                                {
                                    position += item.Length;

                                    items.Add(part.target);

                                    return true;
                                }
                            }
                        }
                    }

                    return false;
                }

                var isFound = tryAdd();
                if (isFound)
                {
                    continue;
                }

                bool tryAddNormally()
                {
                    foreach (var item in harfler)
                    {
                        if (position + item.Length <= data.Length)
                        {
                            var isMatched = data.Substring(position, item.Length) == item;
                            if (isMatched)
                            {
                                position += item.Length;

                                items.Add(item);

                                return true;
                            }
                        }
                    }

                    return false;
                }

                isFound = tryAddNormally();
                if (isFound)
                {
                    continue;
                }

                notRecognized.Add(data[position]);

                int a               = data[position];
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(data[position]);

                var digitValue = CharUnicodeInfo.GetDigitValue(data[position]);


                position++;

            }

            if (notRecognized.Count>0)
            {
                notRecognized.ToList();
            }

            return items;
        }
    }
}