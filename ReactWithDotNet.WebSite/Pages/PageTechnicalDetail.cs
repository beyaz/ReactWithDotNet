namespace ReactWithDotNet.WebSite.Pages;

using h1 = BlogH1;
using p = BlogP;

sealed class PageTechnicalDetail : PureComponent
{
    protected override Element render()
    {
        return new article(WidthFull)
        {
            new h1{"Technical Details"},
            new p
            {
                "How is it working? How to connect React and c# language?"
            },

            new p
            {
                "ReactWithDotNet is working on the .net core. Creates ReactNode hierarchy in c# language then serialize to client.",
                br,
                "Our client engine recalculates ReactNodes from incoming c# generated nodes",
                br,
                "If any react event occurs then serialize only sub react nodes values to server"
            }
        };
    }
}