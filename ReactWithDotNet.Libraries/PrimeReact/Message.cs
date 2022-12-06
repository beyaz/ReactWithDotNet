namespace ReactWithDotNet.Libraries.PrimeReact;

[Serializable]
public class Message : ElementBase
{
    /// <summary>
    ///     Text of the message.
    /// </summary>
    [React]
    public  string text { get; set; }


    /// <summary>
    /// Severity level of the message.
    /// </summary>
    [React]
    public string severity { get; set; }
}