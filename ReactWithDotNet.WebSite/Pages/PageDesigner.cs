namespace ReactWithDotNet.WebSite.Pages;

using h1 = BlogH1;
using p = BlogP;

sealed class PageDesigner : PureComponent
{
    protected override Element render()
    {
        return new BlogPageLayout
        {
            new h1 { "How to preview component and edit hotreload mode?" },
            SpaceY(8),
            new p
            {
                "Preview and design is very important in component driven development style. There are many tools for designing like no code low code.",
                br,
                "What is the main problem with this tools? How to deal with?",
                br,
                "Problem is connect data with this tools. Data can be any format.",
                br,
                "Second problem is this tools have borders. You cannot apply your custom code styles."
            },

            SpaceY(16),

            new p { "What is the solution?" },

            SpaceY(30),
            new h1 { "What is ReactWithDotNet Designer" },
            SpaceY(8),
            new p
            {
                "In Debug mode, goto url '/$'",
                br,
                "Yo wil see this form",
                new img { Src(Asset("design.png")), WidthFull, Padding(16) },
                "When you edit code in hotreload mode then designer updates component preview."
            },
            
            SpaceY(24),
            new PlayButton
            {
                Label    = "Play tutorial (1 min)",
                VideoUrl = Asset("Designer.mp4")
            },

            SpaceY(80)
        };
    }
}