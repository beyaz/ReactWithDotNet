namespace ReactWithDotNet;

/// <summary>
///     display = "flex"
///     <br />
///     flexDirection  = "row"
/// </summary>
public sealed class FlexRow : HtmlElement
{
    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "row"
    /// </summary>
    public FlexRow(params Modifier[] modifiers) : this()
    {
        this.Apply(modifiers);
    }

    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "row"
    /// </summary>
    public FlexRow()
    {
        style.display       = "flex";
        style.flexDirection = "row";
    }

    public override string __type__ => nameof(div);
}

/// <summary>
///     display = "inline-flex"
///     <br />
///     flexDirection  = "row"
/// </summary>
public sealed class InlineFlexRow : HtmlElement
{
    /// <summary>
    ///     display = "inline-flex"
    ///     <br />
    ///     flexDirection  = "row"
    /// </summary>
    public InlineFlexRow(params Modifier[] modifiers) : this()
    {
        this.Apply(modifiers);
    }

    /// <summary>
    ///     display = "inline-flex"
    ///     <br />
    ///     flexDirection  = "row"
    /// </summary>
    public InlineFlexRow()
    {
        style.display       = "inline-flex";
        style.flexDirection = "row";
    }

    public override string __type__ => nameof(div);
}

/// <summary>
///     display = "flex"
///     <br />
///     flexDirection  = "column"
/// </summary>
public sealed class FlexColumn : HtmlElement
{
    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "column"
    /// </summary>
    public FlexColumn(params Modifier[] modifiers) : this()
    {
        this.Apply(modifiers);
    }

    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "column"
    /// </summary>
    public FlexColumn()
    {
        style.display       = "flex";
        style.flexDirection = "column";
    }

    public override string __type__ => nameof(div);
}

/// <summary>
///     display = "inline-flex"
///     <br />
///     flexDirection  = "column"
/// </summary>
public sealed class InlineFlexColumn : HtmlElement
{
    /// <summary>
    ///     display = "inline-flex"
    ///     <br />
    ///     flexDirection  = "column"
    /// </summary>
    public InlineFlexColumn(params Modifier[] modifiers) : this()
    {
        this.Apply(modifiers);
    }

    /// <summary>
    ///     display = "inline-flex"
    ///     <br />
    ///     flexDirection  = "inline-column"
    /// </summary>
    public InlineFlexColumn()
    {
        style.display       = "inline-flex";
        style.flexDirection = "column";
    }

    public override string __type__ => nameof(div);
}

/// <summary>
///     display = "flex"
///     <br />
///     flexDirection  = "row"
///     <br />
///     justifyContent = "center"
///     <br />
///     alignItems     = "center"
/// </summary>
public sealed class FlexRowCentered : HtmlElement
{
    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "row"
    ///     <br />
    ///     justifyContent = "center"
    ///     <br />
    ///     alignItems     = "center"
    /// </summary>
    public FlexRowCentered()
    {
        style.display        = "flex";
        style.flexDirection  = "row";
        style.justifyContent = "center";
        style.alignItems     = "center";
    }

    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "row"
    ///     <br />
    ///     justifyContent = "center"
    ///     <br />
    ///     alignItems     = "center"
    /// </summary>
    public FlexRowCentered(params Modifier[] modifiers) : this()
    {
        this.Apply(modifiers);
    }

    public override string __type__ => nameof(div);
}

/// <summary>
///     display = "inline-flex"
///     <br />
///     flexDirection  = "row"
///     <br />
///     justifyContent = "center"
///     <br />
///     alignItems     = "center"
/// </summary>
public sealed class InlineFlexRowCentered : HtmlElement
{
    /// <summary>
    ///     display = "inline-flex"
    ///     <br />
    ///     flexDirection  = "row"
    ///     <br />
    ///     justifyContent = "center"
    ///     <br />
    ///     alignItems     = "center"
    /// </summary>
    public InlineFlexRowCentered()
    {
        style.display        = "inline-flex";
        style.flexDirection  = "row";
        style.justifyContent = "center";
        style.alignItems     = "center";
    }

    /// <summary>
    ///     display = "inline-flex"
    ///     <br />
    ///     flexDirection  = "row"
    ///     <br />
    ///     justifyContent = "center"
    ///     <br />
    ///     alignItems     = "center"
    /// </summary>
    public InlineFlexRowCentered(params Modifier[] modifiers) : this()
    {
        this.Apply(modifiers);
    }

    public override string __type__ => nameof(div);
}

