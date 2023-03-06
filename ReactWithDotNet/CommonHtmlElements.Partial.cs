namespace ReactWithDotNet;

partial class label
{
    [React]
    public string htmlFor { get; set; }
}

public sealed class Nbsp : HtmlElement
{
    internal Nbsp()
    {
        
    }
    
    [React]
    public int? length { get; set; }
}

partial class Mixin
{
    /// <summary>
    /// Creates new non-breaking space
    /// <br/>
    /// &amp;nbsp;
    /// </summary>
    public static Nbsp nbsp()
    {
        return new Nbsp();
    }

    /// <summary>
    /// Creates new non-breaking space with given <paramref name="length"/>
    /// <br/>
    /// &amp;nbsp;
    /// </summary>
    public static Nbsp nbsp(int length)
    {
        return new Nbsp { length = length };
    }
}