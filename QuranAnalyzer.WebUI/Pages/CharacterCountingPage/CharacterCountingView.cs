using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using QuranAnalyzer.WebUI.Components;
using ReactDotNet;
using ReactDotNet.Html5;
using ReactDotNet.PrimeReact;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;





class CharacterCountingView : ReactComponent<CharacterCountingViewModel>
{
    public override void constructor()
    {
        state = new CharacterCountingViewModel();

        if (Context != null &&  Context.TryGetValue(BrowserInformation.UrlParameters).TryGetValue("q",out var value))
        {
            state.SuraFilter = value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            state.SearchCharacters = value.Split("|".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
        }
    }

    

    void OnCaclculateClicked(string _)
    {
        if (state.IsBlocked == false)
        {
            state.OperationName = "Hesaplanıyor...";
            state.IsBlocked     = true;
            state.ClientTask    = new ClientTaskComebackWithLastAction {Timeout = 5};
            return;
        }

        var matchRecords = QuranAnalyzerMixin.SearchCharachtersWithCache(state.SuraFilter, state.SearchCharacters).Value;

        var results = new List<Occurence>();

        var counts = new List<(string charachter, int count)>();

        foreach (var record in matchRecords)
        {
            var occurence = new Occurence
            {
                AyahNumber = record.verse._index
            };

            if (results.Any(x=>x.AyahNumber == occurence.AyahNumber))
            {
                continue;
            }

            results.Add(occurence);

           

            foreach (var charachter in state.SearchCharacters.AsClearArabicCharacterList())
            {
                var propertyName = "Charachter" + (state.SearchCharacters.AsClearArabicCharacterList().ToList().IndexOf(charachter) + 1);

                var property     = typeof(Occurence).GetProperty(propertyName);
                
                var count =  matchRecords.Count(m => record.verse._index == m.verse._index && m.ToString() == charachter);


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
        var container = new VPanel
        {
            Margin = {Left = 10, Right = 10},
        };

        var searchBar = new divWithBorder
        {
            
            Margin = {Top = 5},
            PaddingAll = 15,
            children =
            {
                new h4{innerText = "Arama"},
                new div
                {   style ={display = "flex", flexDirection = "column"},
                    children=
                    {
                        new div
                        {
                            style ={display = "flex", flexDirection = "column"},
                            children =
                            {
                                new div {innerText   = "Sure:"},
                                new InputText {value = Mixin.Bind( () => state.SuraFilter)}
                            }
                        },

                        new Space {Height = 10},

                        new VPanel
                        {
                            new div {innerText   = "Aranacak Karakterlerler"},
                            new InputText {value = Mixin.Bind(() => state.SearchCharacters)}
                        },

                        new Space {Height = 20},

                        new Button
                        {
                            label   = "Ara",
                            onClick = OnCaclculateClicked,
                            className ="p-button-outlined",
                            style     = {alignSelf = "flex-end", flexDirection = "column", paddingLeft = "50px", paddingRight = "50px"},
                            
                        },
                    }
                }
            }
        };

        if (state.SummaryText.HasNoValue())
        {
            return Extensions.BlockUI(container.appendChild(searchBar), state.IsBlocked, state.OperationName);
        }

        var summaryContent = new HPanel
        {
            new div {innerText = state.SummaryText},
            new div {innerText = state.CountOfCharacters.ToString(), style = {marginLeft = px(5), marginRight = px(5)}},

           
        };

        if (state.CountOfCharacters % 19 == 0)
        {
            summaryContent.Add(new div {innerText = "("});
            summaryContent.Add(new div {innerText = "19 x " + state.CountOfCharacters / 19, style = {color = "red", marginLeft = px(5), marginRight = px(5)}});
            summaryContent.Add(new div {innerText = ")"});
        }


        var resultColumns = new List<Column>
        {
            new() {field = nameof(Occurence.AyahNumber), header = "Ayet No"}
        };

        
        foreach (var charachter in state.SearchCharacters.AsClearArabicCharacterList())
        {
            var propertyName = "Charachter" + (state.SearchCharacters.AsClearArabicCharacterList().ToList().IndexOf(charachter) + 1);

            resultColumns.Add(new Column {field = propertyName, header = charachter});
            
        }

        var dt = new DataTable
        {
            scrollHeight = px(300),
            scrollable   = true,
            value        = state.ResultRecords,
            
        };

        dt.children.AddRange(resultColumns);

        var matchRecords = QuranAnalyzerMixin.SearchCharachtersWithCache(state.SuraFilter, state.SearchCharacters).Value;

        var results = new Card
        {
            title  = "Sonuçlar",
            Margin = {Top = 5},
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
                                new CountsSummaryView{ Counts = new []{new SummaryInfo{Name = "A", Count = 55}}}
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
                                CharachterSearchResultColorizer.ColorizeCharachterSearchResults(matchRecords, state.SearchCharacters.AsClearArabicCharacterList()) + getFontSize()
                            }
                        }
                    }
                }
            }
        };




        ElementModifier getFontSize()
        {
            if (state.AvailableWidth <500)
            {
                return fontSize(9);
            }

            return fontSize(19);
        }


        return Extensions.BlockUI(container.appendChild(searchBar).appendChild(results), state.IsBlocked, state.OperationName);
    }
}


record SummaryInfo
{
    public string  Name { get; set; }
    public int Count { get; set; }
}

[Serializable]
class CountsSummaryView: ReactComponent
{
    public IReadOnlyList<SummaryInfo> Counts { get; set; }

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