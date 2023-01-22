namespace QuranAnalyzer.WebUI;

class MainLayout : ReactComponent
{
    public Element Page { get; set; }

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
                new title{ "Quran Analyzer" },

                new style
                {
                    @"
                   html, body {
                       height: 100vh;
                       margin: 0;
                       font-family: 'Nunito Sans', 'Helvetica Neue', Helvetica, Arial, sans-serif;
                       font-size: 16px;
                       color: rgb(51, 51, 51);
                   }
                   
                   #app {
                       width: 100%;
                       height: 100vh;
                   }
                   
                   input:focus, textarea:focus, select:focus {
                       outline: none;
                   }
"
                },

                new link{rel ="stylesheet" , href = "https://fonts.googleapis.com/css?family=Nunito+Sans:400,700,800,900&amp;display=swap", media ="all"},


            },
            new body
            {
                new div(Id("app"), WidthMaximized,Height100vh)
                {
                    Page
                },

                new script{src =$"{root}/index.js"},

                new script
                {
                    $@"
ReactWithDotNet.RenderComponentIn({{
  fullTypeNameOfReactComponent: '{Page.GetType().GetFullName()}',
  containerHtmlElementId: 'app'
}});
"
                }


            }
        };
    }
}