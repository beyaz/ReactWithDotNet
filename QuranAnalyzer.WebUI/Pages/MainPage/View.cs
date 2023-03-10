using QuranAnalyzer.WebUI.Pages.AlternativeSystems;
using QuranAnalyzer.WebUI.Pages.CharacterCountingPage;
using QuranAnalyzer.WebUI.Pages.CountOfAllahPage;
using QuranAnalyzer.WebUI.Pages.DefinitionPage;
using QuranAnalyzer.WebUI.Pages.InitialLetters;
using QuranAnalyzer.WebUI.Pages.PreInformation;
using QuranAnalyzer.WebUI.Pages.VerseListContainsAllInitialLettersPage;
using QuranAnalyzer.WebUI.Pages.WordSearchingPage;

namespace QuranAnalyzer.WebUI.Pages.MainPage;

[Serializable]
public class MainViewModel
{
    public CharacterCountingViewModel CharacterCountingViewModel { get; set; }

    public bool HamburgerMenuIsOpen { get; set; }
    public bool IsBlocked { get; set; }

    public string LastClickedMenuId { get; set; }

    public double MainDivScrollY { get; set; }

    public string OperationName { get; set; }
    public string PageId { get; set; }
    public string SelectedFact { get; set; }
    public string SummaryText { get; set; }
}

static class HamburgerMenuEvents
{
    public static void OnHamburgerMenuClosed(this Client client, Action handler)
    {
        client.ListenEvent(HamburgerMenuClosed, handler);
    }

    public static void OnHandleHamburgerMenuOpened(this Client client, Action handler)
    {
        client.ListenEvent(HamburgerMenuOpened, handler);
    }

    static void HamburgerMenuClosed(this Client client)
    {
        client.DispatchEvent(nameof(HamburgerMenuClosed));
    }

    static void HamburgerMenuOpened(this Client client)
    {
        client.DispatchEvent(nameof(HamburgerMenuOpened));
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

    protected override Element render()
    {
        return new div(WidthMaximized, HeightAuto)
        {
            new FixedTopPanelContainer(),

            new main(DisplayFlex, FlexDirectionRow, JustifyContentCenter, HeightAuto)
            {
                new MainContentContainer
                {
                    MarginTopBottom(30),

                    new LeftMenu
                    {
                        SelectedPageId = state.PageId,
                        style =
                        {
                            MinWidth(230),
                            MarginTop(101),
                            MediaQueryOnMobile(new Style { DisplayNone })
                        }
                    },

                    buildMainContent()
                }
            }
        };

        Element buildMainContent()
        {
            return state.PageId switch
            {
                PageId.MainPage => new MainPageContent(),
                PageId.SecuringDataWithCurrentTechnology => new SecuringDataWithCurrentTechnology.View(),
                PageId.PreInformation => new PreInformationView(),
                PageId.InitialLetters => new AllInitialLetters(),
                PageId.QuestionAnswerPage => new QuestionAnswerPage.View(),
                PageId.ContactPage => new ContactPage.View(),
                PageId.CharacterCounting => new CharacterCountingView(),
                PageId.WordSearchingPage => new WordSearchingView(),
                PageId.AlternativeSystems => new AlternativeSystemsView(),
                PageId.Definition => new DefinitionView(),
                PageId.PageIdOfMushafOptionsDetail => new MushafOptionsDetail.View(),
                PageId.WhoIsReshadKhalifePage => new WhoIsReshadKhalifePage(),
                PageId.WhyFamousPeopleAreSilentPage => new WhyFamousPeopleAreSilentPage(),
                PageId.AboutEdipYukselPage => new AboutEdipYukselPage(),
                PageId.PageVerseListContainsAllInitialLetters => new PageVerseListContainsAllInitialLetters(),
                PageId.AdditionalVersesPage => new AdditionalVersesPage(),
                PageId.CountOfAllahPage => new CountOfAllah(),
                PageId.AllInitialLettersCombined => new AllInitialLettersCombined.View(),
                PageId.WhereIsTheProblemPage => new WhereIsTheProblemPage(),
                PageId.IsHeMessangerPage => new IsHeMessangerPage(),
                _ => new MainPageContent()
            };
        }
    }

    void OnHamburgerMenuClosed()
    {
        state.HamburgerMenuIsOpen = false;
    }

    void OnHamburgerMenuOpened()
    {
        state.HamburgerMenuIsOpen = true;
    }
}