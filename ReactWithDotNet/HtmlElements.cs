namespace ReactWithDotNet;

public class button : HtmlElement
{
    [React]
    public string type { get; set; }
}



public class input : HtmlElement
{
    [React]
    public string autocomplete { get; set; }

    [React]
    public bool? @checked { get; set; }

    [React]
    [ReactBind(targetProp = nameof(@checked), jsValueAccess = "e.target.checked", eventName = "onChange")]
    public Expression<Func<bool>> checkedBind { get; set; }

    [React]
    public bool? defaultChecked { get; set; }

    [React]
    public string defaultValue { get; set; }

    [React]
    public string name { get; set; }

    [React]
    public string placeholder { get; set; }

    [React]
    public bool? readOnly { get; set; }

    [React]
    public string type { get; set; }

    [React]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceEmptyStringWhenIsNull")]
    public string value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceEmptyStringWhenIsNull")]
    public Expression<Func<string>> valueBind { get; set; }
}

public class a : HtmlElement
{
    public a()
    {
    }

    public a(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    public string href { get; set; }

    [React]
    public string target { get; set; }
}

public class img : HtmlElement
{
    public img()
    {
    }

    public img(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    public string alt { get; set; }

    [React]
    public int height { get; set; }

    [React]
    public string loading { get; set; }

    [React]
    public string src { get; set; }

    [React]
    public int width { get; set; }
}

public class HtmlTextNode : HtmlElement
{
}

public class br : HtmlElement
{
}

public class iframe : HtmlElement
{
    [React]
    public string src { get; set; }
}

public class select : HtmlElement
{
    public select()
    {
    }

    public select(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticChangeEventArguments")]
    public Action<ChangeEvent> onChange { get; set; }

    [React]
    public string value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = nameof(onChange))]
    public Expression<Func<string>> valueBind { get; set; }
}

public class option : HtmlElement
{
    public option()
    {
    }

    public option(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [React]
    public bool? selected { get; set; }

    [React]
    public string value { get; set; }
}

public class style : HtmlElement
{
}
public class link : HtmlElement
{
    [React]
    public string href { get; set; }

    [React]
    public string rel { get; set; }
}

public class textarea : HtmlElement
{
   

    [React]
    public int? cols { get; set; }

    [React]
    public int? rows { get; set; }
    

    [React]
    public string name { get; set; }

    [React]
    public string placeholder { get; set; }

    [React]
    public bool? readOnly { get; set; }
    

    [React]
    public string value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }
}