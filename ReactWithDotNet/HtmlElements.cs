namespace ReactWithDotNet;



public class button : HtmlElement
{
    [React]
    public string type { get; set; }
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
    public string defaultValue { get; set; }

    [React]
    public string placeholder { get; set; }
    

    [React]
    public bool? readOnly { get; set; }
    

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }

    [React]
    public bool? @checked { get; set; }

    [React]
    public bool? defaultChecked { get; set; }

    [React]
    public string autocomplete { get; set; }

    [React]
    [ReactBind(targetProp = nameof(@checked), jsValueAccess = "e.target.checked", eventName = "onChange")]
    public Expression<Func<bool>> checkedBind { get; set; }
}






public class a : HtmlElement
{
    [React]
    public string href { get; set; }
    
    [React]
    public string target { get; set; }
    
    public a() { }
    public a(params IModifier[] modifiers) : base(modifiers) { }

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

    [React]
    public string loading { get; set; }

    public img() { }
    public img(params IModifier[] modifiers) : base(modifiers) { }
}




public class nav : HtmlElement
{
    public nav() { }
    public nav(params IModifier[] modifiers) : base(modifiers) { }
}

public class main : HtmlElement
{
    public main() { }
    public main(params IModifier[] modifiers) : base(modifiers) { }
}
public class footer : HtmlElement
{
    public footer() { }
    public footer(params IModifier[] modifiers) : base(modifiers) { }
}



public class HtmlTextNode : HtmlElement
{
    
}