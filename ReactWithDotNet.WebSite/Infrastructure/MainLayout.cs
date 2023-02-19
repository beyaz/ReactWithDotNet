
namespace ReactWithDotNet.WebSite;



class MainLayout : ReactComponent
{
    public Element Page { get; set; }

    public string QueryString { get; set; }
    
    public string RenderInfo { get; set; }

    protected override Element render()
    {
        const string root = "wwwroot";

        return new html
        {
            Lang("tr"),
            DirLtr,

            new head
            {
                new meta{charset = "utf-8"},
                new meta{name    = "viewport", content = "width=device-width, initial-scale=1"},
                new title{ "React with DotNet" },

                new style
                {
                    @"
                   html, body {
                       height: 100vh;
                       margin: 0;
                       font-family: 'IBM Plex Sans',-apple-system,BlinkMacSystemFont,'Segoe UI',Roboto,'Helvetica Neue',Arial,sans-serif,'Apple Color Emoji','Segoe UI Emoji','Segoe UI Symbol';
                       font-size: 16px;
                       color: #1a2027;
                   }
                   
                   input:focus, textarea:focus, select:focus {
                       outline: none;
                   }
"
                },


                new link { rel  = "preconnect", href = "https://fonts.googleapis.com" },
                
                new link { rel  = "preconnect", href = "https://fonts.gstatic.com", crossOrigin =  "true"},
                
                new link { href = "https://fonts.googleapis.com/css2?family=IBM+Plex+Mono&display=swap", rel = "stylesheet" },





                new link { href = "https://fonts.googleapis.com/css2?family=Roboto:ital,wght@0,300;0,400;0,500;0,700;1,400&amp;display=swap", rel = "stylesheet" }


            },
            new body
            {
                new div(Id("app"), WidthMaximized,Height100vh)
                {
                    Page
                },

                // After page first rendered in client then connect with react system in background.
                // So user first iteraction time will be minimize.
                
               
                new script { type = "module", src = $"{root}/dist/index.js" },

                new script
                {
                    type = "module",
                    text = 
                    $@"

import {{ReactWithDotNet}} from './{root}/dist/index.js';

ReactWithDotNet.RenderComponentIn({{
  idOfContainerHtmlElement: 'app',
  renderInfo: {RenderInfo}
}});

"
                }


            }
        };
    }
}