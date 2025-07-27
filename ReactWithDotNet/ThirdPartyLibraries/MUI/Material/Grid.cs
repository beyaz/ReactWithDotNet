// auto generated code (do not edit manually)

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public sealed class Grid : ElementBase
{
    /// <summary>
    /// Defines the justification of items along the horizontal axis.
    /// </summary>
    [ReactProp]
    public string justifyContent { get; set; }

    /// <summary>
    /// Defines the alignment of items along the vertical axis.
    /// </summary>
    [ReactProp]
    public string alignItems { get; set; }
    
    /// <summary>
    /// Defines the alignment of content within the container when there's extra space.
    /// </summary>
    [ReactProp]
    public string alignContent { get; set; }

    /// <summary>
    /// Reverses the direction of the flex items.
    /// </summary>
    [ReactProp]
    public bool? directionReverse { get; set; }
    
    /// <summary>
    /// If true, the item will grow to fill available space.
    /// </summary>
    [ReactProp]
    public bool? zeroMinWidth { get; set; }
    
    /// <summary>
    /// Defines if the component is an item inside a container.
    /// </summary>
    [ReactProp]
    public bool? item { get; set; }
    
    /// <summary>
    /// The number of columns.
    /// </summary>
    [ReactProp]
    public int? columns { get; set; }
    
    /// <summary>
    /// 	Defines the horizontal space between the type item components. It overrides the value of the spacing prop.
    /// </summary>
    [ReactProp]
    public double? columnSpacing { get; set; }
    
    /// <summary>
    /// 	If true, the component will have the flex container behavior. You should be wrapping items with a container.
    /// </summary>
    [ReactProp]
    public bool? container { get; set; }
    
    /// <summary>
    /// 	Defines the flex-direction style property. It is applied for all screen sizes.
    /// </summary>
    [ReactProp]
    public string direction { get; set; }
    
    /// <summary>
    /// 	Defines the offset value for the type item components.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> offset { get; set; }
    
    /// <summary>
    /// 	Defines the vertical space between the type item components. It overrides the value of the spacing prop.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> rowSpacing { get; set; }
    
    /// <summary>
    /// 	Defines the size of the the type item components.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> size { get; set; }
    
    /// <summary>
    /// 	Defines the space between the type item components. It can only be used on a type container component.
    /// </summary>
    [ReactProp]
    public UnionProp<string,double> spacing { get; set; }
    
    
    /// <summary>
    /// 	'nowrap' | 'wrap-reverse' | 'wrap'
    /// </summary>
    [ReactProp]
    public string wrap { get; set; }
    
    
    /// <summary>
    ///     Override or extend the styles applied to the component.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; } = new ();
    
    /// <summary>
    ///     The system prop that allows defining system overrides as well as additional CSS styles.
    /// </summary>
    [ReactProp]
    [ReactTransformValueInServerSide(typeof(DoNotSendToClientWhenEmpty))]
    public dynamic sx { get; } = new ExpandoObject();
}
