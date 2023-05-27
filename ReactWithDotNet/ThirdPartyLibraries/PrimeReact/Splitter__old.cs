namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;




public class SplitterPanel : ElementBase
{
    [ReactProp]
    public int? size { get; set; }

    [ReactProp]
    public int? minSize { get; set; }

    public SplitterPanel()
    {
        
    }

    public SplitterPanel(params StyleModifier[] modifiers) : base(modifiers) { }
}




