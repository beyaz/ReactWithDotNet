using QuranAnalyzer.WebUI.Pages.CharacterCountingPage;
using QuranAnalyzer.WebUI.Pages.InitialLetters;
using QuranAnalyzer.WebUI.Pages.VerseListContainsAllInitialLettersPage;

namespace QuranAnalyzer.WebUI.Pages.MainPage;

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

static class HamburgerMenuEvents
{
    static void HamburgerMenuClosed(this Client client)
    {
        client.DispatchEvent(nameof(HamburgerMenuClosed));
    }

    static void HamburgerMenuOpened(this Client client)
    {
        client.DispatchEvent(nameof(HamburgerMenuOpened));
    }
    public static void OnHamburgerMenuClosed(this Client client, Action handler)
    {
        client.ListenEvent(HamburgerMenuClosed, handler);
    }

    public static void OnHandleHamburgerMenuOpened(this Client client, Action handler)
    {
        client.ListenEvent(HamburgerMenuOpened, handler);
    }
}

class View : ReactComponent<MainViewModel>
{
    
  
    protected override void constructor()
    {
        state = new MainViewModel
        {
            PageId = Context.Query[QueryKey.Page]
        };

        
        Client.OnHandleHamburgerMenuOpened(OnHamburgerMenuOpened);
        Client.OnHamburgerMenuClosed(OnHamburgerMenuClosed);
    }

    void OnHamburgerMenuClosed()
    {
        state.HamburgerMenuIsOpen = false;
    }

    void OnHamburgerMenuOpened()
    {
        state.HamburgerMenuIsOpen = true;
    }

    protected override Element render()
    {
        return new div(WidthMaximized, HeightAuto)
        {
            new FixedTopPanelContainer(),
            
            new main(DisplayFlex,FlexDirectionRow, JustifyContentCenter, HeightAuto)
            {
                new MainContentContainer
                {
                    MarginTopBottom(30),

                    new LeftMenu
                    {
                        SelectedPageId = state.PageId,
                        style =
                        {
                            MinWidth(230) ,
                            MarginTop(101) ,
                            MediaQueryOnMobile(new Style{DisplayNone})
                        }
                    },

                    buildMainContent()
                }
            }
        };

        Element buildMainContent()
        {
            if (state.PageId == PageId.MainPage)
            {
                return new MainPageContent();
            }

            if (state.PageId == PageId.SecuringDataWithCurrentTechnology)
            {
                return new SecuringDataWithCurrentTechnology.View();
            }

            if (state.PageId == PageId.PreInformation)
            {
                return new PreInformation.PreInformationView();
            }

            if (state.PageId == PageId.InitialLetters)
            {
                return new AllInitialLetters();
            }

            if (state.PageId == PageId.QuestionAnswerPage)
            {
                return new QuestionAnswerPage.View();
            }

            if (state.PageId == PageId.ContactPage)
            {
                return new ContactPage.View();
            }

            if (state.PageId == PageId.CharacterCounting)
            {
                return new CharacterCountingView();
            }

            if (state.PageId == PageId.WordSearchingPage)
            {
                return new WordSearchingPage.WordSearchingView();
            }

            if (state.PageId == PageId.AlternativeSystems)
            {
                return new AlternativeSystems.AlternativeSystemsView();
            }

            if (state.PageId == PageId.Definition)
            {
                return new DefinitionPage.DefinitionView();
            }

            if (state.PageId == PageId.PageIdOfMushafOptionsDetail)
            {
                return new MushafOptionsDetail.View();
            }

            if (state.PageId == PageId.WhoIsReshadKhalifePage)
            {
                return new WhoIsReshadKhalifePage();
            }
            if (state.PageId == PageId.WhyFamousPeopleAreSilentPage)
            {
                return new WhyFamousPeopleAreSilentPage();
            }
            if (state.PageId == PageId.AboutEdipYukselPage)
            {
                return new AboutEdipYukselPage();
            }

            if (state.PageId == PageId.PageVerseListContainsAllInitialLetters)
            {
                return new PageVerseListContainsAllInitialLetters();
            }
            if (state.PageId == PageId.AdditionalVersesPage)
            {
                return new AdditionalVersesPage();
            }

            if (state.PageId == PageId.CountOfAllahPage)
            {
                return new QuranAnalyzer.WebUI.Pages.CountOfAllahPage.CountOfAllah();
            }

            if (state.PageId == PageId.AllInitialLettersCombined)
            {
                return new QuranAnalyzer.WebUI.Pages.AllInitialLettersCombined.View();
            }

            if (state.PageId == PageId.WhereIsTheProblemPage)
            {
                return new WhereIsTheProblemPage();
            }

            if (state.PageId == PageId.IsHeMessangerPage)
            {
                return new IsHeMessangerPage();
            }


            return new MainPageContent();
        }

     
    }
}