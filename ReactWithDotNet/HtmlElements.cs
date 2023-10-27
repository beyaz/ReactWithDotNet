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

public sealed class head : HtmlElement;










[Serializable]
public sealed class InputValueBinder
{
    public static implicit operator InputValueBinder(string value)
    {
        return new InputValueBinder();
    }
    public static implicit operator InputValueBinder(double value)
    {
        return new InputValueBinder();
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
    public input(StyleModifier[] styleModifiers)
    {
        style.Apply(styleModifiers);
    }

    [ReactProp]
    public string required { get; set; }
    
    [ReactProp]
    public string autoComplete { get; set; }

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
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticFocusEventArguments")]
    public FocusEventHandler onBlur { get; set; }

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticChangeEventArguments")]
    public Func<ChangeEvent,Task> onChange { get; set; }

    /// <summary>
    ///     occurs when an element gets focus.
    /// </summary>
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticFocusEventArguments")]
    public FocusEventHandler onFocus { get; set; } // TODO: give FocusEvent react

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
    public Expression<Func<InputValueBinder>> valueBind { get; set; }

    /// <summary>
    ///     if you want to handle when user iteraction finished see example below<br />
    ///     component.valueBind = ()=>state.UserInfo.Name<br />
    ///     component.valueBindDebounceTimeout = 600 // milliseconds<br />
    ///     component.valueBindDebounceHandler = OnUserIterationFinished<br />
    /// </summary>
    public Func<Task> valueBindDebounceHandler { get; set; }

    /// <summary>
    ///     if you want to handle when user iteraction finished see example below<br />
    ///     component.valueBind = ()=>state.UserInfo.Name<br />
    ///     component.valueBindDebounceTimeout = 600 // milliseconds<br />
    ///     component.valueBindDebounceHandler = OnUserIterationFinished<br />
    /// </summary>
    public int? valueBindDebounceTimeout { get; set; }

    [ReactProp]
    public int? max { get; set; }
    
    [ReactProp]
    public int? min { get; set; }
    
    [ReactProp]
    public int? step { get; set; }
}

public sealed class HtmlTextNode : HtmlElement;

sealed class br : HtmlElement;

public sealed class iframe : HtmlElement
{
    [ReactProp]
    public string src { get; set; }

    public static HtmlElementModifier Src(string value) => CreateHtmlElementModifier<iframe>(element => element.src = value);
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
    public Func<ChangeEvent,Task> onChange { get; set; }

    [ReactProp]
    public string value { get; set; }

    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = nameof(onChange))]
    public Expression<Func<string>> valueBind { get; set; }
    
    [ReactProp]
    public string disabled { get; set; }
}


public sealed class style : HtmlElement
{
    public void Add(CssClass classInfo)
    {
        if (classInfo == null)
        {
            throw new ArgumentNullException(nameof(classInfo));
        }
        
        var nameOfClass = classInfo._name?.Trim();
        if (string.IsNullOrWhiteSpace(nameOfClass))
        {
            throw new ArgumentException(classInfo._name);
        }

        if (nameOfClass[0] != '.')
        {
            nameOfClass = "." + nameOfClass;
        }
        
        innerText += Environment.NewLine + nameOfClass +"{"+ new Style(classInfo._styleModifiers).ToCss() + "}";
    }
}

public sealed class CssClass
{
    internal readonly string _name;
    internal readonly IReadOnlyList<StyleModifier> _styleModifiers;

    public CssClass(string name, IReadOnlyList<StyleModifier> styleModifiers)
    {
        _name                = name;
        _styleModifiers = styleModifiers;
    }
}

public sealed class link : HtmlElement
{
    [ReactProp]
    public string href { get; set; }

    [ReactProp]
    public string media { get; set; }

    [ReactProp]
    public string rel { get; set; }
    
    [ReactProp]
    public string sizes { get; set; }
    

    [ReactProp]
    public string type { get; set; }

    [ReactProp]
    public string @as { get; set; }

    [ReactProp]
    public string integrity { get; set; }

    [ReactProp]
    public string crossorigin { get; set; }

    [ReactProp]
    public string referrerpolicy { get; set; }
}

public sealed class textarea : HtmlElement
{
    public textarea()
    {
        
    }
    public textarea(params IModifier[] modifiers) : base(modifiers)
    {
    }
    public textarea(StyleModifier[] styleModifiers)
    {
        style.Apply(styleModifiers);
    }
    
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
    public string defaultValue { get; set; }

    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e.target.value", eventName = "onChange")]
    public Expression<Func<string>> valueBind { get; set; }
    
    [ReactProp]
    public string disabled { get; set; }
}


public sealed class form : HtmlElement
{
    [ReactProp]
    public string action { get; set; }
    
    [ReactProp]
    public string method { get; set; }

    public form()
    {
    }

    public form(params IModifier[] modifiers) : base(modifiers)
    {
    }
}