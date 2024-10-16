namespace ReactWithDotNet.WebSite.Pages;

sealed class PageDocumentation_Start : PageDocumentation
{
    protected override Element CreateContent()
    {
        
    
        return new FlexRow(JustifyContentCenter, WidthFull)
        {
            new article(PaddingTopBottom(4 * rem))
            {
                new h1(FontSize32, FontWeight400, LineHeight32, MarginBottom(1.2 * rem))
                {
                    "Quick Start"
                },
                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "c# & react"
                },

                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    "If you know reactjs and c# language, you already familier to ReactWithDotNet ecosystem.",
                    "Build react component tree and event handlers in c# language at .net core server afterthat component tree will render in browser."
                },

                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "Lets look at component decleration in react"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    new CodeViewerForTypeScript(Height(300), MD(Width(300)))
                    {
                        """
                        class Test extends React.Component 
                        {
                            constructor(props) 
                            {
                                super(props);
                                this.state = {  };
                            }
                        
                            componentDidMount() 
                            {
                              
                            }
                        
                            render ()
                            {
                                this.setState({ hello: "Geek!" });
                            }
                        }
                        """
                    }
                },
               

            }
        };
    
    }
}