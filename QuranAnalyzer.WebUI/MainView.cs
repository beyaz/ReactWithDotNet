using ReactDotNet;
using ReactDotNet.PrimeReact;
using System;
using static ReactDotNet.Mixin;
using static QuranAnalyzer.Mixin;
using System.Threading;
using System.Linq;
using System.Text.Json.Serialization;

namespace QuranAnalyzer.WebUI
{
    class SvgHamburgerIcon : ReactComponent
    {
        public bool HamburgerMenuIsOpen { get; set; }

        public override Element render()
        {
            if (HamburgerMenuIsOpen)
            {
                var svg = new svg { viewBox = "0 0 15 15", style = { height = "20px", margin = "14px" }, onClick = onClick };

                var path = new path { d = "M15 8a.5.5 0 0 0-.5-.5H2.707l3.147-3.146a.5.5 0 1 0-.708-.708l-4 4a.5.5 0 0 0 0 .708l4 4a.5.5 0 0 0 .708-.708L2.707 8.5H14.5A.5.5 0 0 0 15 8z", fill = "blue" };

                return svg.appendChild(path);
            }

            {
                var svg = new svg { viewBox = "0 0 24 24", style = { height = "24px", margin = "12px" }, onClick = onClick };

                var path = new path { d = "M3 18h18v-2H3v2zm0-5h18v-2H3v2zm0-7v2h18V6H3z", fill = "black" };

                return svg.appendChild(path);
            }
        }
    }
   

    static class MixinForUI
    {
        public static Element BlockUI(Element content, bool isBlocked, string operationMessage)
        {
            return new BlockUI
            {
                blocked = isBlocked,
                template = new div
                {
                    new i { className = "pi pi-spin pi-spinner" },
                    new div { Margin  = { Left = 5 }, style = { color = "White" }, text = operationMessage }
                }.MakeCenter(),

                children =
                {
                    content
                }
            };
        }
    }

    class Theme
    {
        public readonly string MainPaperBackgroundColor = "white";
    }

    [Serializable]
    public class MainViewModel
    {
        public string SelectedFact { get; set; }
        public string SummaryText { get;  set; }

        public ClientTask ClientTask { get; set; }
        public string OperationName { get; set; }
        public bool IsBlocked { get; set; }

        public bool HamburgerMenuIsOpen { get; set; }

        public double MainDivScrollY { get; set; }
        public double AvailableWidth { get; set; }
        public double AvailableHeight { get; set; }

        public string SearchPartOfUrl { get; set; }

        public FactViewModel FactViewModel { get; set; }

    }

    class FactMiniViewModel
    {
        public ClientTask ClientTask { get; set; }
        public Fact Fact { get; set; }
    }
    class FactMiniView:ReactComponent<FactMiniViewModel>
    {
        public FactMiniView()
        {
            state = new FactMiniViewModel();
        }


        

        public string title { get; set; }

        void OnClicked(string name)
        {
            state.ClientTask = new ClientTask
            {
               DispatchEvent = true,
               DispatchEventName = "OnFactClicked",
               DispatchEventParameters = new object[]{ name }
            };
        }

        public override Element render()
        {
            return new div
            {
                style =
                {
                    borderRadius = "5px",
                    width        = px(150),
                    height       = px(200) ,
                    //boxShadow  ="0 4px 8px 0 rgba(0,0,0,0.2)",
                    margin         = px(20),
                    display        = Display.flex,
                    justifyContent = JustifyContent.center,
                    alignItems     = AlignItems.center,
                    flexDirection  = FlexDirection.column,
                    textAlign      = TextAlign.center,
                    fontFamily     = "Verdana,sans-serif"
                },
                onClick = ()=>OnClicked(state.Fact.Name),
                children =
                {
                    new div
                    {

                        style = { padding = "1px",  },
                        children =
                        {
                            new div { style =   {color ="#08090a", fontSize = "17px", fontWeight = "600" }, text = title },
                            new div
                            {
                                style =
                                {
                                    wordBreak = WordBreak.break_all,
                                    margin    = "15px",
                                    fontSize  = px(13),
                                    color     ="#546285"
                                },
                                text = "Kısa açıklamaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa1gtgtgrtrtğ", Margin ={ Top =5} }
                        }
                    }

                }

            }.HasBorder();
        }
    }

    class MainView : ReactComponent<MainViewModel>
    {
        Theme theme = new Theme();

        public MainView()
        {
            state = new MainViewModel
            {
                ClientTask = new ClientTask
                {
                    ListenEvent     = true,
                    ListenEventName = "OnFactClicked",
                    ListenEventRouteTo = nameof(OnFactClicked)
                }
            };
        }

