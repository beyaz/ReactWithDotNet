namespace ReactWithDotNet.WebSite.Components;

class GetStartedButton : PureComponent
{
    protected override Element render()
    {
        return new div(CursorDefault)
        {
            text = "Get Started",
            style =
            {
                backgroundImage = "linear-gradient(to right, #DA22FF 0%, #9733EE  51%, #DA22FF  100%)",
                margin          = "10px",
                padding         = "15px 45px",
                textAlign       = "center",
                textTransform   = "uppercase",
                transition      = "0.5s",
                backgroundSize  = "200% auto",
                color           = "white",
                boxShadow       = "0 0 20px #eee",
                borderRadius    = "10px",
                display         = "block",
                
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