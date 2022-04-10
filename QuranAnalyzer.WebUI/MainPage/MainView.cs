using System;
using System.Linq;
using System.Web;
using ReactDotNet;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer.WebUI;

[Serializable]
public class MainViewModel
{
    public string Page { get; set; }
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

        state.SelectedFact = getValueByName("fact");
        state.Page         = getValueByName("page");

        string getValueByName(string name) => HttpUtility.ParseQueryString(state.SearchPartOfUrl).Get(name);
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
    
    void hamburgerMenuClicked() => state.HamburgerMenuIsOpen = !state.HamburgerMenuIsOpen;

    public override Element render()
    {
        var commonData     = ResourceAccess.MainPage;
        var mainMenuModels = commonData.MenuItems;
        var facts          = ResourceAccess.Facts;

        return new MainPage
        {
            topContent     = buildTopNav(),
            mainContent    = buildMainContent(),
            menu           = buildLeftMenu(),
            mainDivScrollY = state.MainDivScrollY
        };

        Element buildMainContent()
        {
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

                    return new FactView { state = state.FactViewModel };
                }
            }

            if (state.Page is not null)
            {
                if (state.Page is nameof(ResourceAccess.QuestionAnswerPage))
                {
                    return new div { text = ResourceAccess.QuestionAnswerPage.Summary };
                }
                if (state.Page is "Contact")
                {
                    return new ContactPage.View { model = ResourceHelper.Read<ContactPage.Model>("ContactPage.Data.yaml") };
                }

                else
                {
                    return new div { text = ResourceAccess.MainPage.Content };
                }
            }

            return new div(facts.Select(x => new FactMiniView { state = new FactMiniViewModel { Fact = x } }))
                   + new Style
                   {
                       background     = "white",
                       display        = Display.flex,
                       flexWrap       = FlexWrap.wrap,
                       justifyContent = JustifyContent.center
                   };

        }


        Element buildTopNav()
        {
            var hamburgerIcon = new SvgHamburgerIcon { HamburgerMenuIsOpen = state.HamburgerMenuIsOpen, onClick = hamburgerMenuClicked };

            return new nav
                   {
                       hamburgerIcon,
                       new div
                       {
                           new div {id = "title", text = commonData.Title}
                       }
                   }
                   + new Style
                   {
                       display        = Display.flex,
                       justifyContent = JustifyContent.flex_start,
                       alignItems     = AlignItems.center
                   };
        }

        Element buildLeftMenu()
        {
            return new div(mainMenuModels.Select(ToMenuItem))
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

            Element ToMenuItem(MainMenuModel m)
            {
                return new a
                       {
                           text    = m.Text,
                           href    = "/index.html?page=" + m.Id,
                           onClick = () => OnMainMenuItemClicked(m.Id)
                       }
                       +
                       new Style
                       {
                           fontSize  = px(17),
                           marginTop = px(50)
                       };
            }
        }

       

        
         
    }
    
}

