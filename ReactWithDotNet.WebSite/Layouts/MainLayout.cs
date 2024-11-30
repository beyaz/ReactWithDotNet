namespace ReactWithDotNet.WebSite;

sealed class MainLayout : PureComponent, IPageLayout
{
    public string ContainerDomElementId => "app";

    public ComponentRenderInfo RenderInfo { get; set; }

    protected override Element render()
    {
        return new html
        {
            Lang("tr"),
            DirLtr,

            // Global Styles
            Margin(0),
            Color(Theme.text_primary),
            FontFamily("'IBM Plex Sans', -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, 'Helvetica Neue', Arial,sans-serif, 'Apple Color Emoji', 'Segoe UI Emoji', 'Segoe UI Symbol'"),
            WebkitFontSmoothingAntialiased,
            MozOsxFontSmoothingGrayScale,
            FontWeight400,
            FontSize(1 * rem),
            LineHeight(1.5 * CssUnit.em),

            new head
            {
                new meta { charset = "utf-8" },
                new meta { name    = "viewport", content = "width=device-width, initial-scale=1" },
                new title { "React with DotNet" },

                new link
                {
                    rel         = "stylesheet",
                    type        = "text/css",
                    href        = IndexCssFilePath
                },
                
                new style
                {
                    """

                    * {
                        margin: 0;
                        padding: 0;
                        box-sizing: border-box;
                    }

                    """
                },

                arrangeFonts(),

                new link { href = "https://fonts.googleapis.com/icon?family=Material+Icons", rel = "stylesheet" }
            },
            new body(Margin(0), Height100vh)
            {
                new div(Id(ContainerDomElementId), SizeFull),

                // After page first rendered in client then connect with react system in background.
                // So user first iteraction time will be minimize.
                
                new script
                {
                    type = "module",

                    text =
                        $$"""
                          import {ReactWithDotNet} from '{{IndexJsFilePath}}';

                          ReactWithDotNet.StrictMode = false;

                          ReactWithDotNet.RequestHandlerPath = '{{RequestHandlerPath}}';
                          ReactWithDotNet.MetadataRequestHandlerPath = '{{MetadataRequestHandlerPath}}';

                          ReactWithDotNet.RenderComponentIn({
                            idOfContainerHtmlElement: '{{ContainerDomElementId}}',
                            renderInfo: {{RenderInfo.ToJsonString()}}
                          });
                          """
                }
            },
            
            new script
            {
                type = "text/javascript",
                src  = "long.js"
            },
            new script
            {
                type = "text/javascript",
                src = "clr.js"
            }
        };

        IEnumerable<Element> arrangeFonts()
        {
            return
            [
                new link { href = "https://fonts.gstatic.com", rel = "preconnect" },

                new link { href = "https://fonts.googleapis.com", rel = "preconnect" },

                new link { href = "https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,300;0,400;0,500;0,700;1,400&display=swap", rel = "stylesheet" },
                
                new link { href = "https://fonts.googleapis.com/css2?family=IBM+Plex+Sans:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;1,100;1,200;1,300;1,400;1,500;1,600;1,700&family=Wix+Madefor+Text:ital,wght@0,400..800;1,400..800&display=swap", rel = "stylesheet" },
                
                new link { href = "https://fonts.googleapis.com/css2?family=IBM+Plex+Sans:ital,wght@0,100;0,200;0,300;0,400;0,500;0,600;0,700;1,100;1,200;1,300;1,400;1,500;1,600;1,700&family=Plus+Jakarta+Sans:ital,wght@0,200..800;1,200..800&family=Wix+Madefor+Text:ital,wght@0,400..800;1,400..800&display=swap", rel = "stylesheet" }

            ];
        }
    }
}

partial class Extensions
{
    public static StyleModifier FontFamily_IBM_Plex_Sans => FontFamily("'IBM Plex Sans'");
    public static StyleModifier FontFamily_PlusJakartaSans_ExtraBold => FontFamily("'Plus Jakarta Sans'");
}