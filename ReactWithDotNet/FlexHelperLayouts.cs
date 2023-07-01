namespace ReactWithDotNet;

/// <summary>
///     <br>display = "flex"</br>
///     <br>flexDirection  = "row"</br>
/// </summary>
public sealed class FlexRow : HtmlElement
{
    /// <summary>
    ///     <br>display = "flex"</br>
    ///     <br>flexDirection  = "row"</br>
    /// </summary>
    public FlexRow(params IModifier[] modifiers) : this()
    {
        this.Apply(modifiers);
    }

    /// <summary>
    ///     <br>display = "flex"</br>
    ///     <br>flexDirection  = "row"</br>
    /// </summary>
    public FlexRow()
    {
        style.display       = "flex";
        style.flexDirection = "row";
    }

    public override string Type => nameof(div);
}

/// <summary>
///     display = "flex"
///     <br/>
///     flexDirection  = "column"
/// </summary>
public sealed class FlexColumn : HtmlElement
{
    /// <summary>
    ///     display = "flex"
    ///     <br/>
    ///     flexDirection  = "column"
    /// </summary>
    public FlexColumn(params IModifier[] modifiers) : this()
    {
        this.Apply(modifiers);
    }

    /// <summary>
    ///     display = "flex"
    ///     <br/>
    ///     flexDirection  = "column"
    /// </summary>
    public FlexColumn()
    {
        style.display       = "flex";
        style.flexDirection = "column";
    }

    public override string Type => nameof(div);
}

/// <summary>
///     <br>display = "flex"</br>
///     <br>flexDirection  = "row"</br>
///     <br>justifyContent = "center"</br>
///     <br>alignItems     = "center"</br>
/// </summary>
public sealed class FlexRowCentered : HtmlElement
{
    /// <summary>
    ///     <br>display = "flex"</br>
    ///     <br>flexDirection  = "row"</br>
    ///     <br>justifyContent = "center"</br>
    ///     <br>alignItems     = "center"</br>
    /// </summary>
    public FlexRowCentered()
    {
        style.display        = "flex";
        style.flexDirection  = "row";
        style.justifyContent = "center";
        style.alignItems     = "center";
    }

    /// <summary>
    ///     <br>display = "flex"</br>
    ///     <br>flexDirection  = "row"</br>
    ///     <br>justifyContent = "center"</br>
    ///     <br>alignItems     = "center"</br>
    /// </summary>
    public FlexRowCentered(params IModifier[] modifiers) : this()
    {
        this.Apply(modifiers);
    }

    public override string Type => nameof(div);

   
}

partial class Mixin
{
    /// <summary>
    ///     display = "flex"
    ///     <br/>
    ///     flexDirection  = "column"
    /// </summary>
    public static StyleModifier DisplayFlexColumn => CreateStyleModifier(style =>
    {
        style.display       = "flex";
        style.flexDirection = "column";
    });
    
    /// <summary>
    ///     <br>display = "flex"</br>
    ///     <br>flexDirection  = "row"</br>
    /// </summary>
    public static StyleModifier DisplayFlexRow=> CreateStyleModifier(style =>
    {
        style.display        = "flex";
        style.flexDirection  = "row";
    });
    
    
    /// <summary>
    ///     <br>display = "flex"</br>
    ///     <br>flexDirection  = "row"</br>
    ///     <br>justifyContent = "center"</br>
    ///     <br>alignItems     = "center"</br>
    /// </summary>
    public static StyleModifier DisplayFlexRowCentered => CreateStyleModifier(style =>
    {
        style.display        = "flex";
        style.flexDirection  = "row";
        style.justifyContent = "center";
        style.alignItems     = "center";
    });
}


partial class Mixin
{
    /// <summary>
    ///     Shorthand property for assign Flex properties
    /// </summary>
    public static StyleModifier Flex(int flexGrow, int flexShrink, CssUnit flexBasis)
    {
        return new StyleModifier(style =>
        {
            style.flexGrow   = flexGrow.ToString();
            style.flexShrink = flexShrink.ToString();
            style.flexBasis  = flexBasis.ToString();
        });
    }
}