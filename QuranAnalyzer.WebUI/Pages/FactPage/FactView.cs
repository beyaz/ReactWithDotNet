using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using QuranAnalyzer.WebUI.Components;
using ReactDotNet;
using ReactDotNet.PrimeReact;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer.WebUI.Pages.FactPage;

static class ResourceAccess
{
    public static readonly IReadOnlyList<FactModel> Facts = ResourceHelper.Read<FactModel[]>("Pages.FactPage.Facts.json");
}

[Serializable]
public class FactViewModel
{
    public string SelectedFact { get; set; }
    public string SummaryText { get; set; }

    public ClientTask ClientTask { get; set; }
    public string OperationName { get; set; }
    public bool IsBlocked { get; set; }

    public string SuraFilter { get; set; }

    public string SearchCharacters { get; set; }

    public int CountOfCharacters { get; set; }
    

    public int SelectedTabIndex { get; set; }

    [NonSerialized] 
    public Occurence[] ResultRecords;

    public double AvailableWidth { get; set; }



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

    public string AyahNumber { get; set; }



}



class FactView : ReactComponent<FactViewModel>
{
    public override void constructor()
    {
        state = new FactViewModel();
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

                Debug.Assert(property != null, nameof(property) + " != null");

                property.SetValue(occurence, count);
            }
        }

        state.ResultRecords = results.ToArray();

        state.CountOfCharacters = matchRecords.Count;

        state.IsBlocked     = false;
        state.OperationName = null;

        state.SummaryText = $"'{state.SuraFilter}' suresinde '{state.SearchCharacters[0]}' harfi geçiş sayısı :";
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
                new h4{text = "Arama"},
                new VPanel
                {
                    new VPanel
                    {
                        new div {text            = "Sure:"},
                        new InputText {value = Mixin.Bind( () => state.SuraFilter)}
                    },

                    new Space {Height = 10},

                    new VPanel
                    {
                        new div {text            = "Aranan Karakterler"},
                        new InputText {value = Mixin.Bind(() => state.SearchCharacters)}
                    },

                    new Space {Height = 20},

                    new Button(className("p-button-outlined"), alignSelf(AlignItems.flex_end), paddingLeft(50), paddingRight(50)) {label = "Ara", onClick = OnCaclculateClicked},
                }
            }
        };

        if (state.SummaryText.HasNoValue())
        {
            return Extensions.BlockUI(container.appendChild(searchBar), state.IsBlocked, state.OperationName);
        }

        var summaryContent = new HPanel
        {
            new div {text = state.SummaryText},
            new div {text = state.CountOfCharacters.ToString(), style = {marginLeft = px(5), marginRight = px(5)}},

           
        };

        if (state.CountOfCharacters % 19 == 0)
        {
            summaryContent.Add(new div {text = "("});
            summaryContent.Add(new div {text = "19 x " + state.CountOfCharacters / 19, style = {color = "red", marginLeft = px(5), marginRight = px(5)}});
            summaryContent.Add(new div {text = ")"});
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
                                new CountsSummaryView{ Counts = new List<(string name, int count)>{("A",5),("B",4), ("c",5)}} | margin(22)
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

[Serializable]
class CountsSummaryView: ReactComponent
{

    public string AAA { get; set; } = "ABC";

    public IReadOnlyList<(string name, int count)> Counts { get; set; } = new[] {("a", 5)};

    public override Element render()
    {
        var returnDiv = new div
        {
            new div(Counts.Select(x => new div($"{x.count} adet {x.name} harfi bulundu.")))
        };

        var total = Counts.Select(x => x.count).Sum();

        if (total % 19 == 0)
        {
            returnDiv.appendChild(new div
            {
                new div {text = "Toplam: ("},
                new div {text = "19 x " + total / 19, style = {color = "red", marginLeft = px(5), marginRight = px(5)}},
                new div {text = ")"}
            });
        }
        else
        {
            returnDiv.appendChild(new div
            {
                new div($"Toplam: {total}")
            });
        }

        return returnDiv;

    }
}


[Serializable]
class DesignerDeneme : ReactComponent
{

    public string AAA { get; set; }

    public string BBB { get; set; }


    public override Element render()
    {
        var returnDiv = new div("Empty")
        {
           
        };

        if (AAA != null)
        {
            returnDiv.appendChild(new div(AAA));
        }

        if (BBB != null)
        {
            returnDiv.appendChild(new div(BBB));
        }

        return returnDiv;

    }
}