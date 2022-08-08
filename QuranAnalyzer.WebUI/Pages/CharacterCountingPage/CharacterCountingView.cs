using System;
using System.Collections.Generic;
using System.Linq;
using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.MainPage;
using ReactWithDotNet;
using ReactWithDotNet.PrimeReact;
using static QuranAnalyzer.Analyzer;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;


[Serializable]
public class CharacterCountingViewModel
{
    public string ChapterFilter { get; set; }

    public string SearchCharacters { get; set; }
    
    public MushafOptions MushafOptions { get; set; } = new();

    public int ClickCount { get; set; }

    public bool IsBlocked { get; set; }

    public int SelectedTabIndex { get; set; }

    [NonSerialized] public IReadOnlyList<SummaryInfo> SummaryInfoList;
}


class CharacterCountingView : ReactComponent<CharacterCountingViewModel>
{
    #region Constructors
    public CharacterCountingView()
    {
        state = new CharacterCountingViewModel();

        StateInitialized += () =>
        {
            if (state.ChapterFilter == null)
            {
                var value = Context.Query[QueryKey.SearchQuery];
                if (value is not null)
                {
                    state.ChapterFilter    = value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).TryGet(0);
                    state.SearchCharacters = value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).TryGet(1);
                }
            }
        };
    }
    #endregion

    public void ComponentDidMount()
    {
        Context.ClientTask.ListenEvent("ArabicKeyboardPressed", nameof(ArabicKeyboardPressed));
    }

    public void ArabicKeyboardPressed(string letter)
    {
        state.SearchCharacters += " " + letter;
    }

    #region Public Methods
    public override Element render()
    {
        var searchPanel = new divWithBorder
        {
            style = { padding = "15px", minWidth = "300px", width = Context.ClientWidth >1000 ? "700px":"400px"},
            
            children =
            {
                new h4("Arama"),
                new VStack
                {
                    new VStack
                    {
                        new div { innerText       = "Sure:" },
                        new InputText { valueBind = () => state.ChapterFilter }
                    },

                    new VSpace(10),

                    new VStack
                    {
                        new div { innerText       = "Aranacak Karakterlerler" },
                        new InputText { valueBind = () => state.SearchCharacters, style = { direction = "ltr"}},
                        
                    },
                    new VSpace(3),
                    new Panel
                    {
                        header = "Arapça Klavye",
                        collapsed = true,
                        toggleable = true,
                        children =
                        {
                            new ArabicKeyboard()
                        }
                    },
                    new VSpace(20),

                    new Button
                    {
                        label     = "Ara",
                        onClick   = OnCaclculateClicked,
                        className = "p-button-outlined",
                        style     = { alignSelf = "flex-end", flexDirection = "column", paddingLeft = "50px", paddingRight = "50px" }
                    },

                    new VSpace(20),
                    new MushafOptionsView
                    {
                        MushafOption                = state.MushafOptions, 
                        Bestaten_7_69               = () => state.MushafOptions.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten,
                        UseElifReferencesFromTanzil = ()=>state.MushafOptions.UseElifReferencesFromTanzil,
                        CountHamzaAsAlif            = ()=>state.MushafOptions.CountHamzaAsAlif,

                    }
                }
            }
        };

        if (state.ClickCount == 0)
        {
            return searchPanel;
        }

        if (state.IsBlocked)
        {
            return new CalculatingComponent { searchPanel };
        }

        var searchLetters = AnalyzeText(state.SearchCharacters).Where(IsArabicLetter).ToList();

        state.SummaryInfoList = searchLetters.AsListOf(x => new SummaryInfo
        {
            Count = VerseFilter.GetVerseList(state.ChapterFilter).Then(verses => QuranAnalyzerMixin.GetCountOfCharacter(verses, x.ArabicLetterIndex,state.MushafOptions)).Value,
            Name  = x.MatchedLetter
        });


        var mushafVerse = new List<LetterColorizer>();
        
        foreach (var verse in VerseFilter.GetVerseList(state.ChapterFilter).Value)
        {
            if (verse.AnalyzedFullText.Any(x => searchLetters.Any(l=>l.ArabicLetterIndex == x.ArabicLetterIndex)))
            {
                var letterColorizer = new LetterColorizer
                {
                    VerseTextNodes = verse.AnalyzedFullText,
                    ChapterNumber = verse.ChapterNumber.ToString(),
                    VerseNumber = verse.Index,
                    LettersForColorizeNodes = searchLetters,
                    VerseText = verse.TextWithBismillah,
                    
                };
                
                mushafVerse.Add(letterColorizer);
            }
            
            
        }



        var results = new Card
        {
            title = "Sonuçlar",
            style = { marginTop = "5px" },
            children =
            {
                new TabView
                {
                    activeIndexBind = () => state.SelectedTabIndex,
                    children =
                    {
                        new TabPanel
                        {
                            header = "Özet",
                            children =
                            {
                                new CountsSummaryView { Counts = state.SummaryInfoList }
                            }
                        },
                        new TabPanel
                        {
                            header = "Mushaf Üzerinde Göster",
                            Children = mushafVerse
                        }
                    }
                }
            }
        };

       

        return new div
        {
            children = { searchPanel, results },
            style =
            {
                display = "flex", flexDirection = "column", alignItems = "stretch"
            }
        };
    }
    #endregion

    #region Methods
    void OnCaclculateClicked(string _)
    {
        state.ClickCount++;
        
        if (state.IsBlocked == false)
        {
            state.IsBlocked     = true;
            Context.ClientTask.PushHistory("", $"/?{QueryKey.Page}={PageId.CharacterCounting}&{QueryKey.SearchQuery}={state.ChapterFilter}|{state.SearchCharacters}");
            Context.ClientTask.GotoMethod(5, nameof(OnCaclculateClicked), _);
            return;
        }

        state.IsBlocked     = false;

       
    }
    #endregion
}