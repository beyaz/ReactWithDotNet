namespace ReactWithDotNet.WebSite.Components;

class MainPageContentSample : ReactPureComponent
{
    protected override Element render()
    {
        return new DemoPanel
        {
            IsSourceCodeVisible = true,
            CSharpCode = @"class HomePageDemoComponentState
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
                display = ""flex"",
                flexDirection = ""column"",
                border  = ""1px solid #dee2e6"",
            },
            
            children =
            {
                new button
                {
                    text    = ""Show Message"",
                    onClick = OnButtonClicked,
                    style =
                    {
                        display         = ""inline-block"",
                        fontWeight      = ""400"",
                        textAlign       = ""center"",
                        border          = ""1px solid transparent"",
                        padding         = "".375rem .75rem"",
                        fontSize        = ""1rem"",
                        lineHeight      = ""1.5"",
                        borderRadius    = "".25rem"",
                        color           = ""#fff"",
                        backgroundColor = ""#007bff""
                    }
                },
                
                state.ShowMessage ? ""Hello world."" : null
            }
        };
    }
}",
            FullNameOfElement = typeof(HomePageDemoComponent).FullName
        };
    }
}