namespace ReactWithDotNet.WebSite.Components;

class MainContentContainer : PureComponent
{
    readonly IModifier[] modifiers;
    public MainContentContainer()
    { }

    public MainContentContainer(params IModifier[] modifiers)
    {
        this.modifiers = modifiers;
    }
    protected override Element render()
    {
        var element = new FlexRow
        {
            MaxWidth(1200),
            JustifyContentCenter,

            children,

            MediaQueryOnMobile(PaddingLeftRight("5%")),
            MediaQueryOnTablet(PaddingLeftRight("10%")),
            MediaQueryOnDesktop(PaddingLeftRight("5%")),
            Data(".net_component_name", nameof(MainContentContainer))

        };

        element.Apply(modifiers);

        return element;
    }
}