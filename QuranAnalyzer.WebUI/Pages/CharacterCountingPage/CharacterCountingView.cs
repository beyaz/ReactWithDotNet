using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.MainPage;
using ReactWithDotNet;
using ReactWithDotNet.PrimeReact;
using static QuranAnalyzer.WebUI.Extensions;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;



class MushafOptionsView:ReactComponent
{
    public bool MushafOptionsPanelIsVisible { get; set; }

    public CountingOption MushafOption { get; set; }

    public Expression<Func<bool>> Bestaten_7_69 { get; set; }
    
    public override Element render()
    {
        return new Panel
        {
            toggleable = true,
            collapsed = true,
            header     = "Mushaf Ayarları",
            children =
            {
                new HPanel
                {
                    new InputSwitch{ @checked = MushafOption.UseElifCountsSpecifiedByRK},
                    new h5{text               = "Elif sayımları için Tanzil.net'i referans al"}
                },
                new div
                {
                    style = {  display = "flex", flexDirection = "row", alignItems = "center"},
                    children =
                    {
                        new InputSwitch
                        {
                            @checkedBind = Bestaten_7_69
                        },
                        new HSpace(15),
                        new h5{text = "7:69 daki bestaten'i Sad olarak say"}
                    }
                },
                new a{text = "Mushaf ayarları hakkında detaylı bilgi", href =  GetPageLink(PageId.PageIdOfMushafOptionsDetail), }
            }
        };
    }
}

class CharacterCountingView : ReactComponent<CharacterCountingViewModel>
{
    public CharacterCountingView()
    {
        state = new CharacterCountingViewModel();

        StateInitialized += () =>
        {
            if (state.ChapterFilter == null)
            {
                if (Context.TryGetValue(BrowserInformation.UrlParameters).TryGetValue("q", out var value))
                {
                    state.ChapterFilter    = value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                    state.SearchCharacters = value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
                }
            }
            
        };
        
      
    }

     

    void OnCaclculateClicked(string _)
    {
        if (state.IsBlocked == false)
        {
            state.ResultRecords = null;
            state.OperationName = "Hesaplanıyor...";
            state.IsBlocked     = true;
            Context.ClientTask.PushHistory("", $"/index.html?page=CharacterCounting&q={state.ChapterFilter}|{state.SearchCharacters}");
            Context.ClientTask.ComebackWithLastAction(5);
            return;
        }

        var matchRecords = QuranAnalyzerMixin.SearchCharachtersWithCache(state.ChapterFilter, state.SearchCharacters).Value;

        state.SummaryInfoList = state.SearchCharacters.AsClearArabicCharacterList().Select(arabicCharcter =>
        {
            var arabicCharacterIndex = arabicCharcter.AsArabicCharacterIndex().Value;
            
            return new SummaryInfo
            {
                Count = matchRecords.Count(x => x.ArabicCharacterIndex == arabicCharacterIndex),
                Name  = arabicCharcter
            };
        }).ToList();
            
       
        
        
        var results = new List<Occurence>();

        var counts = new List<(string charachter, int count)>();

        foreach (var record in matchRecords)
        {
            var occurence = new Occurence
            {
                VerseNumber = record.Verse._index
            };

            if (results.Any(x=>x.VerseNumber == occurence.VerseNumber))
            {
                continue;
            }

            results.Add(occurence);

           

            foreach (var charachter in state.SearchCharacters.AsClearArabicCharacterList())
            {
                var propertyName = "Charachter" + (state.SearchCharacters.AsClearArabicCharacterList().ToList().IndexOf(charachter) + 1);

                var property     = typeof(Occurence).GetProperty(propertyName);
                
                var count =  matchRecords.Count(m => record.Verse._index == m.Verse._index && m.ToString() == charachter);


                counts.Add((charachter,count));

                Debug.Assert(property != null, nameof(property) + " != null");

                property.SetValue(occurence, count);
            }
        }

        state.ResultRecords = results.ToArray();

        state.CountOfCharacters = matchRecords.Count;

        state.IsBlocked     = false;
        state.OperationName = null;

        

        var sb = new StringBuilder();
        foreach (var (charachter, count) in counts)
        {
            sb.AppendLine($"{charachter} : {count}");
        }

        state.SummaryText = sb.ToString();
    }

