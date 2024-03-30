namespace ReactWithDotNet.WebSite.HeaderComponents;

class MainPageHeader : PureComponent
{
    protected override Element render()
    {
        return new header(DisplayFlex, JustifyContentCenter, BoxShadow($"inset 0px -1px 1px {Theme.grey_100}"))
        {
            PositionSticky, Top(0), BackgroundColor(rgba(255, 255, 255, 0.8)), BackdropFilterBlur(6),
            
            new div(ContainerStyle)
            {
                new HeaderMenuBar()
            }
        };
    }
}

class Footer : PureComponent
{
    protected override Element render()
    {
        return new footer(BorderTop(Solid(1, Theme.grey_100)), Height(50),WidthFull)
        {
            DisplayFlexRowCentered,
            new HighlightedText
            {
                Text = "React [\u2665] .Net"
            }
        };
    }
}