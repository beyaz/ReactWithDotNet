using System;
using System.Linq;
using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.CharacterCountingPage;
using ReactWithDotNet;
using static QuranAnalyzer.WebUI.Extensions;

namespace QuranAnalyzer.WebUI.Pages.MainPage;


static class PageId
{
    public const string MainPage = nameof(MainPage);
    public const string SecuringDataWithCurrentTechnology = nameof(SecuringDataWithCurrentTechnology);
    public const string InitialLetters = nameof(InitialLetters);
    public const string QuestionAnswerPage = nameof(QuestionAnswerPage);
    public const string ContactPage = nameof(ContactPage);
    public const string CharacterCounting = nameof(CharacterCounting);

    public const string PageIdOfMushafOptionsDetail = nameof(PageIdOfMushafOptionsDetail);
}

[Serializable]
public class MainViewModel
{
    public string PageId { get; set; }
    public string SelectedFact { get; set; }
    public string SummaryText { get; set; }

    public string OperationName { get; set; }
    public bool IsBlocked { get; set; }

    public bool HamburgerMenuIsOpen { get; set; }

    public double MainDivScrollY { get; set; }
    

    public string LastClickedMenuId { get; set; }

    public CharacterCountingViewModel CharacterCountingViewModel { get; set; }
}

class View : ReactComponent<MainViewModel>
{


    public View()
    {
        state = new MainViewModel();
        
        StateInitialized += () =>
        {
            if (Context.TryGetValue(BrowserInformation.UrlParameters).TryGetValue("page", out var pageId))
            {
                state.PageId = pageId;
            }
        };
    }

    public void ComponentDidMount()
    {
        Context.ClientTask.ListenEvent("MainContentDivScrollChanged", nameof(OnMainContentDivScrollChanged));
        Context.ClientTask.CallJsFunction("RegisterScrollEvents");
    }
    
    public void OnMainContentDivScrollChanged(double mainDivScrollY)
    {
        state.MainDivScrollY = mainDivScrollY;
    }


    
    void hamburgerMenuClicked(string _) => state.HamburgerMenuIsOpen = !state.HamburgerMenuIsOpen;

    public override Element render()
    {
        return new Components.MainPage
        {
            topContent     = buildTopNav(),
            mainContent    = buildMainContent(),
            menu           = buildLeftMenu(),
            mainDivScrollY = state.MainDivScrollY
        };

        Element buildMainContent()
        {
            if (state.PageId == PageId.QuestionAnswerPage)
            {
                return new QuestionAnswerPage.View();
            }

            if (state.PageId == PageId.ContactPage)
            {
                return new ContactPage.View();
            }

            if (state.PageId == PageId.InitialLetters)
            {
                return new InitialLetters.View();
            }

            if (state.PageId == PageId.CharacterCounting)
            {
                return new CharacterCountingView();
            }

            if (state.PageId == PageId.SecuringDataWithCurrentTechnology)
            {
                return new SecuringDataWithCurrentTechnology.View();
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
                        new SiteTitle("19 Sistemi Nedir")
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
            
            return BuildLeftMenu(state.HamburgerMenuIsOpen);


        }

       

        
         
    }

    static Element BuildLeftMenu(bool hamburgerMenuIsOpen)
    {
        return new div
        {
            children =
            {
                BuildLeftMenuContent()
            },
            style =
            {
                position       = "fixed",
                height         = "100%",
                width          = "70%",
                maxWidth       = "400px",
                top            = "50px",
                background     = "white",
                boxShadow      = "5px 0 5px -5px rgb(0 0 0 / 28%)",
                zIndex         = "1",
                display        = hamburgerMenuIsOpen ? "flex" : "none",
                
            }
        };
    }
    
    static Element BuildLeftMenuContent()
    {
        return new div
        {
            children =
            {
                new VSpace(20),
                toSidebarMenuItem("1 - Anasayfa",PageId.MainPage),
                new VSpace(20),
                toSidebarMenuItem("2 - Günümüz Teknolojisinde Veri Nasıl Korunur",PageId.SecuringDataWithCurrentTechnology),
                new VSpace(20),
                toSidebarMenuItem("3 - Ön Bilgiler",PageId.MainPage),
                new VSpace(20),
                toSidebarMenuItem("4 - Başlangıç Harfleri",PageId.InitialLetters),
                new VSpace(20),
                toSidebarMenuItem("5 - Soru - Cevap",PageId.MainPage),
                new VSpace(20),
                toSidebarMenuItem("6 - İletişim",PageId.ContactPage),
            },
            style =
            {
                width_height = "100%",
                display = "flex",
                flexDirection = "column",
                alignItems = "center",
                textAlign = "center"
            }
        };

        static Element toSidebarMenuItem(string text, string id)
        {
            return new a
            {
                className = "q-sidebarlink",
                innerText = text,
                href      = GetPageLink(id),
                style     = { padding = "10px", textDecoration = "none", color = "Black", overflowWrap = "break-word"}
            };
        }


    }
    
  

}

