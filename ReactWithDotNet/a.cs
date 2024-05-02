namespace ReactWithDotNet;

partial class Mixin
{
   

    /// <summary>
    ///     a.target = '_blank'
    /// </summary>
    public static HtmlElementModifier TargetBlank => Target("_blank");
    
    
    /// <summary>
    ///     a.href = <paramref name="href" />
    /// </summary>
    public static HtmlElementModifier Href(string href) => CreateHtmlElementModifier<HtmlElement>(element =>
    {
        if (element is null)
        {
            return;
        }
        
        if (element is a aElement)
        {
            aElement.href = href;
        }
        else if (element is link linkElement)
        {
            linkElement.href = href;
        }
        else
        {
            throw DeveloperException($"Href not calculated.{element.GetType().FullName}");    
        }
        
    });

    /// <summary>
    ///     a.target = <paramref name="target" />
    /// </summary>
    public static HtmlElementModifier Target(string target) => CreateHtmlElementModifier<a>(element => element.target = target);
    
    /// <summary>
    ///     a.href = <paramref name="value" />;
    ///     <br/>
    ///     <br/>
    ///     Specifies the relationship between the current document and the linked document.
    ///     <br />
    ///     Only used if the href attribute is present.
    ///     <br />
    ///     Tip: Search engines can use this attribute to get more information about a link!
    /// </summary>
    public static HtmlElementModifier Rel(string value) => CreateHtmlElementModifier<HtmlElement>(element =>
    {
        if (element is null)
        {
            return;
        }
        
        if (element is a aElement)
        {
            aElement.rel = value;
        }
        else if (element is link linkElement)
        {
            linkElement.rel = value;
        }
        else
        {
            throw DeveloperException($"rel not calculated.{element.GetType().FullName}");    
        }       
    });
}