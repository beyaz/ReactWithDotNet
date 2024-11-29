namespace ReactWithDotNet.WebSite.Pages;

sealed class PageDocumentation_ServerDrivenUI : PageDocumentation
{
    protected override Element CreateContent()
    {
        
    
        return new FlexRow(JustifyContentCenter, WidthFull)
        {
            new article(PaddingTopBottom(4 * rem))
            {
                new h1(FontSize32, FontWeight400, LineHeight32, MarginBottom(1.2 * rem))
                {
                    "Setup"
                },
                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "How to initialize"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "Skip long and boring documentations!"  ,
                    
                    br,br,
                    "As always we said that, if you are familiar to c# and react library, you are already know ReactWithDotNet library",
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

                

            }
        };
    
    }
}