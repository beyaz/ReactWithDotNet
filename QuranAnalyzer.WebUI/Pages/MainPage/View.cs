using System.Threading.Tasks;
using PuppeteerSharp;
using QuranAnalyzer.WebUI.Components;
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
        Context.Set(ContextKey.HamburgerMenuIsOpen,state.HamburgerMenuIsOpen);
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

            if (state.PageId == "32")
            {
                try
                {
                    return new div(A().GetAwaiter().GetResult());
                }
                catch (Exception exception)
                {
                    return new div(exception.ToString());
                }
            }


            return new MainPageContent();
        }

        async Task<string> A()
        {
            await new BrowserFetcher(Product.Chrome).DownloadAsync();
            await using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless          = true,
                IgnoreHTTPSErrors = true,
                Args = new []{ "--enable-features=NetworkService" },
                
                Product = Product.Chrome,

            });
            await using var page = await browser.NewPageAsync();
            // await page.GoToAsync("http://beyaz1404-001-site1.itempurl.com/?p=17");

            await page.GoToAsync("https://www.google.com/search?q=--enable-features%3DNetworkService&oq=--enable-features%3DNetworkService&aqs=edge..69i57.239j0j4&sourceid=chrome&ie=UTF-8");
            await page.WaitForNavigationAsync();
            var content = await page.GetContentAsync();

            await browser.CloseAsync();

            return content;
        }
    }
}