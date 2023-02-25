
namespace ReactWithDotNet.WebSite.Components;

class DemoPanel : ReactPureComponent
{
    public string CSharpCode { get; set; }

    public Element Element { get; set; }

    protected override Element render()
    {
        return new FlexRow(BoxShadow("rgb(0 0 0 / 34%) 0px 2px 5px 0px"), Padding(10), BorderRadius(5), MarginTopBottom(1), FlexWrap)
        {
            new fieldset(Border("1px solid #dee2e6"),WidthMaximized)
            {
                new legend{new img{Src(Asset("csharp.svg")), Width(25), Height(20)}},
                new FlexColumn(AlignItemsFlexStart,WidthMaximized, HeightMaximized)
                {
                    new CSharpCodePanel{ Code = CSharpCode}
                }
            },
            
            new fieldset(Border("1px solid #dee2e6"), WidthMaximized)
            {
                new legend{"Output"},
                Element ?? "Element is empty"
            }
        };
    }
}


class DemoContainer : ReactPureComponent
{
    public bool ShowSourceCode { get; set; }

    protected override Element render()
    {
        return new FlexRowCentered(BackgroundColor(Theme[Context].grey_100), Padding(40), WidthMaximized, BorderRadius(10), PositionRelative)
        {
            children,
            new Button{size ="small",onClick = OnSourceCodeClicked, children = { "Show c# source code" }, style = { PositionAbsolute, Right(1), Bottom(1) }},
        };
    }

    void OnSourceCodeClicked(MouseEvent obj)
    {


    }
}