    public override Element render()
    {

        var searchPanel = new divWithBorder
        {
            style = { padding = "15px", minWidth = "300px"},
            children =
            {
                new h4("Arama"),
                new VPanel
                {
                    new VPanel
                    {
                        new div {innerText   = "Sure:"},
                        new InputText {valueBind = () => state.ChapterFilter}
                    },

                    new VSpace(10),

                    new VPanel
                    {
                        new div {innerText       = "Aranacak Karakterlerler"},
                        new InputText {valueBind = () => state.SearchCharacters}
                    },

                    new VSpace(20),

                    new Button
                    {
                        label     = "Ara",
                        onClick   = OnCaclculateClicked,
                        className ="p-button-outlined",
                        style     = {alignSelf = "flex-end", flexDirection = "column", paddingLeft = "50px", paddingRight = "50px"}
                    },

                    new VSpace(20),
                    new MushafOptionsView{ MushafOption = state.CountingOption, Bestaten_7_69 = ()=>state.CountingOption.Use_Sad_in_Surah_7_Verse_69_in_word_bestaten}


                }
            }
        };

        if (state.SummaryText.HasNoValue())
        {
            return BlockUI(searchPanel, state.IsBlocked, state.OperationName);
        }

        var summaryContent = new HPanel
        {
            new div {innerText = state.SummaryText},
            new div {innerText = state.CountOfCharacters.ToString(), style = {marginLeft = "5px", marginRight = "5px"}},

           
        };

        if (state.CountOfCharacters % 19 == 0)
        {
            summaryContent.Add(new div {innerText = "("});
            summaryContent.Add(new div {innerText = "19 x " + state.CountOfCharacters / 19, style = {color = "red", marginLeft = "5px", marginRight = "5px" } });
            summaryContent.Add(new div {innerText = ")"});
        }


        var resultColumns = new List<Column>
        {
            new() {field = nameof(Occurence.VerseNumber), header = "Ayet No"}
        };

        
        foreach (var charachter in state.SearchCharacters.AsClearArabicCharacterList())
        {
            var propertyName = "Charachter" + (state.SearchCharacters.AsClearArabicCharacterList().ToList().IndexOf(charachter) + 1);

            resultColumns.Add(new Column {field = propertyName, header = charachter});
            
        }

        var dt = new DataTable
        {
            scrollHeight = "300px",
            scrollable   = true,
            value        = state.ResultRecords,
            
        };

        dt.children.AddRange(resultColumns);

        var matchRecords = QuranAnalyzerMixin.SearchCharachtersWithCache(state.ChapterFilter, state.SearchCharacters).Value;

        var results = new Card
        {
            title  = "Sonuçlar",
            style = { marginTop = "5px"},
            children =
            {
                new TabView
                {
                    activeIndexBind = ()=>state.SelectedTabIndex,
                    children =
                    {
                        new TabPanel
                        {
                            header = "Özet",
                            children =
                            {
                                new CountsSummaryView{ Counts = state.SummaryInfoList}
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
                                applyFontSize(CharachterSearchResultColorizer.ColorizeCharachterSearchResults(matchRecords, state.SearchCharacters.AsClearArabicCharacterList()))
                            }
                        }
                    }
                }
            }
        };




        Element applyFontSize(HtmlElement el)
        {
            if (state.AvailableWidth <500)
            {
                el.style.fontSize = "9px";
                return el;
            }

            el.style.fontSize = "19px";
            
            return el;
        }


        return BlockUI( new VPanel{searchPanel, results}, state.IsBlocked, state.OperationName);
    }
}


public class SummaryInfo
{
    public string  Name { get;  set; }
    public int Count { get; set; }
}

[Serializable]
class CountsSummaryView: ReactComponent
{
    public IReadOnlyList<SummaryInfo> Counts { get; set; } = new List<SummaryInfo>();

    public override Element render()
    {
        static Element toElement(SummaryInfo x)
        {
            return new div
            {
                innerHTML = $"<strong>{x.Count}</strong> adet <strong>{x.Name}</strong> harfi bulundu."
            };
        }
        
        var returnDiv = new div
        {
            new div
            {
                Children = Counts.Select(toElement)
            }
        };

        var total = Counts.Select(x => x.Count).Sum();

        if (total % 19 == 0)
        {
            returnDiv.appendChild(new div
            {
                children =
                {
                    new div {innerHTML = $"Toplam: <strong>{total}</strong> ("},
                    new div {innerText               = "19 x " + total / 19, style = {color = "red", marginLeftRight = "5px"}},
                    new div {innerText               = ")"}
                },
                style = { display = "flex", flexDirection = "row"},
            });
        }
        else
        {
            returnDiv.appendChild(new div
            {
                new div{innerHTML = $"Toplam: <strong>{total}</strong>"}
            });
        }

        return returnDiv;
    }
}