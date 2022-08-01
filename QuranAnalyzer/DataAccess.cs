using System.IO;
using System.Text.Json;
using static QuranAnalyzer.Analyzer;

namespace QuranAnalyzer;

public static class DataAccess
{
    public static readonly IReadOnlyList<Surah> AllSurahs;
    
    static DataAccess()
    {
        var chapters = JsonSerializer.Deserialize<Surah_[]>(File.ReadAllText(@"D:\work\git\QuranAnalyzer\QuranAnalyzer\Data.json"));

        AllSurahs = chapters.AsListOf(chapter => new Surah
        {
            Name  = chapter._name,
            Index = int.Parse(chapter._index),
            Verses = chapter.aya.AsListOf(v => new Verse
            {
                Index            = v._index,
                Bismillah        = v._bismillah,
                Text             = v._text,
                ChapterNumber    = int.Parse(chapter._index),
                Id               = $"{chapter._index}:{v._index}",
                WordList         = v._text.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).AsListOf(word => AnalyzeText(word)),
                AnalyzedFullText = AnalyzeText(v._bismillah + v._text)
            })
        });
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
public sealed class Surah
{
    public int Index { get; init; }
    public string Name { get; init; }
    public IReadOnlyList<Verse> Verses { get; init; }
}

[Serializable]
public sealed class Verse
{
    /// <summary>
    ///     bismillah + text
    /// </summary>
    public IReadOnlyList<LetterMatchInfo> AnalyzedFullText { get; init; }

    public string Bismillah { get; init; }
    public int ChapterNumber { get; init; }

    public string Id { get; init; }
    public string Index { get; init; }
    public string Text { get; init; }

    public IReadOnlyList<IReadOnlyList<LetterMatchInfo>> WordList { get; init; }
}