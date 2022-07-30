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
    public int ChapterNumber { get; set; }

    public string Id => $"{ChapterNumber}:{_index}";
}

public static class DataAccess
{
    public static readonly Surah[] AllSurahs = JsonSerializer.Deserialize<Surah[]>(File.ReadAllText(@"D:\work\git\QuranAnalyzer\QuranAnalyzer\Data.json"));
}