namespace QuranAnalyzer.WebUI.Components;

static class Theme
{
    public static string TextColor = "rgb(51, 51, 51)";
}

class divWithBorder : HtmlElement
{
    public divWithBorder()
    {
        this.Apply(BorderRadius(5), ComponentBorder);
    }

    public divWithBorder(params IModifier[] modifiers)
    {
        this.Apply(BorderRadius(5), ComponentBorder);
        this.Apply(modifiers);
    }

    public override string Type => nameof(div);
}

class LargeTitle : ReactPureComponent
{
    readonly string text;

    public LargeTitle(string text)
    {
        this.text = text;
    }

    protected override Element render()
    {
        return new div
        {
            style = { FontSize18, TextAlignCenter },
            text  = text
        };
    }
}

class Important : ReactPureComponent
{
    readonly string text;

    public Important(string text)
    {
        this.text = text;
    }

    protected override Element render()
    {
        return new div
        {
            style = { FontWeight500 },
            text  = text
        };
    }
}

class Title : ReactPureComponent
{
    readonly string text;

    public Title(string text)
    {
        this.text = text;
    }

    protected override Element render()
    {
        return new div
        {
            style = { FontSize18, TextAlignCenter },
            text  = text
        };
    }
}

class SubTitle : ReactPureComponent
{
    readonly string text;

    public SubTitle(string text)
    {
        this.text = text;
    }

    protected override Element render()
    {
        return new div
        {
            style = { FontSize16, TextAlignCenter },
            text  = text
        };
    }
}

class Article : ReactComponent
{
    protected override Element render()
    {
        return new article
        {
            Aria("label", "article"),
            Children(children)
        };
    }
}