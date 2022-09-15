using System;
using System.Linq.Expressions;

namespace ReactWithDotNet;


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
public class b : HtmlElement
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
    
    [React]
    public string target { get; set; }
}
public class lu : HtmlElement
{
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
    public int width { get; set; }

    [React]
    public int height { get; set; }
}




public class nav : HtmlElement
{
}

public class HtmlTextNode : HtmlElement
{
}