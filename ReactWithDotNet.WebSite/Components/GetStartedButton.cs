namespace ReactWithDotNet.WebSite.Components;

sealed class GetStartedButton : PureComponent
{
    public string Text { get; init; }
    
    public string Href { get; init; }
    
    protected override Element render()
    {
        return new a(Href(Href), TextDecorationNone, CursorDefault)
        {
            text = Text,
            style =
            {
                backgroundImage = "linear-gradient(to right, #DA22FF 0%, #9733EE  51%, #DA22FF  100%)",
                padding         = "15px 45px",
                textAlign       = "center",
                transition      = "0.5s",
                backgroundSize  = "200% auto",
                color           = "white",
                boxShadow       = "0 0 20px #eee",
                borderRadius    = "10px",
                
                hover =
                {
                    backgroundPosition = "right center",
                    color              = "#fff",
                    textDecoration     = "none"
                }
            }
        };
    }
}