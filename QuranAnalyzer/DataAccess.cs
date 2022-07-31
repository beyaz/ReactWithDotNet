using System.IO;
using System.Text.Json;

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

        AllSurahs = chapters.Select(chapter => new Surah
        {
            _index = chapter._index,
            _name  = chapter._name,
            Index  = int.Parse(chapter._index),
            Verses = chapter.aya.Select(v => new Verse
            {
                _index        = v._index,
                _bismillah    = v._bismillah,
                _text         = v._text,
                ChapterNumber = int.Parse(chapter._index),
                Id            = $"{chapter._index}:{v._index}"
            }).ToList()

        }).ToList();

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
    public string _index { get; init; }
    public string _text { get; init; }
    public string _bismillah { get; init; }
    public int ChapterNumber { get; init; }

    public string Id { get; init; }
}