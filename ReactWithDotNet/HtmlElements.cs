using System.Text;

namespace ReactWithDotNet;

[Serializable]
public sealed class InputValueBinder
{
    public static implicit operator InputValueBinder(string value)
    {
        return new();
    }

    public static implicit operator InputValueBinder(double value)
    {
        return new();
    }
}

partial class input
{
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
}

public sealed class HtmlTextNode : HtmlElement
{
    internal StringBuilder stringBuilder;
}

sealed class br : HtmlElement;



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

        innerText += Environment.NewLine + nameOfClass + "{" + new Style(classInfo._styleModifiers).ToCss() + "}";
    }
}

public sealed class CssClass
{
    internal readonly string _name;
    internal readonly IReadOnlyList<StyleModifier> _styleModifiers;

    public CssClass(string name, IReadOnlyList<StyleModifier> styleModifiers)
    {
        _name           = name;
        _styleModifiers = styleModifiers;
    }
}

public sealed partial class textarea
{
    /// <summary>
    ///     Occurs when an element loses focus.
    /// </summary>
    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction("ReactWithDotNet::Core::CalculateSyntheticFocusEventArguments")]
    public FocusEventHandler onBlur { get; set; }

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
}