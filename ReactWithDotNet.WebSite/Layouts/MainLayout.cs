
namespace ReactWithDotNet.WebSite;


class MainLayout : PureComponent, IPageLayout
{
    public ComponentRenderInfo RenderInfo { get; set; }
    
    public string ContainerDomElementId => "app";

    protected override Element render()
    {
        const string root = "wwwroot";
        const string fonts = root + "/assets/fonts/";

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
            LineHeight(1.5*CssUnit.em),
            //Background($"url({Asset("background.svg")})"), // TODO: check usage remove


            new head
            {
                new meta{charset = "utf-8"},
                new meta{name    = "viewport", content = "width=device-width, initial-scale=1"},
                new title{ "React with DotNet" },
                
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

                new link { href = "https://fonts.googleapis.com/icon?family=Material+Icons", rel = "stylesheet" },
            },
            new body(Margin(0),Height100vh)
            {
                new div(Id(ContainerDomElementId), WidthHeightMaximized),

                // After page first rendered in client then connect with react system in background.
                // So user first iteraction time will be minimize.
                
               
                new link
                {
                    rel = "stylesheet",
                    type = "text/css",
                    href = $"{root}/dist/index.css",
                    crossorigin = "anonymous"
                },
                

                new script
                {
                    type = "module",
                    text = 
                    $@"

import {{ReactWithDotNet}} from './{root}/dist/index.js?v={Guid.NewGuid():N}';

ReactWithDotNet.StrictMode = false;
ReactWithDotNet.RenderComponentIn({{
  idOfContainerHtmlElement: '{ContainerDomElementId}',
  renderInfo: {RenderInfo.ToJsonString()}
}});

"
                }


            }
        };


        IEnumerable<Element> arrangeFonts()
        {
            
            return new Element[]
            {
                new link{href   = "https://fonts.gstatic.com", rel = "preconnect", crossorigin = "anonymous"},
                
                new link{href   = "https://fonts.googleapis.com", rel = "preconnect"},
                
                new link { href = "https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,300;0,400;0,500;0,700;1,400&display=swap", rel = "stylesheet" },

                // prevent font flash
                // optimized for english characters (40kb -> 6kb)
                new link
                {
                    rel = "preload", 
                    href = $"{fonts}PlusJakartaSans-ExtraBold-subset.woff2",
                    type="font/woff2",
                    crossorigin = "anonymous",
                    @as = "font"
                },
                

                

                
                new style
                {
                    @$"""


                    
html{{  }}

/* IBM Plex Sans */

@font-face{{
    font-family:'IBM Plex Sans';
    src:url({fonts}IBMPlexSans-Regular.woff2) format('woff2'),url({fonts}IBMPlexSans-Regular.woff) format('woff'),url({fonts}IBMPlexSans-Regular.ttf) format('truetype');
    font-weight:400;
    font-style:normal;
    font-display:swap
}}
@font-face{{
    font-family:'IBM Plex Sans';
    src:url({fonts}IBMPlexSans-Medium.woff2) format('woff2'),url({fonts}IBMPlexSans-Medium.woff) format('woff'),url({fonts}IBMPlexSans-Medium.ttf) format('truetype');
    font-weight:500;
    font-style:normal;
    font-display:swap
}}
@font-face{{
    font-family:'IBM Plex Sans';
    src:url({fonts}IBMPlexSans-SemiBold.woff2) format('woff2'),url({fonts}IBMPlexSans-SemiBold.woff) format('woff'),url({fonts}IBMPlexSans-SemiBold.ttf) format('truetype');
    font-weight:600;
    font-style:normal;
    font-display:swap
}}
@font-face{{
    font-family:'IBM Plex Sans';
    src:url({fonts}IBMPlexSans-Bold.woff2) format('woff2'),url({fonts}IBMPlexSans-Bold.woff) format('woff'),url({fonts}IBMPlexSans-Bold.ttf) format('truetype');
    font-weight:700;
    font-style:normal;
    font-display:swap
}}



/* PlusJakartaSans */

@font-face{{
    font-family:'PlusJakartaSans-ExtraBold';
    font-style:normal;
    font-weight:800;
    font-display:swap;
    src:url('{fonts}PlusJakartaSans-ExtraBold-subset.woff2') format('woff2');
}}
 @font-face{{
    font-family:'PlusJakartaSans-Bold';
    font-style:normal;
    font-weight:700;
    font-display:swap;
    src:url('{fonts}PlusJakartaSans-Bold-subset.woff2') format('woff2');
}}

                    """
                }
            };
        }
    }
    
    
    
}


partial class Extensions
{
    public static StyleModifier FontFamily_PlusJakartaSans_ExtraBold => FontFamily("PlusJakartaSans-ExtraBold");
    public static StyleModifier FontFamily_IBM_Plex_Sans=> FontFamily("'IBM Plex Sans'");
}