        void OnFirstLoaded()
        {
            if (state.SearchPartOfUrl != null && state.SearchPartOfUrl.Length >0  )
            {
                state.SelectedFact = System.Web.HttpUtility.UrlDecode(state.SearchPartOfUrl.Split("=").Last());
            }
        }

        void OnFactClicked(string selectedFactName)
        {
            state.SelectedFact = selectedFactName;
            state.ClientTask = new ClientTask
            {
                HistoryPushState      = true,
                HistoryPushStateTitle = selectedFactName,
                HistoryPushStateUrl   = "/index.html?fact="+selectedFactName
            };
        }

        void OnMainContentDivScrollChanged()
        {

        }
        

        public override Element render()
        {
            var facts = Fact.GetFacts().ToList();

            var searchText = new span("p-input-icon-right")
                                + new i("pi pi-search")
                                + new InputText { placeholder = "ara" };








            
            

            var hamburgerIcon = new SvgHamburgerIcon { HamburgerMenuIsOpen = state.HamburgerMenuIsOpen , onClick = ()=>state.HamburgerMenuIsOpen = !state.HamburgerMenuIsOpen};

            var topNav = new nav
                         {
                             hamburgerIcon,
                             new div
                             {
                                 new div {id = "title", text = "19 Sistemi Hakkında"}
                             }
                         }

                         + new Style
                         {
                             display        = Display.flex,
                             justifyContent = JustifyContent.flex_start,
                             alignItems     = AlignItems.center
                         };

            

            var main = new div(facts.Select(x=>new FactMiniView {state = new FactMiniViewModel{ Fact = x} ,title = x.Name}))
                           + new Style
                           {
                               background     = theme.MainPaperBackgroundColor,
                               display        = Display.flex,
                               flexWrap       = FlexWrap.wrap,
                               justifyContent = JustifyContent.center
                           };





            if (state.SelectedFact != null)
            {
                var fact = facts.FirstOrDefault(x => x.Name == state.SelectedFact);
                if (fact!= null)
                {
                    state.FactViewModel ??= new FactViewModel
                    {
                        SuraFilter       = fact.SearchScript,
                        SearchCharacters = fact.SearchCharacters
                    };

                    main = new FactView{ state = state.FactViewModel};
                }
            }

            



           


            Element createMenuItem(string label)
            {
                return new div { text = label } + new Style { fontSize = px(17) , marginTop = px(20) };
            }

            var menu = new div 
            {
                createMenuItem("Anasayfa"), 
                createMenuItem("Başlangıç Harfleri"), 
                createMenuItem("Soru - Cevap"), 
                createMenuItem("İletişim"),
            } 
            + 
            new Style 
            { 
                position      = Position.@fixed, 
                height        = "100%",
                width         = "70%",
                top           = px(50),
                background    = "white",
                boxShadow = "5px 0 5px -5px rgb(0 0 0 / 28%)",
                zIndex        = "1",
                display       = state.HamburgerMenuIsOpen ? Display.flex : Display.none,
                transition =  "visibility 0s linear 500ms, opacity 500ms",
                flexDirection = FlexDirection.column,
                alignItems    = AlignItems.center,
                fontSize      = px(18),
                
            };
            
            

            return CreatePage(topNav, main, menu);
        }

        Element CreatePage(Element topContent, Element mainContent, Element menu)
        {
            var top = new div { topContent } + new Style
            { 
                position       = Position.@fixed,
                top            = px(0),
                left = px(0),

                width = "100%",
                height         = px(50),
                zIndex = "1",
                //background = "rgb(248, 249, 249)",
                borderBottom = "1px solid #dadce0"
            };

            if (state.MainDivScrollY > 0 )
            {
                top = new div { topContent } + new Style
                { 
                    position = Position.@fixed,
                    top      = px(0),
                    left     = px(0),

                    width      = "100%",
                    height     = px(50),
                    zIndex     = "1",
                    //background = "rgb(248, 249, 249)",
                    boxShadow  = "0 1px 2px hsla(0,0%,0%,0.05),0 1px 4px hsla(0,0%,0%,0.05),0 2px 8px hsla(0,0%,0%,0.05)"
                };
            }

            mainContent.key = "tt";

            var main   = new div { id="main", children = { mainContent }} + new Style 
            { 
                position       = Position.@fixed, 
                top            = px(0),
                left = px(0),
                marginTop= px(50),
                marginBottom= px(27),
                
                width = "100%",
                height = "calc(100% - 65px)",
                overflowY = Overflow.auto,
                
            };

            return new div {  top, menu,  main,  } + new Style { height = "100vh" , width = "100%" };
        }
        


    }
}

// https://codepen.io/Zeeslag/pen/MWpLoKX