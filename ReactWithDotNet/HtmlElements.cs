namespace ReactWithDotNet;

public sealed class html : HtmlElement
{
    public html()
    {
    }

    public html(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [ReactProp]
    public string xmlns { get; set; } = "http://www.w3.org/1999/xhtml";
}

public sealed class head : HtmlElement
{
}

public sealed class title : HtmlElement
{
}

public sealed class script : HtmlElement
{
    [ReactProp]
    public string src { get; set; }

    [ReactProp]
    public string type { get; set; }
}

public sealed class body : HtmlElement
{
    public body()
    {
    }

    public body(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public sealed class meta : HtmlElement
{
    [ReactProp]
    public string charset { get; set; }

    [ReactProp]
    public string content { get; set; }

    [ReactProp]
    public string httpEquiv { get; set; }

    [ReactProp]
    public string name { get; set; }

    public static HtmlElementModifier Charset(string charset) => Modify<meta>(element => element.charset = charset);
    public static HtmlElementModifier Content(string content) => Modify<meta>(element => element.content = content);
    public static HtmlElementModifier HttpEquiv(string httpEquiv) => Modify<meta>(element => element.httpEquiv = httpEquiv);
    public static HtmlElementModifier Name(string name) => Modify<meta>(element => element.name = name);
}

public sealed class button : HtmlElement
{
    [ReactProp]
    public string type { get; set; }

    public button()
    {
    }

    public button(params IModifier[] modifiers) : base(modifiers)
    {
    }
}

public sealed class input : HtmlElement
{
    public input()
    {
    }

    public input(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [ReactProp]
    public string autocomplete { get; set; }

    [ReactProp]
    public bool? @checked { get; set; }

    [ReactProp]
    [ReactBind(targetProp = nameof(@checked), jsValueAccess = "e.target.checked", eventName = "onChange")]
    public Expression<Func<bool>> checkedBind { get; set; }

    [ReactProp]
    public bool? defaultChecked { get; set; }

    [ReactProp]
    public string defaultValue { get; set; }

    [ReactProp]
    public bool? disabled { get; set; }

    [ReactProp]
    public string name { get; set; }

    /// <summary>
    ///     Occurs when an element loses focus.
    /// </summary>
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public Action<MouseEvent> onBlur { get; set; }

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticChangeEventArguments")]
    public Action<ChangeEvent> onChange { get; set; }

    /// <summary>
    ///     occurs when an element gets focus.
    /// </summary>
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticMouseEventArguments")]
    public Action<MouseEvent> onFocus { get; set; }

    [ReactProp]
    public string placeholder { get; set; }

    [ReactProp]
    public bool? readOnly { get; set; }

    [ReactProp]
    public string type { get; set; }

    [ReactProp]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceEmptyStringWhenIsNull")]
    public string value { get; set; }

    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceEmptyStringWhenIsNull")]
    public Expression<Func<string>> valueBind { get; set; }

    /// <summary>
    ///     if you want to handle when user iteraction finished see example below<br />
    ///     component.valueBind = ()=>state.UserInfo.Name<br />
    ///     component.valueBindDebounceTimeout = 600 // milliseconds<br />
    ///     component.valueBindDebounceHandler = OnUserIterationFinished<br />
    /// </summary>
    public Action valueBindDebounceHandler { get; set; }

    /// <summary>
    ///     if you want to handle when user iteraction finished see example below<br />
    ///     component.valueBind = ()=>state.UserInfo.Name<br />
    ///     component.valueBindDebounceTimeout = 600 // milliseconds<br />
    ///     component.valueBindDebounceHandler = OnUserIterationFinished<br />
    /// </summary>
    public int? valueBindDebounceTimeout { get; set; }
}

public sealed class HtmlTextNode : HtmlElement
{
}

sealed class br : HtmlElement
{
}

public sealed class iframe : HtmlElement
{
    [ReactProp]
    public string src { get; set; }
}

public sealed class select : HtmlElement
{
    public select()
    {
    }

    public select(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticChangeEventArguments")]
    public Action<ChangeEvent> onChange { get; set; }

    [ReactProp]
    public string value { get; set; }

    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = nameof(onChange))]
    public Expression<Func<string>> valueBind { get; set; }
}

public sealed class option : HtmlElement
{
    public option()
    {
    }

    public option(params IModifier[] modifiers) : base(modifiers)
    {
    }

    [ReactProp]
    public bool? selected { get; set; }

    [ReactProp]
    public string value { get; set; }
}

public sealed class style : HtmlElement
{
}

public sealed class link : HtmlElement
{
    [ReactProp]
    public string crossOrigin { get; set; }

    [ReactProp]
    public string href { get; set; }

    [ReactProp]
    public string media { get; set; }

    [ReactProp]
    public string rel { get; set; }

    [ReactProp]
    public string type { get; set; }

    [ReactProp]
    public string @as { get; set; }
}

public sealed class textarea : HtmlElement
{
    [ReactProp]
    public int? cols { get; set; }

    [ReactProp]
    public string name { get; set; }

    [ReactProp]
    public string placeholder { get; set; }

    [ReactProp]
    public bool? readOnly { get; set; }

    [ReactProp]
    public int? rows { get; set; }

    [ReactProp]
    public string value { get; set; }

    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }
}