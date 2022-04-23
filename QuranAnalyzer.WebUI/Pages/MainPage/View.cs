using System;
using System.Linq;
using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.FactPage;
using ReactDotNet;
using static ReactDotNet.Mixin;

namespace QuranAnalyzer.WebUI.Pages.MainPage;


[Serializable]
public sealed class MainMenuModel
{
    public string Text { get; set; }
    public string Id { get; set; }
}

[Serializable]
public sealed class MainPageModel
{
    public string Content { get; set; }

    public string Title { get; set; }
    public MainMenuModel[] MenuItems { get; set; }
}



[Serializable]
public class MainViewModel
{
    public string PageId { get; set; }
    public string SelectedFact { get; set; }
    public string SummaryText { get; set; }

    public ClientTask ClientTask { get; set; }
    public string OperationName { get; set; }
    public bool IsBlocked { get; set; }

    public bool HamburgerMenuIsOpen { get; set; }

    public double MainDivScrollY { get; set; }
    

    public string LastClickedMenuId { get; set; }

    public FactViewModel FactViewModel { get; set; }
}

class View : ReactComponent<MainViewModel>
{

    static readonly MainPageModel ConstantData = ResourceHelper.ReadPageData<MainPageModel>(nameof(MainPage));

    public override void constructor()
    {
        state = new MainViewModel();

        if (Context.TryGetValue(BrowserInformation.UrlParameters).TryGetValue("fact", out var selectedFact))
        {
            state.SelectedFact = selectedFact;
        }

        if (Context.TryGetValue(BrowserInformation.UrlParameters).TryGetValue("page", out var pageId))
        {
            state.PageId = pageId;

            if (pageId == InitialLetters.View.PageId)
            {
                state.ClientTask = new ClientTaskListenComponentEvent
                {
                    EventName     = ReactComponentEvents.componentDidMount.ToString(),
                    RouteToMethod = nameof(OnFirstLoaded),
                    After = new ClientTaskCallJsFunction
                    {
                        JsFunctionPath = "RegisterScrollEvents"
                    }
                };
            }
        }

    }

    void OnFirstLoaded()
    {
        state.ClientTask = new ClientTaskListenEvent
        {
            EventName     = nameof(OnFactClicked),
            RouteToMethod = nameof(OnFactClicked)
        };
    }
    
    void OnFactClicked(string selectedFactName)
    {
        state.SelectedFact = selectedFactName;
        state.ClientTask = new ClientTaskPushHistory
        {
            Title = selectedFactName,
            Url   = "/index.html?fact=" + selectedFactName
        };
    }

    public void OnMainContentDivScrollChanged(double mainDivScrollY)
    {
        state.MainDivScrollY = mainDivScrollY;
    }

    void OnMainMenuItemClicked(string menuId)
    {
        state.LastClickedMenuId = menuId;

        state.ClientTask = new ClientTaskDispatchEvent
        {
            EventName       = nameof(OnMainMenuItemClicked),
            EventArguments = new object[] {menuId}
        };
    }
    
    void hamburgerMenuClicked() => state.HamburgerMenuIsOpen = !state.HamburgerMenuIsOpen;

    public override Element render()
    {
        var mainMenuModels = ConstantData.MenuItems;
        var facts          = ResourceAccess.Facts;

        return new Components.MainPage
        {
            topContent     = buildTopNav(),
            mainContent    = buildMainContent(),
            menu           = buildLeftMenu(),
            mainDivScrollY = state.MainDivScrollY
        };

        Element buildMainContent()
        {
            if (state.SelectedFact is not null)
            {
                var fact = facts.FirstOrDefault(x => x.Name == state.SelectedFact);
                if (fact is not null)
                {
                    state.FactViewModel ??= new FactViewModel
                    {
                        SuraFilter       = fact.SearchScript,
                        SearchCharacters = fact.SearchCharacters,
                        AvailableWidth = Context.TryGetValue(BrowserInformation.AvailableWidth)
                    };

                    return new FactView { state = state.FactViewModel };
                }
            }

            if (state.PageId is not null)
            {
                var pages = new Element[]
                {
                    new QuestionAnswerPage.View(),
                    new ContactPage.View(),
                    new InitialLetters.View()
                };

                var page = pages.FirstOrDefault(x => x.id == state.PageId);
                if (page is not null)
                {
                    return page;
                }
            }

            return new div
            {
                new div{text = ConstantData.Title},
                new div{text = ConstantData.Content}
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
                           new div {id = "title", text = ConstantData.Title}
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

