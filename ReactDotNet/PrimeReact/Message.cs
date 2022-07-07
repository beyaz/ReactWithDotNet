using System;

namespace ReactDotNet.Html5.PrimeReact;

[Serializable]
public class Message : ElementBase
{
    /// <summary>
    ///     Text of the message.
    /// </summary>
    [React]
    public new string text { get; set; }


    /// <summary>
    /// Severity level of the message.
    /// </summary>
    [React]
    public string severity { get; set; }
}