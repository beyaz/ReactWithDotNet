// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public sealed class Splitter : ElementBase
{
    /// <summary>
    ///     Orientation of the panels, valid values are "horizontal" and "vertical".
    ///     <br/>
    ///     @defaultValue horizontal
    /// </summary>
    [ReactProp]
    public string layout { get; set; }
    
    /// <summary>
    ///     Size of the divider in pixels.
    ///     <br/>
    ///     @defaultValue 4
    /// </summary>
    [ReactProp]
    public double? gutterSize { get; set; }
    
    /// <summary>
    ///     Storage identifier of a stateful Splitter.
    /// </summary>
    [ReactProp]
    public string stateKey { get; set; }
    
    /// <summary>
    ///     Defines where a stateful splitter keeps its state, valid values are "session" for sessionStorage and "local" for localStorage.
    ///     <br/>
    ///     @defaultValue session
    /// </summary>
    [ReactProp]
    public string stateStorage { get; set; }
    
    protected override Element GetSuspenseFallbackElement()
    {
        return _children?.FirstOrDefault() ?? new ReactWithDotNetSkeleton.Skeleton();
    }
    
    public static IModifier Modify(Action<Splitter> modifyAction) => CreateThirdPartyReactComponentModifier(modifyAction);
    
    public Splitter(){ }
    
    public Splitter(params Action<Splitter>[] modifiers) => modifiers.ApplyAll(Add);
    
    public void Add(Action<Splitter> modify) => modify?.Invoke(this);
}
