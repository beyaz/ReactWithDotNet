// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Dialog : ElementBase
{
    [ReactProp] 
    public bool open { get; set; }

    [ReactProp] 
    public Func<object, string, Task> onClose { get; set; } // (event, reason)

    [ReactProp] 
    public bool? fullScreen { get; set; }

    [ReactProp] 
    public bool? fullWidth { get; set; }

    /// <summary>
    /// 'xs' | 'sm' | 'md' | 'lg' | 'xl' | false | custom string
    /// </summary>
    [ReactProp] 
    public string maxWidth { get; set; }

    [ReactProp] 
    public bool? disableEscapeKeyDown { get; set; }

    [ReactProp] 
    public string ariaLabelledby { get; set; }

    [ReactProp] 
    public string ariaDescribedby { get; set; }

    [ReactProp] 
    public Element backdropComponent { get; set; }

    [ReactProp] 
    public Element paperComponent { get; set; }

    [ReactProp] 
    public Element transitionComponent { get; set; }

    /// <summary>
    /// 'body' | 'paper'
    /// </summary>
    [ReactProp] 
    public string scroll { get; set; }

    [ReactProp] 
    public object transitionDuration { get; set; }

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();

    [ReactProp] 
    public object slotProps { get; set; }

    [ReactProp] 
    public object slots { get; set; }

    [ReactProp] 
    public object paperProps { get; set; }

}
