namespace ReactWithDotNet.WebSite.Pages;

using h1 = BlogH1;
using p = BlogP;

sealed class PageDocumentation_Setup : PageDocumentation
{
    protected override Element CreateContent()
    {
        return new article(PaddingTopBottom(4 * rem))
        {
            new h1
            {
                "Setup"
            },
            Space,
            new p
            {
                "Skip long and boring documentations!",
                br,
                "Stop thinking about js bundle size problems, npm and library dependencies.",

                br, br,
                "As always we said that, if you are familiar to c# and react library, you are already know ReactWithDotNet library.",
                br, br,
                "Best way to learn ReactWithDotNet library is see our 'Counter' sample project",
                br,
                br,
                new a
                {
                    "Counter Sample",
                    Href("https://github.com/beyaz/ReactWithDotNet.Samples")
                }
            },
            SpaceY(200)
            
            
        };
    }
}