namespace ReactWithDotNet;

partial class label
{
    [React]
    public string htmlFor { get; set; }
}

public sealed class nbsp : HtmlElement
{
    internal nbsp()
    {
        
    }
    
    [React]
    public int length { get; set; }
}
partial class Mixin
{
    /// <summary>
    /// Creates new non-breaking space
    /// <br/>
    /// &amp;nbsp;
    /// </summary>
    public static nbsp nbsp()
    {
        return new nbsp();
    }

    /// <summary>
    /// Creates new non-breaking space with given <paramref name="length"/>
    /// <br/>
    /// &amp;nbsp;
    /// </summary>
    public static nbsp nbsp(int length)
    {
        return new nbsp { length = length };
    }
}