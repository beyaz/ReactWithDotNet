namespace ReactWithDotNet.ThirdPartyLibraries.ReactSimpleCodeEditor;

public class Editor : ThirdPartyReactComponent
{
    protected const string Prefix = "ReactWithDotNet.ThirdPartyLibraries.ReactSimpleCodeEditor.";

    /// <summary>
    ///     Callback which will receive text to highlight. You'll need to return an HTML string or a React element with syntax
    ///     highlighting using a library such as prismjs.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInClient(Prefix + "GetHighlightFunction")]
    public string highlight { get; set; }

    /// <summary>
    ///     Whether the editor should ignore tab key presses so that keyboard users can tab past the editor. Users can toggle
    ///     this behaviour using Ctrl+Shift+M (Mac) / Ctrl+M manually when this is false. Default: false.
    /// </summary>
    [ReactProp]
    public bool? ignoreTabKey { get; set; }

    /// <summary>
    ///     Whether to use spaces for indentation. Default: true. If you set it to false, you might also want to set tabSize to
    ///     1.
    /// </summary>
    [ReactProp]
    public bool? insertSpaces { get; set; }

    /// <summary>
    ///     Optional padding for code. Default: 0.
    /// </summary>
    [ReactProp]
    public int? padding { get; set; }

    /// <summary>
    ///     A className for the underlying pre, can be useful for more precise control of its styles.
    /// </summary>
    [ReactProp]
    public string preClassName { get; set; }

    /// <summary>
    ///     The number of characters to insert when pressing tab key. For example, for 4 space indentation, tabSize will be 4
    ///     and insertSpaces will be true. Default: 2.
    /// </summary>
    [ReactProp]
    public int? tabSize { get; set; }

    /// <summary>
    ///     A className for the underlying textarea, can be useful for more precise control of its styles.
    /// </summary>
    [ReactProp]
    public string textareaClassName { get; set; }

    /// <summary>
    ///     An ID for the underlying textarea, can be useful for setting a label.
    /// </summary>
    [ReactProp]
    public string textareaId { get; set; }

    [ReactProp]
    public string value { get; set; }

    [ReactProp]
    [ReactBind(targetProp = nameof(value), jsValueAccess = "e", eventName = "onValueChange")]
    public Expression<Func<string>> valueBind { get; set; }

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

    protected override Element GetSuspenseFallbackElement()
    {
        return base.GetSuspenseFallbackElement() + MinHeight(300) + MinWidth(400);
    }
}