namespace ReactWithDotNet.PrimeReact;

public class BlockUI : ElementBase
{
        

    /// <summary>
    /// Controls the blocked state.
    /// </summary>
    [React]
    public bool blocked { get; set; }


    [React]
    public Element template { get; set; }
        

}