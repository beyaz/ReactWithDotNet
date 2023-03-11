namespace QuranAnalyzer.WebUI.Components;

static class Theme
{
    public static string TextColor = "rgb(51, 51, 51)";
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