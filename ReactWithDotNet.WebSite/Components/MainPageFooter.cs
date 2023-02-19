namespace ReactWithDotNet.WebSite.Components;

class MainPageFooter: ReactPureComponent
{
    protected override Element render()
    {
        return new footer
        {
            BoxShadow($"{Theme[Context].grey_100} -1px -1px 1px"),
            Height(50),
            new div("Copyright")
        };
    }
}