using System.IO;
using System.Text.Json;
using static QuranAnalyzer.Analyzer;

namespace QuranAnalyzer;





public static class DataAccess
{
    class Verse_
    {
        public string _index { get; set; }
        public string _text { get; set; }
        public string _bismillah { get; set; }
    }

    class Surah_
    {
        public Verse_[] aya { get; set; }
        public string _index { get; set; }
        public string _name { get; set; }
    }

    public static readonly IReadOnlyList<Surah> AllSurahs;

    static DataAccess()
    {
        var chapters    = JsonSerializer.Deserialize<Surah_[]>(File.ReadAllText(@"D:\work\git\QuranAnalyzer\QuranAnalyzer\Data.json"));

        AllSurahs = chapters.AsListOf(chapter => new Surah
        {
            _index = chapter._index,
            _name  = chapter._name,
            Index  = int.Parse(chapter._index),
            Verses = chapter.aya.AsListOf(v => new Verse
            {
                Index           = v._index,
                Bismillah       = v._bismillah,
                Text            = v._text,
                ChapterNumber    = int.Parse(chapter._index),
                Id               = $"{chapter._index}:{v._index}",
                WordList         = v._text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).AsListOf(word=>AnalyzeText(word)),
                AnalyzedFullText = AnalyzeText(v._bismillah + v._text)
                
            })

        });

    }
    
}

[Serializable]
public sealed class Surah
{
    public string _index { get; init; }
    public string _name { get; init; }
    public int Index { get; init; }
    public IReadOnlyList<Verse> Verses { get; init; }
}

[Serializable]
public sealed class Verse
{
    public string Index { get; init; }
    public string Text { get; init; }
    public string Bismillah { get; init; }
    public int ChapterNumber { get; init; }

    public string Id { get; init; }

    public IReadOnlyList<IReadOnlyList<LetterMatchInfo>> WordList { get; init; }

    /// <summary>
    ///     bismillah + text
    /// </summary>
    public IReadOnlyList<LetterMatchInfo> AnalyzedFullText { get; init; }
}