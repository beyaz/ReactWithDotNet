namespace ReactWithDotNet.WebSite._1_HelloWorld;


class HomePageDemoComponentState
{
    public bool ShowMessage { get; set; }
}
class HomePageDemoComponent : Component<HomePageDemoComponentState>
{
    protected override Task constructor()
    {
        state = new HomePageDemoComponentState();
        
        return Task.CompletedTask;
    }

    Task OnButtonClicked(MouseEvent e)
    {
        state.ShowMessage = true;
        
        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return new div
        {
            style =
            {
                display       = "flex",
                flexDirection = "column",
                border        = "1px solid #dee2e6",
            },
            
            children =
            {
                new button
                {
                    text    = "Show Message",
                    onClick = OnButtonClicked,
                    style =
                    {
                        display         = "inline-block",
                        fontWeight      = "400",
                        textAlign       = "center",
                        border          = "1px solid transparent",
                        padding         = ".375rem .75rem",
                        fontSize        = "1rem",
                        lineHeight      = "1.5",
                        borderRadius    = ".25rem",
                        color           = "#fff",
                        backgroundColor = "#007bff"
                    }
                },
                
                state.ShowMessage ? "Hello world." : null
            }
        };
    }
}
