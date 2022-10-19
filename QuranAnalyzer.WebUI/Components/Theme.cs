
namespace QuranAnalyzer.WebUI.Components;



class divWithBorder: HtmlElement
{
    public override string Type => nameof(div);
    
    public divWithBorder()
    {
        this.Apply(BorderRadius(5),
                   Border("1px solid rgb(218, 220, 224)"));
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
            style    = { FontSize18, TextAlignCenter, FontWeight500},
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
            MarginLeftRight(8), PaddingLeftRight(16),
            Children(children)
        };
    }
}
