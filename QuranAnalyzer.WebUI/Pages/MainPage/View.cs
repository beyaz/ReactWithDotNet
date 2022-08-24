using System;
using QuranAnalyzer.WebUI.Components;
using QuranAnalyzer.WebUI.Pages.CharacterCountingPage;
using ReactWithDotNet;

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


    public View()
    {
        state = new MainViewModel();
        
        StateInitialized += () =>
        {
            state.PageId = Context.Query[QueryKey.Page];
        };
    }

    public void ComponentDidMount()
    {
        Context.ClientTask.ListenEvent("OnHamburgerMenuClicked", nameof(hamburgerMenuClicked));
    }

  



    void hamburgerMenuClicked(bool hamburgerMenuIsOpen) => state.HamburgerMenuIsOpen = hamburgerMenuIsOpen;

    public override Element render()
    {
        return new ApplicationLayout
        {
            topContent  = new TopNavigationPanel(),
            mainContent = buildMainContent(),
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

            if (state.PageId == PageId.WordSearchingPage)
            {
                return new WordSearchingPage.View();
            }


            return new MainPageContent();
        }
        

       

       

        
         
    }

    
}