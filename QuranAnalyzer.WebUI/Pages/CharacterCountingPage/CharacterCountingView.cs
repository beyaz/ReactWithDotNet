using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using QuranAnalyzer.WebUI.Components;
using ReactWithDotNet;
using ReactWithDotNet.PrimeReact;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

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
                var value = Context.Query["q"];
                if (value is not null)
                {
                    state.ChapterFilter    = value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).TryGet(0);
                    state.SearchCharacters = value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries).TryGet(1);
                }
            }
        };
    }
    #endregion

    #region Public Methods
    public override Element render()
    {
        var searchPanel = new divWithBorder
        {
            style = { padding = "15px", minWidth = "300px" },
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
                        new InputText { valueBind = () => state.SearchCharacters }
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

        var summaryContent = new HStack
        {
            new div { innerText = state.SummaryText },
            new div { innerText = state.CountOfCharacters.ToString(), style = { marginLeft = "5px", marginRight = "5px" } },
        };

        if (state.CountOfCharacters % 19 == 0)
        {
            summaryContent.Add(new div { innerText = "(" });
            summaryContent.Add(new div { innerText = "19 x " + state.CountOfCharacters / 19, style = { color = "red", marginLeft = "5px", marginRight = "5px" } });
            summaryContent.Add(new div { innerText = ")" });
        }

        var resultColumns = new List<Column>
        {
            new() { field = nameof(Occurence.VerseId), header = "Ayet No" }
        };

        foreach (var charachter in Analyzer.AnalyzeText(state.SearchCharacters).Where(x=>x.ArabicLetterIndex>=0))
        {
            var propertyName = "Charachter" + (Analyzer.AnalyzeText(state.SearchCharacters).Where(x => x.ArabicLetterIndex >= 0).ToList().IndexOf(charachter) + 1);

            resultColumns.Add(new Column { field = propertyName, header = charachter.MatchedLetter });
        }

        var dt = new DataTable
        {
            scrollHeight = "300px",
            scrollable   = true,
            value        = state.ResultRecords,
        };

        dt.children.AddRange(resultColumns);

        var matchRecords = QuranAnalyzerMixin.SearchCharachters(state.ChapterFilter, state.SearchCharacters, state.MushafOptions).Value;

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
                            header = "Detaylı Tablo",
                            children =
                            {
                                dt
                            }
                        },
                        new TabPanel
                        {
                            header = "Mushaf Üzerinde Göster",
                            children =
                            {
                                applyFontSize(CharachterSearchResultColorizer.ColorizeCharachterSearchResults(matchRecords, Analyzer.AnalyzeText(state.SearchCharacters).Where(x => x.ArabicLetterIndex >= 0).Select(x=>x.MatchedLetter).ToList()))
                            }
                        }
                    }
                }
            }
        };

        Element applyFontSize(HtmlElement el)
        {
            if (state.AvailableWidth < 500)
            {
                el.style.fontSize = "9px";
                return el;
            }

            el.style.fontSize = "19px";

            return el;
        }

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
            state.ResultRecords = null;
            state.IsBlocked     = true;
            Context.ClientTask.PushHistory("", $"/index.html?page=CharacterCounting&q={state.ChapterFilter}|{state.SearchCharacters}");
            Context.ClientTask.GotoMethod(5, nameof(OnCaclculateClicked), _);
            return;
        }

        var matchRecords = QuranAnalyzerMixin.SearchCharachters(state.ChapterFilter, state.SearchCharacters, state.MushafOptions).Value;

        state.SummaryInfoList = state.SearchCharacters.AsClearArabicCharacterList().Select(arabicCharcter =>
        {
            var arabicCharacterIndex = arabicCharcter.AsArabicCharacterIndex().Value;

            return new SummaryInfo
            {
                Count = matchRecords.Count(x => x.ArabicLetterIndex == arabicCharacterIndex),
                Name  = arabicCharcter
            };
        }).ToList();

        var results = new List<Occurence>();

        var counts = new List<(string charachter, int count)>();

        foreach (var record in matchRecords)
        {
            var occurence = new Occurence
            {
                VerseId = record.Verse.Id
            };

            if (results.Any(x => x.VerseId == occurence.VerseId))
            {
                continue;
            }

            results.Add(occurence);

            foreach (var charachter in state.SearchCharacters.AsClearArabicCharacterList())
            {
                var propertyName = "Charachter" + (state.SearchCharacters.AsClearArabicCharacterList().ToList().IndexOf(charachter) + 1);

                var property = typeof(Occurence).GetProperty(propertyName);

                var count = matchRecords.Count(m => record.Verse._index == m.Verse._index && m.ToString() == charachter);

                counts.Add((charachter, count));

                Debug.Assert(property != null, nameof(property) + " != null");

                property.SetValue(occurence, count);
            }
        }

        state.ResultRecords = results.ToArray();

        state.CountOfCharacters = matchRecords.Count;

        state.IsBlocked     = false;

        var sb = new StringBuilder();
        foreach (var (charachter, count) in counts)
        {
            sb.AppendLine($"{charachter} : {count}");
        }

        state.SummaryText = sb.ToString();
    }
    #endregion
}