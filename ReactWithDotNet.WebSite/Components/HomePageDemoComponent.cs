namespace ReactWithDotNet.WebSite.Components;

class HomePageDemoComponentState
{
    public bool ShowMessage { get; set; }
}
class HomePageDemoComponent : ReactComponent<HomePageDemoComponentState>
{
    protected override void constructor()
    {
        state = new HomePageDemoComponentState();
    }

    void OnButtonClicked(MouseEvent e)
    {
        state.ShowMessage = true;
    }

    protected override Element render()
    {
        return new div
        {
            style =
            {
                display       = "flex",
                flexDirection = "column"
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
                        border          = "1px solid transparent",
                        textAlign       = "center",
                        padding         = ".375rem .75rem",
                        fontSize        = "1rem",
                        lineHeight      = "1.5",
                        borderRadius    = ".25rem",
                        color           = "#fff",
                        width           = "200px",
                        backgroundColor = "#007bff",
                        hover           = {color = "#f1ba72"}
                    }
                },
                
                state.ShowMessage ? "Hello world." : null
            }
        };
    }
}