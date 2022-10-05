namespace ReactWithDotNet;

public sealed class HStack : HtmlElement
{
    public HStack()
    {
        style.display       = "flex";
        style.flexDirection = "row";
        style.alignItems    = "stretch";
        style.width         = "100%";
    }

    public override string Type => nameof(div);
}

public sealed class HPanel : HtmlElement
{
    public HPanel()
    {
        style.display       = "flex";
        style.flexDirection = "row";
        style.alignItems    = "center";
    }

    public override string Type => nameof(div);
}

public sealed class VPanel : HtmlElement
{
    public VPanel()
    {
        style.display       = "flex";
        style.flexDirection = "column";
        style.alignItems    = "center";
    }

    public override string Type => nameof(div);
}

public sealed class VStack : HtmlElement
{
    public VStack()
    {
        style.display       = "flex";
        style.flexDirection = "column";
        style.alignItems    = "stretch";
        style.height        = "100%";
    }

    public override string Type => nameof(div);
}

public sealed class divVerticalCentered : HtmlElement
{
    public divVerticalCentered(int gapAsPixel = 10)
    {
        style.width_height   = "100%";
        style.display        = "flex";
        style.flexDirection  = "column";
        style.justifyContent = "center";
        style.alignItems     = "center";
        style.textAlign      = "center";
        style.gap            = gapAsPixel + "px";
    }

    public override string Type => nameof(div);
}

public sealed class divHorizontalCentered : HtmlElement
{
    public divHorizontalCentered(int gapAsPixel = 10)
    {
        style.width_height   = "100%";
        style.display        = "flex";
        style.flexDirection  = "row";
        style.justifyContent = "center";
        style.alignItems     = "center";
        style.textAlign      = "center";
        style.gap            = gapAsPixel + "px";
    }

    public override string Type => nameof(div);
}

public sealed class FlexRow : HtmlElement
{
    public FlexRow(params IModifier[] modifiers):this()
    {
        this.Apply(modifiers);
    }

    public FlexRow()
    {
        style.display       = "flex";
        style.flexDirection = "row";
    }

    public FlexRow(IEnumerable<Element> children):this()
    {
        Children = children;
    }

    public override string Type => nameof(div);
}

public sealed class FlexColumn : HtmlElement
{
    public FlexColumn(params IModifier[] modifiers) : this()
    {
       

        this.Apply(modifiers);
    }

    public FlexColumn()
    {
        style.display       = "flex";
        style.flexDirection = "column";
    }

    public FlexColumn(IEnumerable<Element> children) : this()
    {
        Children = children;
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