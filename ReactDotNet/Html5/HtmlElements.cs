using System;
using System.Linq.Expressions;

namespace ReactDotNet.Html5;


public class button : HtmlElement
{
}

public class label : HtmlElement
{
    [React]
    public string htmlFor { get; set; }
}

public class input : HtmlElement
{
    [React]
    public string type { get; set; }

    [React]
    public string name { get; set; }

    [React]
    public string value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }

    [React]
    public bool @checked { get; set; }

    [React]
    [ReactBind(targetProp = nameof(@checked), jsValueAccess = "e.target.checked", eventName = "onChange")]
    public Expression<Func<bool>> checkedBind { get; set; }
}

public class i : HtmlElement
{
}

public class p : HtmlElement
{
    public p()
    {
    }

    public p(string innerText)
    {
        this.innerText = innerText;
    }
}
public class pre : HtmlElement
{
}

public class h5 : HtmlElement
{
}

public class h4 : HtmlElement
{
    public h4()
    {
    }

    public h4(string innerText)
    {
        this.innerText = innerText;
    }
}

public class h3 : HtmlElement
{
    public h3()
    {
    }

    public h3(string innerText)
    {
        this.innerText = innerText;
    }
}

public class h2 : HtmlElement
{
  

    public h2()
    {
    }

    public h2(string innerText)
    {
        this.innerText = innerText;
    }
}

public class h1 : HtmlElement
{
    public h1()
    {
    }

    public h1(string innerText)
    {
        this.innerText = innerText;
    }
}

public class a : HtmlElement
{
    [React]
    public string href { get; set; }
}

public class li : HtmlElement
{
}
public class img : HtmlElement
{
    [React]
    public string src { get; set; }

    [React]
    public string alt { get; set; }

    [React]
    public new int width { get; set; }

    [React]
    public new int height { get; set; }
}

public class HPanel : div
{
    static void InitializeStyle(Style style)
    {
        style.display       = Display.flex;
        style.flexDirection = FlexDirection.row;
        style.alignItems    = AlignItems.stretch;
        style.width         = "100%";

    }
    public HPanel()
    {
        InitializeStyle(style);
    }
    
}

public sealed class VPanel : div
{
    public VPanel()
    {
        style.display       = Display.flex;
        style.flexDirection = FlexDirection.column;
        style.alignItems    = AlignItems.stretch;
        style.height        = "100%";
    }
}

public class svg : HtmlElement
{
    [React]
    public string xmlns { get; set; } = "http://www.w3.org/2000/svg";

    [React]
    public string viewBox { get; set; }
}

public class path : HtmlElement
{
    [React]
    public string d { get; set; }

    [React]
    public string fill { get; set; }
}

public class rect : HtmlElement
{
    [React]
    public int y { get; set; }
}

public class nav : HtmlElement
{
}