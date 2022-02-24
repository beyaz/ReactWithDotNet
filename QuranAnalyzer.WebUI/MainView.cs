using System;
using System.Linq;
using System.Web;
using ReactDotNet;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer.WebUI;

[Serializable]
public class MainViewModel
{
    public string SelectedFact { get; set; }
    public string SummaryText { get; set; }

    public ClientTask ClientTask { get; set; }
    public string OperationName { get; set; }
    public bool IsBlocked { get; set; }

    public bool HamburgerMenuIsOpen { get; set; }

    public double MainDivScrollY { get; set; }
    public double AvailableWidth { get; set; }
    public double AvailableHeight { get; set; }

    public string SearchPartOfUrl { get; set; }

    public string LastClickedMenuId { get; set; }

    public FactViewModel FactViewModel { get; set; }
}

class MainView : ReactComponent<MainViewModel>
{
    readonly Theme theme = new();

    public MainView()
    {
        state = new MainViewModel
        {
            ClientTask = new ClientTask
            {
                ListenEvent        = true,
                ListenEventName    = nameof(OnFactClicked),
                ListenEventRouteTo = nameof(OnFactClicked)
            }
        };
    }

    public void OnFirstLoaded()
    {
        if (state.SearchPartOfUrl != null && state.SearchPartOfUrl.Length > 0)
        {
            state.SelectedFact = HttpUtility.UrlDecode(state.SearchPartOfUrl.Split("=").Last());
        }
    }

    void OnFactClicked(string selectedFactName)
    {
        state.SelectedFact = selectedFactName;
        state.ClientTask = new ClientTask
        {
            HistoryPushState      = true,
            HistoryPushStateTitle = selectedFactName,
            HistoryPushStateUrl   = "/index.html?fact=" + selectedFactName
        };
    }

    public void OnMainContentDivScrollChanged()
    {
    }

    void OnMainMenuItemClicked(string menuId)
    {
        state.LastClickedMenuId = menuId;

        state.ClientTask = new ClientTask
        {
            DispatchEvent           = true,
            DispatchEventName       = nameof(OnMainMenuItemClicked),
            DispatchEventParameters = new object[] {menuId}
        };
    }

    public override Element render()
    {
        var commonData     = ResourceAccess.CommonData;
        var mainMenuModels = commonData.MainMenuItems;
        var facts          = ResourceAccess.Facts;

        var hamburgerIcon = new SvgHamburgerIcon {HamburgerMenuIsOpen = state.HamburgerMenuIsOpen, onClick = () => state.HamburgerMenuIsOpen = !state.HamburgerMenuIsOpen};

        var topNav = new nav
                     {
                         hamburgerIcon,
                         new div
                         {
                             new div {id = "title", text = commonData.MainTitle}
                         }
                     }
                     + new Style
                     {
                         display        = Display.flex,
                         justifyContent = JustifyContent.flex_start,
                         alignItems     = AlignItems.center
                     };

        var main = new div(facts.Select(x => new FactMiniView {state = new FactMiniViewModel {Fact = x}}))
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
            if (fact != null)
            {
                state.FactViewModel ??= new FactViewModel
                {
                    SuraFilter       = fact.SearchScript,
                    SearchCharacters = fact.SearchCharacters
                };

                main = new FactView {state = state.FactViewModel};
            }
        }

        Element ToMenuItem(MainMenuModel m)
        {
            return new div
                   {
                       text = m.Text,
                       onClick = ()=>OnMainMenuItemClicked(m.Id)
                   }
                   +
                   new Style
                   {
                       fontSize  = px(17),
                       marginTop = px(50)
                   };
        }

        var menu = new div(mainMenuModels.Select(ToMenuItem))
                   +
                   new Style
                   {
                       position      = Position.@fixed,
                       height        = "100%",
                       width         = "70%",
                       top           = px(50),
                       background    = "white",
                       boxShadow     = "5px 0 5px -5px rgb(0 0 0 / 28%)",
                       zIndex        = "1",
                       display       = state.HamburgerMenuIsOpen ? Display.flex : Display.none,
                       transition    = "visibility 0s linear 1000ms, opacity 500ms",
                       flexDirection = FlexDirection.column,
                       alignItems    = AlignItems.center,
                       fontSize      = px(18),
                   };

        return CreatePage(topNav, main, menu);
    }

    Element CreatePage(Element topContent, Element mainContent, Element menu)
    {
        var top = new div {topContent} + new Style
        {
            position = Position.@fixed,
            top      = px(0),
            left     = px(0),

            width  = "100%",
            height = px(50),
            zIndex = "1",
            borderBottom = "1px solid #dadce0"
        };

        if (state.MainDivScrollY > 0)
        {
            top += new Style
            {
                borderBottom = "",
                boxShadow    = "0 1px 2px hsla(0,0%,0%,0.05),0 1px 4px hsla(0,0%,0%,0.05),0 2px 8px hsla(0,0%,0%,0.05)"
            };
        }


        var main = new div {id = "main", children = {mainContent}} + new Style
        {
            position     = Position.@fixed,
            top          = px(0),
            left         = px(0),
            marginTop    = px(50),
            marginBottom = px(27),

            width     = "100%",
            height    = "calc(100% - 65px)",
            overflowY = Overflow.auto,
        };

        return new div {top, menu, main,} + new Style {height = "100vh", width = "100%"};
    }
}

// https://codepen.io/Zeeslag/pen/MWpLoKX