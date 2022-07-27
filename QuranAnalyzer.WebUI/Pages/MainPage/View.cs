using System;
using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.CharacterCountingPage;
using ReactWithDotNet;

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
            state.PageId = Context.Query["page"];
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
        return new ApplicationLayout
        {
            topContent     = new TopNavigationPanel
            {
                HamburgerMenuIsOpen    = state.HamburgerMenuIsOpen,
                OnHamburgerMenuClicked = hamburgerMenuClicked
            },
            mainContent    = buildMainContent(),
            menu           = new FixedLeftMenuContainer
            {
                IsOpen   = state.HamburgerMenuIsOpen,
                children = { new LeftMenuContent() }
            },
            mainDivScrollY = state.MainDivScrollY,
            IsBackDropActive = state.HamburgerMenuIsOpen
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
        

       

       

        
         
    }

    
}