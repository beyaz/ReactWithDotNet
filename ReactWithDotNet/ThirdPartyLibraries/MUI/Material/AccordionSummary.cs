// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class AccordionSummary : ElementBase
{
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     The icon to display as the expand indicator.
    /// </summary>
    [ReactProp]
    public Element expandIcon { get; set; }
    
    /// <summary>
    ///     The icon to display as the expand indicator.
    /// </summary>
    public static Modifier ExpandIcon(Element value) => CreateThirdPartyReactComponentModifier<AccordionSummary>(x => x.expandIcon = value);
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic sx { get; } = new ExpandoObject();
    
    protected override Element GetSuspenseFallbackElement()
    {
        return _children?.FirstOrDefault() ?? new ReactWithDotNetSkeleton.Skeleton();
    }
    
    public AccordionSummary(){ }
    
    public AccordionSummary(params Action<AccordionSummary>[] modifiers) => modifiers.ApplyAll(Add);
    
    public AccordionSummary(StyleModifier styleModifier, params Action<AccordionSummary>[] modifiers)
    {
        Add(styleModifier);
        modifiers.ApplyAll(Add);
    }
    
    public void Add(Action<AccordionSummary> modify) => modify?.Invoke(this);
}
