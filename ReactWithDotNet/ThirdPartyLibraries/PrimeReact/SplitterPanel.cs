// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public sealed class SplitterPanel : ElementBase
{
    /// <summary>
    ///     Size of the element relative to 100%.
    /// </summary>
    [ReactProp]
    public double? size { get; set; }
    
    /// <summary>
    ///     Minimum size of the element relative to 100%.
    /// </summary>
    [ReactProp]
    public double? minSize { get; set; }
    
    
    protected override Element GetSuspenseFallbackElement()
    {
        return _children?.FirstOrDefault() ?? new ReactWithDotNetSkeleton.Skeleton();
    }
    
    public SplitterPanel(){ }
    
    public SplitterPanel(params Action<SplitterPanel>[] modifiers) => modifiers.ApplyAll(Add);
    
    public SplitterPanel(StyleModifier styleModifier, params Action<SplitterPanel>[] modifiers)
    {
        Add(styleModifier);
        modifiers.ApplyAll(Add);
    }
    
    public void Add(Action<SplitterPanel> modify) => modify?.Invoke(this);
}
