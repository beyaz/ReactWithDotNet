// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Typography : ElementBase
{
    [ReactProp]
    public MouseEventHandler onClick{ get; set; }
    
    [ReactProp]
    public dangerouslySetInnerHTML dangerouslySetInnerHTML{ get; set; }
        
    /// <summary>
    /// Defines the justification of items along the horizontal axis.
    /// </summary>
    [ReactProp]
    public string id { get; set; }
    
    
    /// <summary>
    ///     Set the text-align on the component.
    ///     <br/>
    ///     @default 'inherit'
    /// </summary>
    [ReactProp]
    public string align { get; set; }
    
    /// <summary>
    ///     Set the text-align on the component.
    ///     <br/>
    ///     @default 'inherit'
    /// </summary>
    public static Modifier Align(string value) => CreateThirdPartyReactComponentModifier<Typography>(x => x.align = value);
    
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     The color of the component.
    ///     <br/>
    ///     It supports both default and custom theme colors, which can be added as shown in the
    ///     <br/>
    ///     [palette customization guide](https://mui.com/material-ui/customization/palette/#custom-colors).
    /// </summary>
    [ReactProp]
    public string color { get; set; }
    
    /// <summary>
    ///     The color of the component.
    ///     <br/>
    ///     It supports both default and custom theme colors, which can be added as shown in the
    ///     <br/>
    ///     [palette customization guide](https://mui.com/material-ui/customization/palette/#custom-colors).
    /// </summary>
    public static Modifier Color(string value) => CreateThirdPartyReactComponentModifier<Typography>(x => x.color = value);
    
    /// <summary>
    ///     // to work with v5 color prop type which allows any string
    /// </summary>
    [ReactProp]
    public bool? gutterBottom { get; set; }
    
    /// <summary>
    ///     // to work with v5 color prop type which allows any string
    /// </summary>
    public static Modifier GutterBottom(bool? value) => CreateThirdPartyReactComponentModifier<Typography>(x => x.gutterBottom = value);
    
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
    public static Modifier NoWrap(bool? value) => CreateThirdPartyReactComponentModifier<Typography>(x => x.noWrap = value);
    
    /// <summary>
    ///     If `true`, the element will be a paragraph element.
    ///     <br/>
    ///     @default false
    ///     <br/>
    ///     @deprecated Use the `component` prop instead. This prop will be removed in v7. See [Migrating from deprecated APIs](https://mui.com/material-ui/migration/migrating-from-deprecated-apis/) for more details.
    /// </summary>
    [ReactProp]
    public bool? paragraph { get; set; }
    
    /// <summary>
    ///     If `true`, the element will be a paragraph element.
    ///     <br/>
    ///     @default false
    ///     <br/>
    ///     @deprecated Use the `component` prop instead. This prop will be removed in v7. See [Migrating from deprecated APIs](https://mui.com/material-ui/migration/migrating-from-deprecated-apis/) for more details.
    /// </summary>
    public static Modifier Paragraph(bool? value) => CreateThirdPartyReactComponentModifier<Typography>(x => x.paragraph = value);
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic sx { get; } = new ExpandoObject();
    
    /// <summary>
    ///     Applies the theme typography styles.
    ///     <br/>
    ///     @default 'body1'
    /// </summary>
    [ReactProp]
    public string variant { get; set; }
    
    /// <summary>
    ///     Applies the theme typography styles.
    ///     <br/>
    ///     @default 'body1'
    /// </summary>
    public static Modifier Variant(string value) => CreateThirdPartyReactComponentModifier<Typography>(x => x.variant = value);
    
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
    public string component { get; set; }
}
