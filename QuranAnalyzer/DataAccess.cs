using System.IO;
using System.Text.Json;
using static QuranAnalyzer.Analyzer;

namespace QuranAnalyzer;

public static class DataAccess
{
    public static readonly IReadOnlyList<Chapter> AllChapters;

    static DataAccess()
    {
        AllChapters = ReadAllChaptersFromJsonfile(GetFilePath());
        
        

        static string GetFilePath()
        {
            var path = Path.GetDirectoryName(typeof(DataAccess).Assembly.Location) + Path.DirectorySeparatorChar;
            
            var filePath = @"Data.json";

            if (File.Exists(path+filePath))
            {
                return path + filePath;
            }

            if (!File.Exists(filePath))
            {
                filePath = Path.Combine("bin", "Debug", "netcoreapp3.1", filePath);
            }

            return filePath;
        }
    }

    static IReadOnlyList<Chapter> ReadAllChaptersFromJsonfile(string jsonFilePath)
    {
        var chapters = JsonSerializer.Deserialize<Surah_[]>(File.ReadAllText(jsonFilePath));

        return chapters.AsListOf(chapter => new Chapter
        {
            Name   = chapter._name,
            Index  = int.Parse(chapter._index),
            Verses = chapter.aya.AsListOf(v => toVerse(chapter, v))
        });

        static Verse toVerse(Surah_ chapter, Verse_ v)
        {
            var textWithBismillah = v._bismillah + " " + v._text;

            var analyzedFullText = AnalyzeText(textWithBismillah);

            var analyzedText = AnalyzeText(v._text);

            return new Verse
            {
                Index                     = v._index,
                IndexAsNumber             = int.Parse(v._index),
                Bismillah                 = v._bismillah,
                Text                      = v._text,
                TextAnalyzed              = analyzedText,
                TextWordList              = analyzedText.GetWords(),
                ChapterNumber             = int.Parse(chapter._index),
                Id                        = $"{chapter._index}:{v._index}",
                TextWithBismillahWordList = analyzedFullText.GetWords(),
                TextWithBismillahAnalyzed = analyzedFullText,
                TextWithBismillah         = textWithBismillah
            };
        }
    }

    class Surah_
    {
        public string _index { get; set; }
        public string _name { get; set; }
        public Verse_[] aya { get; set; }
    }

    class Verse_
    {
        public string _bismillah { get; set; }
        public string _index { get; set; }
        public string _text { get; set; }
    }
}

[Serializable]
public sealed class Chapter
{
    public int Index { get; init; }
    public string Name { get; init; }
    public IReadOnlyList<Verse> Verses { get; init; }
}

[Serializable]
public sealed class Verse
{
    public string Bismillah { get; init; }
    public int ChapterNumber { get; init; }

    public string Id { get; init; }
    public string Index { get; init; }
    public int IndexAsNumber { get; init; }
    public string Text { get; init; }
    public IReadOnlyList<LetterInfo> TextAnalyzed { get; set; }
    public string TextWithBismillah { get; init; }

    /// <summary>
    ///     bismillah + text
    /// </summary>
    public IReadOnlyList<LetterInfo> TextWithBismillahAnalyzed { get; init; }

    public IReadOnlyList<IReadOnlyList<LetterInfo>> TextWithBismillahWordList { get; init; }
    
    public IReadOnlyList<IReadOnlyList<LetterInfo>> TextWordList { get; set; }
}