namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;

public class BlockUI : ElementBase
{
        

    /// <summary>
    /// Controls the blocked state.
    /// </summary>
    [ReactProp]
    public bool blocked { get; set; }


    [ReactProp]
    public Element template { get; set; }
        

}