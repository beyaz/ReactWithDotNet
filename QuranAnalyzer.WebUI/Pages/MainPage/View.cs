using System;
using System.Linq;
using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.CharacterCountingPage;
using ReactDotNet;

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

    public CharacterCountingViewModel CharacterCountingViewModel { get; set; }
}

class View : ReactComponent<MainViewModel>
{

    static readonly MainPageModel ConstantData = ResourceHelper.ReadPageData<MainPageModel>(nameof(MainPage));

    public View()
    {
        state = new MainViewModel();
    }

    public void ComponentDidMount()
    {
        if (Context.TryGetValue(BrowserInformation.UrlParameters).TryGetValue("page", out var pageId))
        {
            state.PageId = pageId;

            state.ClientTask = new ClientTaskListenEvent
            {
                EventName     = "MainContentDivScrollChanged",
                RouteToMethod = nameof(OnMainContentDivScrollChanged),
                After = new ClientTaskCallJsFunction
                {
                    JsFunctionPath = "RegisterScrollEvents"
                }
            };
        }
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
    
    void hamburgerMenuClicked(string _) => state.HamburgerMenuIsOpen = !state.HamburgerMenuIsOpen;

    public override Element render()
    {
        var mainMenuModels = ConstantData.MenuItems;

        return new Components.MainPage
        {
            topContent     = buildTopNav(),
            mainContent    = buildMainContent(),
            menu           = buildLeftMenu(),
            mainDivScrollY = state.MainDivScrollY
        };

        Element buildMainContent()
        {
            if (state.PageId == "QuestionAnswerPage")
            {
                return new QuestionAnswerPage.View();
            }

            if (state.PageId == "ContactPage")
            {
                return new ContactPage.View();
            }

            if (state.PageId == "InitialLetters")
            {
                return new InitialLetters.View();
            }

            if (state.PageId == "CharacterCounting")
            {
                return new CharacterCountingView();
            }

            return new MainPageContent();
        }


        Element buildTopNav()
        {
            return new nav
            {
                children =
                {
                    new SvgHamburgerIcon { HamburgerMenuIsOpen = state.HamburgerMenuIsOpen, onClick = hamburgerMenuClicked },
                    new div
                    {
                        new SiteTitle(ConstantData.Title)
                    }
                },
                style =
                {
                    display        = "flex",
                    justifyContent = "flex-start",
                    alignItems     = "center"
                }
            };
        }

        Element buildLeftMenu()
        {
            return new div(mainMenuModels.Select(toMenuItem))
            {
                style =
                {
                    position      = "fixed",
                    height        = "100%",
                    width         = "70%",
                    maxWidth = "400px",
                    top           = "50px",
                    background    = "white",
                    boxShadow     = "5px 0 5px -5px rgb(0 0 0 / 28%)",
                    zIndex        = "1",
                    display       = state.HamburgerMenuIsOpen ? "flex" : "none",
                    transition    = "visibility 0s linear 1000ms, opacity 500ms",
                    flexDirection = "column",
                    alignItems    = "center",
                    fontSize      = "18px"
                }
            };

            Element toMenuItem(MainMenuModel m)
            {
                return new a
                       {
                           innerText    = m.Text,
                           href    = "/index.html?page=" + m.Id,
                           onClick = _ => OnMainMenuItemClicked(m.Id),
                           style = { fontSize = "17px", marginTop = "50px" }
                       };
            }
        }

       

        
         
    }
    
}

