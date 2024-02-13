namespace ReactWithDotNet;

sealed class Nbsp : HtmlElement
{
    #region int? length
    PropertyValueNode<int?> _length;
    static readonly PropertyValueDefinition _length_ = new()
    {
        name = nameof(length)
    };
    
    public int? length
    {
        get => _length?.value;
        set => SetValue(_length_, ref _length, value);
    }
    #endregion
}

partial class Mixin
{
    public static HtmlElement br()
    {
        return new br();
    }

    /// <summary>
    ///     Creates new non-breaking space
    ///     <br />
    ///     &amp;nbsp;
    /// </summary>
    public static HtmlElement nbsp()
    {
        return new Nbsp();
    }

    /// <summary>
    ///     Creates new non-breaking space with given <paramref name="length" />
    ///     <br />
    ///     &amp;nbsp;
    /// </summary>
    public static HtmlElement nbsp(int length)
    {
        return new Nbsp { length = length };
    }
}