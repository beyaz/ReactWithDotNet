namespace ReactWithDotNet;



partial class Mixin
{
    /// <summary>
    ///     img.alt = <paramref name="alt" />
    /// </summary>
    public static HtmlElementModifier Alt(string alt) => CreateHtmlElementModifier<img>(element => element.alt = alt);

    /// <summary>
    ///     img.src = <paramref name="src" />
    /// </summary>
    public static HtmlElementModifier Src(string src) => CreateHtmlElementModifier<img>(element => element.src = src);


    /// <summary>
    ///     img.loading = 'lazy'
    /// </summary>
    public static HtmlElementModifier LoadingLazy => img.Loading("lazy");
    
    /// <summary>
    ///     img.loading = 'eager'
    /// </summary>
    public static HtmlElementModifier LoadingEager => img.Loading("eager");
}