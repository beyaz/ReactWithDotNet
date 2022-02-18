using System;
using System.Text.Json.Serialization;
using ReactDotNet;
using ReactDotNet.PrimeReact;
using static QuranAnalyzer.WebUI.MixinForUI;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer.WebUI;

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

    //[JsonPropertyName("$stateId")]
    //public int? StateId { get; set; } = 4;

    public int SelectedTabIndex { get; set; }
}

[Serializable]
public sealed class Occurence
{
    public string Charachter { get; set; }

    public string ABc { get; set; }
}

class FactView : ReactComponent<FactViewModel>
{
    public FactView()
    {
        state = new FactViewModel();
    }

    public void constructor()
    {
        state = new FactViewModel();
    }

    void OnSelectClicked()
    {
        if (state.IsBlocked == false)
        {
            state.OperationName = "Hesaplanıyor...";
            state.IsBlocked     = true;
            state.ClientTask    = new ClientTask {ComebackWithLastAction = true};
            return;
        }

        state.CountOfCharacters = Mixin.GetCountOfCharacter(state.SearchCharacters, new[] {42});

        state.IsBlocked     = false;
        state.OperationName = null;

        state.SummaryText = "42. suredeki " + state.SearchCharacters[0] + " harfi geçiş sayısı :";
    }

    public override Element render()
    {
        var container = new div {Margin = {Left = 10, Right = 10}};

        var searchBar = new Card
        {
            title  = "Arama",
            Margin = {Top = 5},
            children =
            {
                new VPanel
                {
                    new VPanel
                    {
                        new div {text            = "Sure:"},
                        new InputText {valueBind = () => state.SuraFilter}
                    },

                    new Space {Height = 10},

                    new VPanel
                    {
                        new div {text            = "Aranan Karakterler"},
                        new InputText {valueBind = () => state.SearchCharacters}
                    },

                    new Space {Height = 10},

                    new Button("p-button-outlined") {label = "Ara", onClick = OnSelectClicked},
                }
            }
        };

        if (state.SummaryText.HasNoValue())
        {
            return BlockUI(container + searchBar, state.IsBlocked, state.OperationName);
        }

        var summaryContent = new HPanel
        {
            new div {text = state.SummaryText},
            new div {text = state.CountOfCharacters.ToString(), style = {marginLeft = px(5), marginRight = px(5)}}
        };

        if (state.CountOfCharacters % 19 == 0)
        {
            summaryContent.Add(new div {text = "("});
            summaryContent.Add(new div {text = "19 x " + state.CountOfCharacters / 19, style = {color = "red", marginLeft = px(5), marginRight = px(5)}});
            summaryContent.Add(new div {text = ")"});
        }

        var results = new Card
        {
            title  = "Sonuçlar",
            Margin = {Top = 5},
            children =
            {
                new TabView
                {
                    onTabChange = e=>state.SelectedTabIndex = e.index,
                    activeIndex = state.SelectedTabIndex,
                    children =
                    {
                        new TabPanel
                        {
                            header = "Özet",
                            children =
                            {
                                summaryContent
                            }
                        },
                        new TabPanel
                        {
                            header = "Detaylı Tablo",
                            children =
                            {
                                new DataTable
                                {
                                    scrollHeight = px(300),
                                    scrollable   = true,
                                    value = new[]
                                    {
                                        new Occurence {ABc = "A", Charachter  = "C"},
                                        new Occurence {ABc = "A2", Charachter = "C3"}
                                    },
                                    children =
                                    {
                                        new Column {field = nameof(Occurence.ABc), header        = "Abc"},
                                        new Column {field = nameof(Occurence.Charachter), header = "Abc4"}
                                    }
                                }
                            }
                        }
                    }
                }
            }
        };

        return BlockUI(container + searchBar + results, state.IsBlocked, state.OperationName);
    }
}