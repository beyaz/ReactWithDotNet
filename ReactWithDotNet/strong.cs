namespace ReactWithDotNet;

public class aside : HtmlElement
{
    public aside()
    {
    }

    public aside(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public class section : HtmlElement
{
    public section()
    {
    }

    public section(params IModifier[] modifiers) : base(modifiers)
    {
    }
}




public class br : HtmlElement
{
}

public class fieldset : HtmlElement
{
    public fieldset()
    {
    }

    public fieldset(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public class legend : HtmlElement
{
    public legend()
    {
    }

    public legend(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public class iframe : HtmlElement
{
    [React]
    public string src { get; set; }
}





















public class select : HtmlElement
{
    [React]
    public string value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = nameof(onChange))]
    public Expression<Func<string>> valueBind { get; set; }

    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticChangeEventArguments")]
    public Action<ChangeEvent> onChange { get; set; }

    public select()
    {
    }

    public select(params IModifier[] modifiers) : base(modifiers)
    {
    }
}
public class option : HtmlElement
{
    [React]
    public bool? selected { get; set; }

    [React]
    public string value { get; set; }
    
    public option()
    {
    }

    public option(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public class style : HtmlElement
{
    
}
