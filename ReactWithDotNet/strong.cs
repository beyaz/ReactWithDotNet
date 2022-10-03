namespace ReactWithDotNet;

public class aside : HtmlElement
{
    public aside()
    {
    }

    public aside(params Modifier[] modifiers) : base(modifiers)
    {
    }
}

public class section : HtmlElement
{
    public section()
    {
    }

    public section(params Modifier[] modifiers) : base(modifiers)
    {
    }
}

public class small : HtmlElement
{
    public small()
    {
    }

    public small(params Modifier[] modifiers) : base(modifiers)
    {
    }
}

public class ul : HtmlElement
{
    public ul()
    {
    }

    public ul(params Modifier[] modifiers) : base(modifiers)
    {
    }
}

public class br : HtmlElement
{
}

public class article : HtmlElement
{
    public article()
    {
    }

    public article(params Modifier[] modifiers) : base(modifiers)
    {
    }
}

public class fieldset : HtmlElement
{
    public fieldset()
    {
    }

    public fieldset(params Modifier[] modifiers) : base(modifiers)
    {
    }
}

public class legend : HtmlElement
{
    public legend()
    {
    }

    public legend(params Modifier[] modifiers) : base(modifiers)
    {
    }
}

public class iframe : HtmlElement
{
    [React]
    public string src { get; set; }
}

public class strong : HtmlElement
{
    public strong()
    {
    }

    public strong(params Modifier[] modifiers) : base(modifiers)
    {
    }
}

public class span : HtmlElement
{
    public span()
    {
    }

    public span(params Modifier[] modifiers) : base(modifiers)
    {
    }

    public span(string innerText)
    {
        this.innerText = innerText;
    }
}

public class select : HtmlElement
{
    [React]
    public string value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }
    
    
    public select()
    {
    }

    public select(params Modifier[] modifiers) : base(modifiers)
    {
    }
}
public class option : HtmlElement
{
    public option()
    {
    }

    public option(params Modifier[] modifiers) : base(modifiers)
    {
    }
}