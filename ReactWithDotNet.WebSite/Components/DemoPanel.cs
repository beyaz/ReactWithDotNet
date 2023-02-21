
namespace ReactWithDotNet.WebSite.Components;

class DemoPanel : ReactPureComponent
{
    public string CSharpCode { get; set; }

    public Element Element { get; set; }

    protected override Element render()
    {
        return new FlexRow(BoxShadow("rgb(0 0 0 / 34%) 0px 2px 5px 0px"), Padding(15), BorderRadius(5), MarginTopBottom(10), FlexWrap)
        {
            new FlexColumn(AlignItemsFlexStart)
            {
                new img{Src(Asset("csharp.svg")), Width(25), Height(20), MarginTop(5)}, 
                new CSharpCodePanel{ Code = CSharpCode}
            },
            
            new FlexRowCentered
            {
                Element ?? "Element is empty"
            }
        };
    }
}

class MainPageContentSample : ReactPureComponent
{
    protected override Element render()
    {
        return new DemoPanel
        {
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
            Element = new HomePageDemoComponent()
        }+MinWidth(500) + FontSize10;
    }
}


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
                display = "flex",
                flexDirection = "column",
                border  = "1px solid #dee2e6",
            },
            
            children =
            {
                new button
                {
                    text    = "Show Message",
                    onClick = OnButtonClicked,
                    style =
                    {
                        display    = "inline-block",
                        fontWeight = "400",
                        border     = "1px solid transparent",
                        textAlign       = "center",
                        padding         = ".375rem .75rem",
                        fontSize        = "1rem",
                        lineHeight      = "1.5",
                        borderRadius    = ".25rem",
                        color           = "#fff",
                        backgroundColor = "#007bff",
                        hover           = {color = "#f1ba72"}
                    }
                },
                
                state.ShowMessage ? "Hello world." : null
            }
        };
    }
}