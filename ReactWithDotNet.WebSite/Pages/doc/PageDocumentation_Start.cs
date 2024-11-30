namespace ReactWithDotNet.WebSite.Pages;

using h1 = BlogH1;
using p = BlogP;

sealed class PageDocumentation_Start : PageDocumentation
{
    protected override Element CreateContent()
    {
        
            
        return new FlexRow(JustifyContentCenter, WidthFull)
        {
            new article(PaddingTopBottom(4 * rem))
            {
                new h1
                {
                    "Quick Start"
                },
                Space,
                new p
                {
                    "c# & react"
                },
                Space,
                Space,
                new p
                {
                    "If you know reactjs and c# language, you already familier to ReactWithDotNet ecosystem.",
                    "Build react component tree and event handlers in c# language at .net core server afterthat component tree will render in browser."
                },
                Space,
                Space,
                new p
                {
                    "Lets look at component decleration in react"
                },
                new CodeViewer(CodeViewer.LangTypeScript, Height(300), MD(Width(400)))
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
                },
                Space,Space,Space,Space,
                new p
                {
                    "In ReactWithDotNet system, the c# version of this component is"
                },
                new CodeViewer(CodeViewer.LangCSharp, Height(400), MD(Width(400)))
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
                },
                Space,Space,Space,Space,
                new p
                {
                    "Let's create simple functional component"
                },
                new CodeViewer(CodeViewer.LangTypeScript, Height(200), MD(Width(400)))
                {
                    """
                    const HelloWorld = () => {
                      return (
                        <div style={{ fontSize: '14px', color: 'gray' }}>
                          <h1>Hello World!</h1>
                        </div>
                      );
                    };

                    // sample usage
                    <div className="App">
                      <HelloWorld />
                    </div>
                    """
                },
                Space,Space,Space,Space,
                new p
                {
                    "Here is c# version"
                },
                new CodeViewer(CodeViewer.LangTypeScript, Height(210), MD(Width(400)))
                {
                    """
                    Element HelloWorld()
                    {
                      return new div(FontSize14, Color("gray"))
                      {
                        new h1 { "Hello World!" }
                      };
                    }

                    // sample usage
                    new div(ClassName("App"))
                    {
                        HelloWorld
                    }
                    """
                },
                Space,Space,Space,Space,
                new p
                {
                    "Here is c# async version"
                },
                new CodeViewer(CodeViewer.LangTypeScript, Height(330), MD(Width(400)))
                {
                    """
                    async Task<string> GetMessageFromDb()
                    {
                        await Task.Delay(1);
                    
                        return "Hello World!";
                    }

                    async Task<Element> HelloWorld()
                    {
                        return new div(ClassName("App"))
                        {
                            new h1 { await GetMessageFromDb() }
                        };
                    }

                    // sample usage
                    new div(ClassName("App"))
                    {
                        HelloWorld
                    }
                    """
                },
                Space,Space,Space,Space,
                new p
                {
                    "As you can see, main idea is writing react components in c# language at serverside.",
                    br,
                    "Our aim is to combine the strengths of both platforms (C# and ReactJS) to provide an easier web development environment.",
                    br,
                    "You dont need to import or compile any js library. All you need to reference ReactWithDotNet.dll to your .net core webapi project."
                }
            }
        };
    }
}