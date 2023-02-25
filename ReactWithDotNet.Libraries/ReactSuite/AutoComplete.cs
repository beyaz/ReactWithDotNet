namespace ReactWithDotNet.Libraries.ReactSuite;

public sealed class AutoComplete : ElementBase
{
    [React]
    public IEnumerable<string> data { get; set; }

    [React]
    public string id { get; set; }

    [React]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + nameof(AutoComplete) + "::OnChange")]
    public Action<string> onChange { get; set; }

    [React]
    public string value { get; set; }

    [React]
    public string placeholder { get; set; }
}