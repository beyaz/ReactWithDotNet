namespace ReactWithDotNet.WebSite.Components;

class HomePageDemoComponentState
{
    public string Message { get; set; }
}

class HomePageDemoComponent : Component<HomePageDemoComponentState>
{
    protected override Task constructor()
    {
        state = new();

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return new div(DisplayFlexColumn)
        {
            new button(Size(200, 50), DisplayFlexRowCentered)
            {
                text    = "Show Message",
                onClick = OnButtonClicked,
                style =
                {
                    Background(Blue500),
                    Border(1, solid, Gray100),
                    Color(White),
                    FontSize14,
                    Hover(Color(WhiteSmoke), FontSize16, Background(Blue600))
                }
            },

            new span(Color(Gray600))
            {
                state.Message
            }
        };
    }

    Task OnButtonClicked(MouseEvent e)
    {
        state.Message = "Hello world";

        return Task.CompletedTask;
    }
}