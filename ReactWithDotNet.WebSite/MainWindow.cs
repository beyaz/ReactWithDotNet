using ReactWithDotNet.WebSite.Content;
using ReactWithDotNet.WebSite.HeaderComponents;
using ReactWithDotNet.WebSite.Pages;

namespace ReactWithDotNet.WebSite;

public class MainWindow : ReactPureComponent
{
    string PageName => Context.Query[QueryKey.Page];

    protected override Element render()
    {

        return new div(WidthMaximized, HeightMaximized)
        {
            
            new MainPageHeader(),

            new main
            {
                createContent,
                //new div(Background("#f9f9f9"))
                //{
                //    new FlexRow(DisplayFlex, JustifyContentCenter)
                //    {
                //        new MainContentContainer(JustifyContentCenter, WidthMaximized,FlexDirectionColumn)
                //        {
                //            new section(DisplayFlexRow,Padding(100), FlexWrap,JustifyContentCenter, Margin(-20), Gap(20), CursorDefault)
                //            {
                //                RawData.YoutubeLinks.Select(x=>new YoutubeCard{ Model = x})
                //            }
                //        }
                //    }
                //},

                new MainContentContainer(JustifyContentCenter, WidthMaximized,FlexDirectionColumn)
                {
                    //new Article{ FilePathInContentFolder = "tr\\6.html"},


                    //new div{BorderBottom(Solid(1,Theme.grey_100)), MarginTopBottom(40)},
                    
                    new section(DisplayFlexRow, FlexWrap,JustifyContentCenter, Margin(-20))
                    {
                        RawData.Cards.Select(x=>new RawCardViewer{ Model = x})
                    }
                }


            },
            
            
            new footer(BorderTop(Solid(1, Theme.grey_100)), Height(50), DisplayFlexRowCentered)
            {
                new HighlightedText{Text = RawData.FooterText}
            }
        };

        Element createContent()
        {
            var typeOfPage = Type.GetType($"ReactWithDotNet.WebSite.Pages.{PageName}") ?? typeof(PageMain);

            return (Element)Activator.CreateInstance(typeOfPage);
        }
    }
}