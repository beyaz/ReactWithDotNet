using QuranAnalyzer.WebUI.Pages.CountOfAllahPage;
using QuranAnalyzer.WebUI.Pages.InitialLetters;
using QuranAnalyzer.WebUI.Pages.PageCharacterCounting;
using QuranAnalyzer.WebUI.Pages.PageVerseListContainsAllInitialLetters;
using QuranAnalyzer.WebUI.Pages.PageWordSearching;

namespace QuranAnalyzer.WebUI.Pages.MainPage;

class View : ReactPureComponent
{
    string SelectedPageId => Context.Query[QueryKey.Page] ?? PageId.MainPage;

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
                        SelectedPageId = SelectedPageId,
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
            return SelectedPageId switch
            {
                PageId.MainPage                               => new MainPageContent(),
                PageId.SecuringDataWithCurrentTechnology      => new PageSecuringDataWithCurrentTechnology(),
                PageId.PreInformation                         => new PagePreInformation(),
                PageId.InitialLetters                         => new AllInitialLetters(),
                PageId.QuestionAnswerPage                     => new PageQuestionAnswer(),
                PageId.ContactPage                            => new PageContact(),
                PageId.CharacterCounting                      => new CharacterCountingView(),
                PageId.WordSearchingPage                      => new WordSearchingView(),
                PageId.AlternativeSystems                     => new PageAlternativeSystems(),
                PageId.Definition                             => new PageSimpleDefinition(),
                PageId.PageIdOfMushafOptionsDetail            => new PageMushafOptionsDetail(),
                PageId.WhoIsReshadKhalifePage                 => new PageWhoIsReshadKhalife(),
                PageId.WhyFamousPeopleAreSilentPage           => new PageWhyFamousPeopleAreSilent(),
                PageId.AboutEdipYukselPage                    => new PageAboutEdipYuksel(),
                PageId.PageVerseListContainsAllInitialLetters => new PageVerseListContainsAllInitialLettersView(),
                PageId.AdditionalVersesPage                   => new PageAdditionalVerses(),
                PageId.CountOfAllahPage                       => new CountOfAllah(),
                PageId.AllInitialLettersCombined              => new PageAllInitialLettersCombined.PageAllInitialLettersCombinedView(),
                PageId.WhereIsTheProblemPage                  => new PageWhereIsTheProblem(),
                PageId.IsHeMessangerPage                      => new PageIsHeMessanger(),
                _                                             => new MainPageContent()
            };
        }
    }
}