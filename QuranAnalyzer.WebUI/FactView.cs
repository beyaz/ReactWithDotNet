using System;
using System.Threading;
using ReactDotNet;
using ReactDotNet.PrimeReact;

namespace QuranAnalyzer.WebUI;


    [Serializable]
    public class FactViewModel
    {
        public string SelectedFact { get; set; }
        public string SummaryText { get;  set; }

        public ClientTask ClientTask { get; set; }
        public string OperationName { get; set; }
        public bool IsBlocked { get; set; }

        public string SuraFilter { get; set; }

        public string SearchCharacters { get; set; }
    }

    class FactView : ReactComponent<FactViewModel>
    {
        public FactView()
        {
            state = new FactViewModel();
        }

        

        void OnSelectClicked()
        {
            if (state.IsBlocked == false)
            {
                state.OperationName = "Hesaplanıyor...";
                state.IsBlocked     = true;
                state.ClientTask    = new ClientTask { ComebackWithLastAction = true };
                return;
            }
            

            var number = QuranAnalyzer.Mixin.GetCountOfCharacter(state.SearchCharacters, new[] {42});

            state.IsBlocked = false;

            state.SummaryText = number+"---42. suredeki Kaf harfi toplam geçiş adeti <span>19</span> x 6";
        }

        public override Element render()
        {
            var searchBar = new HPanel
            {
                new div
                {
                    new div{text            = "Sure:"},
                    new InputText{valueBind = ()=>state.SuraFilter}
                },
                new div
                {
                    new div{text        = "Aranan Karakterler"},
                    new InputText{valueBind = ()=>state.SearchCharacters}
                },
                    
                new Button("p-button-outlined") { text ="Ara", onClick = OnSelectClicked },
            } + new Style{ margin = ReactDotNet.Mixin.px(10)};

            var results = new TabView
            {
                new TabPanel
                {
                    header = "Özet", 
                    children = 
                    {
                        new div(){text = state.SummaryText},
                        new HPanel
                        { 
                            new div { text = "42. suredeki Kaf harfi geçiş sayısı "},
                            new pre { text = " '19 ", style = {fontFamily = "'Source Sans Pro', 'Helvetica Neue', Arial, sans-serif", color = "red" } },
                            new div { text = "x 6' dır."},
                        }
                    } 
                },
                new TabPanel
                {
                    header = "Detaylı Tablo", 
                    children = 
                    {
                        new div(){text = "detaylı tablo"}
                    } 
                }
            };

            return MixinForUI.BlockUI(new div { searchBar, results } + new Style { marginTop = ReactDotNet.Mixin.px(55), marginBottom = ReactDotNet.Mixin.px(55), padding = "25px"}, state.IsBlocked, state.OperationName);
        }
    }
