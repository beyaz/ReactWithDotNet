namespace ReactWithDotNet.WebSite.Components;

class MainContentContainer : PureComponent
{
    readonly Modifier[] modifiers;
    public MainContentContainer()
    { }

    public MainContentContainer(params Modifier[] modifiers)
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

            PaddingLeftRight("5%"),
            MD(PaddingLeftRight("10%")),
            LG(PaddingLeftRight("5%")),
            Data(".net_component_name", nameof(MainContentContainer))

        };

        element.Apply(modifiers);

        return element;
    }
}