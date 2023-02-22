namespace ReactWithDotNet.WebSite.Components;

class MainContentContainer : ReactComponent
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

            Children(children),

            MediaQueryOnMobile(MarginLeftRight("5%")),
            MediaQueryOnTablet(MarginLeftRight("10%")),
            MediaQueryOnDesktop(MarginLeftRight("5%")),
            Role(nameof(MainContentContainer))

        };

        element.Apply(modifiers);

        return element;
    }
}