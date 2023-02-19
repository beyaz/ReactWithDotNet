using ReactWithDotNet.WebSite.Components;

namespace ReactWithDotNet.WebSite;

public class MainWindow : ReactComponent
{
    public string PageId { get; set; }

    protected override void constructor()
    {
        PageId = Context.Query[QueryKey.Page];
    }

    protected override Element render()
    {
        //return new span(Text("react.js"))
        //{
        //    style =
        //    {
        //        webkitBackgroundClip = "text",
        //        webkitTextFillColor  = "transparent",
        //        background           = $"linear-gradient(to right, yellow, {Theme[Context].primary_700})"

        //    }
        //};
        
        if (PageId == nameof(FigmaCss2ReactInlineStyleConverterView))
        {
            return new FigmaCss2ReactInlineStyleConverterView();
        }

        if (PageId == nameof(HtmlToCSharpView))
        {
            return new HtmlToCSharpView();
        }

        return new div
        {
            new header(PositionSticky,DisplayFlex, JustifyContentCenter, BoxShadow($"inset 0px -1px 1px {Theme[Context].grey_100}"))
            {
                new MainContentContainer(JustifyContentFlexStart, WidthMaximized)
                {
                    new HeaderMenuBar()
                }
               
            },
            new main(PaddingTopBottom(80))
            {
                new MainContentContainer(JustifyContentCenter, WidthMaximized)
                {
                   new FlexRow(Gap(50))
                   {
                       new MainPageContentDescription(),
                       new MainPageContentSample()
                   }
                }

            },

            new footer
            {
                BoxShadow($"{Theme[Context].grey_100} -1px -1px 1px"),
                Height(200),
                new div("Copyright")
            }
        };

        return new divVerticalCentered
        {
            new a { text = "Figma css to React inline style", href = $"?{QueryKey.Page}={nameof(FigmaCss2ReactInlineStyleConverterView)}" },

            new a { text = "Html to ReactWithDotNet", href = $"?{QueryKey.Page}={nameof(HtmlToCSharpView)}" }
        };

    }
}

static class QueryKey
{
    public static string Page = "p";
}