namespace ReactWithDotNet;


public class html : HtmlElement
{
    [React]
    public string xmlns { get; set; } = "http://www.w3.org/1999/xhtml";


    public html() { }

    public html(params IModifier[] modifiers) : base(modifiers) { }

}

public class head : HtmlElement
{

}
public class title : HtmlElement
{

}
public class script : HtmlElement
{
    [React]
    public string src { get; set; }
    
    [React]
    public string type { get; set; }
}


public class body : HtmlElement
{

}
public class meta : HtmlElement
{
    [React]
    public string charset { get; set; }

    [React]
    public string name { get; set; }

    [React]
    public string content { get; set; }

    [React]
    public string httpEquiv { get; set; }

    public static HtmlElementModifier Content(string content) => new(element => ((meta)element).content = content);
    public static HtmlElementModifier HttpEquiv(string httpEquiv) => new(element => ((meta)element).httpEquiv = httpEquiv);
    public static HtmlElementModifier Name(string name) => new(element => ((meta)element).name = name);
    public static HtmlElementModifier Charset(string charset) => new(element => ((meta)element).charset = charset);
}


public class button : HtmlElement
{
    [React]
    public string type { get; set; }
}



public class input : HtmlElement
{
    public input() { }

    public input(params IModifier[] modifiers) : base(modifiers) { }
    
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


    /// <summary>
    /// if you want to handle when user iteraction finished see example below<br/>
    /// component.valueBind = ()=>state.UserInfo.Name<br/>
    /// component.valueBindDebounceTimeout = 600 // milliseconds<br/>
    /// component.valueBindDebounceHandler = OnUserIterationFinished<br/>
    /// </summary>
    public Action valueBindDebounceHandler { get; set; }


    /// <summary>
    /// if you want to handle when user iteraction finished see example below<br/>
    /// component.valueBind = ()=>state.UserInfo.Name<br/>
    /// component.valueBindDebounceTimeout = 600 // milliseconds<br/>
    /// component.valueBindDebounceHandler = OnUserIterationFinished<br/>
    /// </summary>
    public int? valueBindDebounceTimeout { get; set; }
    
    

    /// <summary>
    ///     Occurs when an element loses focus.
    /// </summary>
    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public Action<MouseEvent> onBlur { get; set; }

    /// <summary>
    ///     occurs when an element gets focus.
    /// </summary>
    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public Action<MouseEvent> onFocus { get; set; }


    [React]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticChangeEventArguments")]
    public Action<ChangeEvent> onChange { get; set; }
    
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

    /// <summary>
    /// Download file when clicking on the link (instead of navigating to the file):
    /// </summary>
    [React]
    public string download { get; set; }
    
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
    
    [React]
    public string media { get; set; }
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