/// <summary>
///     display = "flex"
///     <br />
///     flexDirection  = "column"
///     <br />
///     justifyContent = "center"
///     <br />
///     alignItems     = "center"
/// </summary>
public sealed class FlexColumnCentered : HtmlElement
{
    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "column"
    ///     <br />
    ///     justifyContent = "center"
    ///     <br />
    ///     alignItems     = "center"
    /// </summary>
    public FlexColumnCentered()
    {
        style.display        = "flex";
        style.flexDirection  = "column";
        style.justifyContent = "center";
        style.alignItems     = "center";
    }

    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "column"
    ///     <br />
    ///     justifyContent = "center"
    ///     <br />
    ///     alignItems     = "center"
    /// </summary>
    public FlexColumnCentered(params Modifier[] modifiers) : this()
    {
        this.Apply(modifiers);
    }

    public override string __type__ => nameof(div);
}

/// <summary>
///     display = "inline-flex"
///     <br />
///     flexDirection  = "column"
///     <br />
///     justifyContent = "center"
///     <br />
///     alignItems     = "center"
/// </summary>
public sealed class InlineFlexColumnCentered : HtmlElement
{
    /// <summary>
    ///     display = "inline-flex"
    ///     <br />
    ///     flexDirection  = "column"
    ///     <br />
    ///     justifyContent = "center"
    ///     <br />
    ///     alignItems     = "center"
    /// </summary>
    public InlineFlexColumnCentered()
    {
        style.display        = "inline-flex";
        style.flexDirection  = "column";
        style.justifyContent = "center";
        style.alignItems     = "center";
    }

    /// <summary>
    ///     display = "inline-flex"
    ///     <br />
    ///     flexDirection  = "column"
    ///     <br />
    ///     justifyContent = "center"
    ///     <br />
    ///     alignItems     = "center"
    /// </summary>
    public InlineFlexColumnCentered(params Modifier[] modifiers) : this()
    {
        this.Apply(modifiers);
    }

    public override string __type__ => nameof(div);
}

partial class Mixin
{
    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "column"
    /// </summary>
    public static StyleModifier DisplayFlexColumn => CreateStyleModifier(style =>
    {
        style.display       = "flex";
        style.flexDirection = "column";
    });
    
    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "column-reverse"
    /// </summary>
    public static StyleModifier DisplayFlexColumnReverse => CreateStyleModifier(style =>
    {
        style.display       = "flex";
        style.flexDirection = "column-reverse";
    });

    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "column"
    ///     <br />
    ///     justifyContent = "center"
    ///     <br />
    ///     alignItems     = "center"
    /// </summary>
    public static StyleModifier DisplayFlexColumnCentered => CreateStyleModifier(style =>
    {
        style.display        = "flex";
        style.flexDirection  = "column";
        style.justifyContent = "center";
        style.alignItems     = "center";
    });

    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "row"
    /// </summary>
    public static StyleModifier DisplayFlexRow => CreateStyleModifier(style =>
    {
        style.display       = "flex";
        style.flexDirection = "row";
    });
    
    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "row-reverse"
    /// </summary>
    public static StyleModifier DisplayFlexRowReverse => CreateStyleModifier(style =>
    {
        style.display       = "flex";
        style.flexDirection = "row-reverse";
    });

    /// <summary>
    ///     display = "flex"
    ///     <br />
    ///     flexDirection  = "row"
    ///     <br />
    ///     justifyContent = "center"
    ///     <br />
    ///     alignItems     = "center"
    /// </summary>
    public static StyleModifier DisplayFlexRowCentered => CreateStyleModifier(style =>
    {
        style.display        = "flex";
        style.flexDirection  = "row";
        style.justifyContent = "center";
        style.alignItems     = "center";
    });

    /// <summary>
    ///     display = "inline-flex"
    ///     <br />
    ///     flexDirection  = "column"
    /// </summary>
    public static StyleModifier DisplayInlineFlexColumn => CreateStyleModifier(style =>
    {
        style.display       = "inline-flex";
        style.flexDirection = "column";
    });

    /// <summary>
    ///     display = "inline-flex"
    ///     <br />
    ///     flexDirection  = "column"
    ///     <br />
    ///     justifyContent = "center"
    ///     <br />
    ///     alignItems     = "center"
    /// </summary>
    public static StyleModifier DisplayInlineFlexColumnCentered => CreateStyleModifier(style =>
    {
        style.display        = "inline-flex";
        style.flexDirection  = "column";
        style.justifyContent = "center";
        style.alignItems     = "center";
    });

    /// <summary>
    ///     display = "inline-flex"
    ///     <br />
    ///     flexDirection  = "row"
    /// </summary>
    public static StyleModifier DisplayInlineFlexRow => CreateStyleModifier(style =>
    {
        style.display       = "inline-flex";
        style.flexDirection = "row";
    });

    /// <summary>
    ///     display = "inline-flex"
    ///     <br />
    ///     flexDirection  = "row"
    ///     <br />
    ///     justifyContent = "center"
    ///     <br />
    ///     alignItems     = "center"
    /// </summary>
    public static StyleModifier DisplayInlineFlexRowCentered => CreateStyleModifier(style =>
    {
        style.display        = "inline-flex";
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