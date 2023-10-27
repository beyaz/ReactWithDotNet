namespace ReactWithDotNet;



sealed class Nbsp : HtmlElement
{
    [ReactProp]
    public int? length { get; set; }
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