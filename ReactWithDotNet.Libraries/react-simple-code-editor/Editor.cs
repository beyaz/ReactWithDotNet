using System;
using System.Linq.Expressions;

namespace ReactWithDotNet.react_simple_code_editor;

public class Editor : ThirdPartyReactComponent
{
    /// <summary>
    /// json, js ...
    /// </summary>
    [React]
    [ReactTransformValueInClient($"{nameof(ReactWithDotNet)}.{nameof(react_simple_code_editor)}.{nameof(highlight)}")]
    public string highlight { get; set; }

    [React]
    public string value { get; set; }

    [React]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e", eventName = nameof(onValueChange))]
    public Expression<Func<string>> valueBind { get; set; }

    [React]
    public Action<string> onValueChange { get; set; }
}