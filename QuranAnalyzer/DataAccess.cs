using System.IO;
using System.Text.Json;
using System.Xml;
using System.Xml.Serialization;
using ReactWithDotNet;
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

            var filePath = @"quran-uthmani.xml";

            if (File.Exists(path + filePath))
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

    static IReadOnlyList<Chapter> ReadAllChaptersFromJsonfile(string xmlFilePath)
    {
        XmlSerializer ser = new XmlSerializer(typeof(Quran));
        Quran         quran;
        using (XmlReader reader = XmlReader.Create(xmlFilePath))
        {
            quran = (Quran)ser.Deserialize(reader);
        }

        var chapters = quran.Sura;

        return chapters.AsListOf(chapter => new Chapter
        {
            Name   = chapter.Name,
            Index  = int.Parse(chapter.Index),
            Verses = chapter.Aya.AsListOf(v => toVerse(chapter, v))
        });

        static Verse toVerse(Sura chapter, Aya v)
        {
            var textWithBismillah = v.Bismillah + " " + v.Text;

            var analyzedFullText = AnalyzeText(textWithBismillah);

            var analyzedText = AnalyzeText(v.Text);

            return new Verse
            {
                Index                     = v.Index,
                IndexAsNumber             = int.Parse(v.Index),
                Bismillah                 = v.Bismillah,
                Text                      = v.Text,
                TextAnalyzed              = analyzedText,
                TextWordList              = analyzedText.GetWords(),
                ChapterNumber             = int.Parse(chapter.Index),
                Id                        = $"{chapter.Index}:{v.Index}",
                TextWithBismillahWordList = analyzedFullText.GetWords(),
                TextWithBismillahAnalyzed = analyzedFullText,
                TextWithBismillah         = textWithBismillah
            };
        }
    }





    [XmlRoot(ElementName = "aya")]
    public class Aya
    {
        [XmlAttribute(AttributeName = "index")]
        public string Index { get; set; }
        [XmlAttribute(AttributeName = "text")]
        public string Text { get; set; }
        [XmlAttribute(AttributeName = "bismillah")]
        public string Bismillah { get; set; }
    }

    [XmlRoot(ElementName = "sura")]
    public class Sura
    {
        [XmlElement(ElementName = "aya")]
        public List<Aya> Aya { get; set; }
        [XmlAttribute(AttributeName = "index")]
        public string Index { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
    }

    [XmlRoot(ElementName = "quran")]
    public class Quran
    {
        [XmlElement(ElementName = "sura")]
        public List<Sura> Sura { get; set; }
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