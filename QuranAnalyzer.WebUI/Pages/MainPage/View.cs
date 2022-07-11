using System;
using System.Linq;
using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.FactPage;
using ReactDotNet.Html5;
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

    public CharacterCountingViewModel CharacterCountingViewModel { get; set; }
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
            RouteToMethod = nameof(OnFactClicked),
            After = new ClientTaskListenEvent
            {
                EventName     = "MainContentDivScrollChanged",
                RouteToMethod = nameof(OnMainContentDivScrollChanged)
            }
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
            if (state.PageId is not null)
            {
                var pages = new PageBase[]
                {
                    new QuestionAnswerPage.View(),
                    new ContactPage.View(),
                    new InitialLetters.View(),
                    
                };

                var page = pages.FirstOrDefault(x => x.id == state.PageId);
                if (page is not null)
                {
                    return page;
                }

                if (state.PageId == "CharacterCounting")
                {
                    var tt = new CharacterCountingView();

                    tt.Context = Context;
                    
                    tt.constructor();
                    
                    return tt;
                }
            }

            return new div
            {
                new LargeTitle("Bu sitede ne var?"){ style={marginTopBottom = "14px"}},
                new div(@"
Bir kaç yıl önce Kuran hakkında 19 Sistemi - 19 Mucizesi benzeri isimlerle duyduğum bir konu üzerine vakit buldukça araştırma yapma fırsatım oldu.
Elimden geldiğince aklımın yettiği ölçüde nedir ne değildir inceledim.
Bu konu etrafında doğru yanlış bir çok şey duydum.
Konuyu kendi bizzat incelemek ve konu etrafında dönen doğru yanlış şeylere kendimce verdiğim cevapları paylaşmak istedim.
Böylelikle konuyu araştıran kişiler için tarafsız bir gözlem sunmak niyetindeyim.
Site şu 3 ana konuyu ele alır.
Lütfen konunun anlaşılması için soldaki menüyü sırası ile takip ediniz.
")
            };

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
                    display        = Display.flex,
                    justifyContent = JustifyContent.flex_start,
                    alignItems     = AlignItems.center
                }
            };
        }

        Element buildLeftMenu()
        {
            return new div(mainMenuModels.Select(toMenuItem))
            {
                style =
                {
                    position      = Position.@fixed,
                    height        = "100%",
                    width         = "70%",
                    maxWidth = "400px",
                    top           = px(50),
                    background    = "white",
                    boxShadow     = "5px 0 5px -5px rgb(0 0 0 / 28%)",
                    zIndex        = "1",
                    display       = state.HamburgerMenuIsOpen ? Display.flex : Display.none,
                    transition    = "visibility 0s linear 1000ms, opacity 500ms",
                    flexDirection = FlexDirection.column,
                    alignItems    = AlignItems.center,
                    fontSize      = px(18)
                }
            };

            Element toMenuItem(MainMenuModel m)
            {
                return new a
                       {
                           innerText    = m.Text,
                           href    = "/index.html?page=" + m.Id,
                           onClick = _ => OnMainMenuItemClicked(m.Id),
                           style = { fontSize = px(17), marginTop = px(50) }
                       };
            }
        }

       

        
         
    }
    
}

