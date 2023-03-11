namespace ReactWithDotNet;

public sealed class FlexRow : HtmlElement
{
    public FlexRow(params IModifier[] modifiers) : this()
    {
        this.Apply(modifiers);
    }

    public FlexRow()
    {
        style.display       = "flex";
        style.flexDirection = "row";
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