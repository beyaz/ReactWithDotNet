// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Accordion : ElementBase
{
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     If `true`, expands the accordion by default.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? defaultExpanded { get; set; }
    
    /// <summary>
    ///     If `true`, expands the accordion by default.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static IModifier DefaultExpanded(bool? value) => CreateThirdPartyReactComponentModifier<Accordion>(x => x.defaultExpanded = value);
    
    /// <summary>
    ///     If `true`, the component is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? disabled { get; set; }
    
    /// <summary>
    ///     If `true`, the component is disabled.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static IModifier Disabled(bool? value) => CreateThirdPartyReactComponentModifier<Accordion>(x => x.disabled = value);
    
    /// <summary>
    ///     If `true`, it removes the margin between two expanded accordion items and the increase of height.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? disableGutters { get; set; }
    
    /// <summary>
    ///     If `true`, it removes the margin between two expanded accordion items and the increase of height.
    ///     <br/>
    ///     @default false
    /// </summary>
    public static IModifier DisableGutters(bool? value) => CreateThirdPartyReactComponentModifier<Accordion>(x => x.disableGutters = value);
    
    /// <summary>
    ///     If `true`, expands the accordion, otherwise collapse it.
    ///     <br/>
    ///     Setting this prop enables control over the accordion.
    /// </summary>
    [ReactProp]
    public bool? expanded { get; set; }
    
    /// <summary>
    ///     If `true`, expands the accordion, otherwise collapse it.
    ///     <br/>
    ///     Setting this prop enables control over the accordion.
    /// </summary>
    public static IModifier Expanded(bool? value) => CreateThirdPartyReactComponentModifier<Accordion>(x => x.expanded = value);
    
    /// <summary>
    ///     Callback fired when the expand/collapse state is changed.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     @param {React.SyntheticEvent} event The event source of the callback. **Warning**: This is a generic event not a change event.
    ///     <br/>
    ///     @param {boolean} expanded The `expanded` state of the accordion.
    /// </summary>
    [ReactProp]
    public Func<MouseEvent, bool?, Task> onChange {get;set;}
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic sx { get; } = new ExpandoObject();
    
    /// <summary>
    ///     The component used for the transition.
    ///     <br/>
    ///     [Follow this guide](/material-ui/transitions/#transitioncomponent-prop) to learn more about the requirements for this component.
    ///     <br/>
    ///     @default Collapse
    /// </summary>
    
    protected override Element GetSuspenseFallbackElement()
    {
        return _children?.FirstOrDefault() ?? new ReactWithDotNetSkeleton.Skeleton();
    }
    
    public Accordion(){ }
    
    public Accordion(params Action<Accordion>[] modifiers) => modifiers.ApplyAll(Add);
    
    public Accordion(StyleModifier styleModifier, params Action<Accordion>[] modifiers)
    {
        Add(styleModifier);
        modifiers.ApplyAll(Add);
    }
    
    public void Add(Action<Accordion> modify) => modify?.Invoke(this);
}
