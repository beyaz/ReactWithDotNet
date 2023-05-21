// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Typography : ElementBase
{
    /// <summary>
    ///     Set the text-align on the component.
    ///     <br/>
    ///     @default 'inherit'
    /// </summary>
    [ReactProp]
    public string align { get; set; }
    
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     If `true`, the text will have a bottom margin.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? gutterBottom { get; set; }
    
    /// <summary>
    ///     If `true`, the text will not wrap, but instead will truncate with a text overflow ellipsis.
    ///     <br/>
    ///     <br/>
    ///     <br/>
    ///     Note that text overflow can only happen with block or inline-block level elements
    ///     <br/>
    ///     (the element needs to have a width in order to overflow).
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? noWrap { get; set; }
    
    /// <summary>
    ///     If `true`, the element will be a paragraph element.
    ///     <br/>
    ///     @default false
    /// </summary>
    [ReactProp]
    public bool? paragraph { get; set; }
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic sx { get; } = new ExpandoObject();
    
    /// <summary>
    ///     Applies the theme typography styles.
    ///     <br/>
    ///     @default 'body1'
    /// </summary>
    [ReactProp]
    public string variant { get; set; }
    
    /// <summary>
    ///     The component maps the variant prop to a range of different HTML element types.
    ///     <br/>
    ///     For instance, subtitle1 to `&lt;h6&gt;`.
    ///     <br/>
    ///     If you wish to change that mapping, you can provide your own.
    ///     <br/>
    ///     Alternatively, you can use the `component` prop.
    ///     <br/>
    ///     @default {
    ///     <br/>
    ///     h1: 'h1',
    ///     <br/>
    ///     h2: 'h2',
    ///     <br/>
    ///     h3: 'h3',
    ///     <br/>
    ///     h4: 'h4',
    ///     <br/>
    ///     h5: 'h5',
    ///     <br/>
    ///     h6: 'h6',
    ///     <br/>
    ///     subtitle1: 'h6',
    ///     <br/>
    ///     subtitle2: 'h6',
    ///     <br/>
    ///     body1: 'p',
    ///     <br/>
    ///     body2: 'p',
    ///     <br/>
    ///     inherit: 'p',
    ///     <br/>
    ///     }
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic variantMapping { get; } = new ExpandoObject();
    
    [ReactProp]
    public string color { get; set; }
    
    [ReactProp]
    public string component { get; set; }
}
