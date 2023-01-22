
namespace QuranAnalyzer.WebUI.Components;

static class Theme
{
    public static string TextColor = "rgb(51, 51, 51)";
}

class divWithBorder: HtmlElement
{
    public override string Type => nameof(div);
    
    public divWithBorder()
    {
        this.Apply(BorderRadius(5),ComponentBorder);
    }

    public divWithBorder(params IModifier[] modifiers)
    {
        this.Apply(BorderRadius(5), ComponentBorder);
        this.Apply(modifiers);
    }
}

class LargeTitle : ReactComponent
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
            style    = { FontSize18, TextAlignCenter},
            text = text
        };
    }
}


class Important : ReactComponent
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


class Title : HtmlElement
{
    public override string Type => nameof(div);
    public Title(string innerText)
    {
        this.innerText = innerText;

        style.fontSize  = "18px";
        style.textAlign = "center";
    }
}

class SubTitle : HtmlElement
{
    public override string Type => nameof(div);
    public SubTitle(string innerText)
    {
        this.innerText = innerText;

        style.fontSize  = "16px";
        style.textAlign = "center";
    }
}

class Article : ReactComponent
{
    protected override Element render()
    {
        return new article
        {
            Aria("label","article"),
            Children(children)
        };
    }
}