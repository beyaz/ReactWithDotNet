namespace AlyaVillas.WebUI;

class container :   ReactWithDotNet.ReactComponent
{
    readonly Action<HtmlElement>[] modifiers;

    public container(params Action<HtmlElement>[] modifiers)
    {
        this.modifiers = modifiers;
    }
        
    protected override Element render()
    {
        var style = new Style
        {
            maxWidth = $"{Mixin.containerWidth}px",
            width    = "100%",
            margin   = "0 auto",
            padding  = "0 40px"
        };

        if (Context.IsMobile())
        {
            style.padding  = "0 24px";
            style.maxWidth = "100%";
        }

        if (Context.Is(MediaSize.mini))
        {
            style.padding  = "0 16px";
            style.maxWidth = "100%";
        }

        var returnDiv = new div(style)
        {
            Children = children
        };

        returnDiv.Apply(modifiers);
        
        return returnDiv;
    }
}