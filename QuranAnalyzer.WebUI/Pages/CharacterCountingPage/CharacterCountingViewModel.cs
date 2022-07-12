using System;
using System.Collections.Generic;
using ReactDotNet;
using ReactDotNet.Html5;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

[Serializable]
public class CharacterCountingViewModel
{
    public string SelectedFact { get; set; }
    public string SummaryText { get; set; }

    public ClientTask ClientTask { get; set; }
    public string OperationName { get; set; }
    public bool IsBlocked { get; set; }

    public string ChapterFilter { get; set; }

    public string SearchCharacters { get; set; }

    public int CountOfCharacters { get; set; }
    

    public int SelectedTabIndex { get; set; }

    [NonSerialized] 
    public Occurence[] ResultRecords;

    public double AvailableWidth { get; set; }
    public IReadOnlyList<SummaryInfo> SummaryInfoList { get; set; }
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

    public string VerseNumber { get; set; }



}