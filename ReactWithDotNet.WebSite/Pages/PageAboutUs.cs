
namespace ReactWithDotNet.WebSite.Pages;

class PageAboutUs : PureComponent
{
    protected override Element render()
    {
        return  new FlexColumn(AlignItemsCenter)
        {
            SpaceY(70),
            new MainContentContainer(JustifyContentCenter, WidthFull, FlexDirectionColumn)
            {
                new h2(new Style { boxSizing = "border-box", color = "rgb(0, 127, 255)", fontFamily = "'IBM Plex Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial, sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol'", fontSize = "14px", fontWeight = "700", letterSpacing = "normal", lineHeight = "21px", margin = "0px 0px 8px", scrollMarginTop = "92px", textAlign = "center" })
                {
                    "About us"
                },
                    
                SpaceY(30),
                
                new FlexRowCentered(FontSize30, FontWeight600,Color(rgb(31, 38, 46)))
                {
                    new HighlightedText
                    {
                        Text = "Build better UIs by [c#] and [react]"
                    }
                },
                
                SpaceY(30),
                new FlexRowCentered
                {
                    "Contact with us for special web apps"
                }
                
                
            }
        };
    }
}






