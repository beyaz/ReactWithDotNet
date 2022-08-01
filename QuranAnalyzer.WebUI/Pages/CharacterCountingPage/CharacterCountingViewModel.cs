using System;
using System.Collections.Generic;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

[Serializable]
public class CharacterCountingViewModel2
{
    public string SelectedFact { get; set; }
    public string SummaryText { get; set; }

    
    public bool IsBlocked { get; set; }

    public string ChapterFilter { get; set; }

    public string SearchCharacters { get; set; }

    public int CountOfCharacters { get; set; }
    

    public int SelectedTabIndex { get; set; }

    [NonSerialized] 
    public Occurence[] ResultRecords;

    public double AvailableWidth { get; set; }

    [NonSerialized] public IReadOnlyList<SummaryInfo> SummaryInfoList;

    public MushafOptions MushafOptions { get; set; } = new();

    public int ClickCount { get; set; }
}

[Serializable]
public sealed class Occurence
{
    public int? Charachter1 { get; set; }
    public int? Charachter2 { get; set; }
    public int? Charachter3 { get; set; }
    public int? Charachter4 { get; set; }
    public int? Charachter5 { get; set; }
    public int? Charachter6 { get; set; }
    public int? Charachter7 { get; set; }
    public int? Charachter8 { get; set; }
    public int? Charachter9 { get; set; }

    public string VerseId { get; set; }



}