using System;
using System.Net.Mime;
using ReactWithDotNet;
using ReactWithDotNet.PrimeReact;

namespace QuranAnalyzer.WebUI.Components;



class divWithBorder: HtmlElement
{
    public override string Type => nameof(div);
    
    public divWithBorder()
    {
        style.border       = "1px solid rgb(218, 220, 224)";
        style.borderRadius = "5px";
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
            style    = { fontSize = "18px", textAlign = "center", fontWeight = "500"},
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
            style = { fontWeight = "500" },
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
            style = { marginLeftRight = "8px", paddingLeftRight = "16px"},
            Children = children
        };
    }
}
