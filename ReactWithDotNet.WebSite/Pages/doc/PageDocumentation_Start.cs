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
                    new CodeViewer(CodeViewer.LangTypeScript, Height(300), MD(Width(300)))
                    {
                        """
                        class PrintHello extends React.Component 
                        {
                            constructor(props) 
                            {
                                super(props);
                                this.state = { message: 'loading...' };
                            }
                        
                            componentDidMount() 
                            {
                                this.setState({ message: "hello world" });
                            }
                        
                            render ()
                            {
                                return <div>{this.state.message}</div>
                            }
                        }
                        """
                    }
                },
               
                new h2(FontSize24, FontWeight400, LineHeight32, MarginBottom(1 * rem))
                {
                    "In ReactWithDotNet system, the c# version of this component is"
                },
                new p(LineHeight28, MarginBottom(1.5 * rem))
                {
                    new CodeViewer(CodeViewer.LangCSharp, Height(300), MD(Width(300)))
                    {
                        """
                        record PrintHelloState
                        {
                            public string Message { get; init; }
                        }
                        class PrintHello : Component<PrintHelloState>
                        {
                            protected override Task constructor()
                            {
                                state = new (){ Message = "loading..." };
                                
                                return Task.CompletedTask;
                            }
                        
                            protected override Task componentDidMount() 
                            {
                                state = state with { Message = "hello world" };
                                
                                return Task.CompletedTask;
                            }
                        
                            protected override Element render()
                            {
                                return new div { this.state.message };
                            }
                        }
                        """
                    }
                },

            }
        };
    
    }
}