using ReactWithDotNet;

namespace QuranAnalyzer.WebUI.Components;



class divWithBorder: div
{
    public divWithBorder()
    {
        style.border       = "1px solid rgb(218, 220, 224)";
        style.borderRadius = "5px";
    }
}

class SiteTitle : div
{
    public SiteTitle(string innerText)
    {
        this.innerText = innerText;
        
        style.fontSize = "20px";
    }
}

class LargeTitle : div
{
    public LargeTitle(string innerText)
    {
        this.innerText = innerText;

        style.fontSize = "20px";
    }
}

class Title : div
{
    public Title(string innerText)
    {
        this.innerText = innerText;

        style.fontSize = "18px";
    }
}

class SubTitle : div
{
    public SubTitle(string innerText)
    {
        this.innerText = innerText;

        style.fontSize = "16px";
    }
}

class MainPage : ReactComponent
{
   public Element topContent;
   public Element mainContent;
   public Element menu;
   public double mainDivScrollY;

    public override Element render()
    {
        var top = new div
        {
            style=
            {
                position = "fixed",
                top      = "0px",
                left     = "0px",

                width        = "100%",
                height       = "50px",
                zIndex       = "1",
                borderBottom = "1px solid #dadce0"
            },
            children = { topContent }
        } ;

        if (mainDivScrollY > 0)
        {
            top.style.borderBottom = "";
            top.style.boxShadow    = "0 1px 2px hsla(0,0%,0%,0.05),0 1px 4px hsla(0,0%,0%,0.05),0 2px 8px hsla(0,0%,0%,0.05)";
        }

        var main = new div
        {
            id = "main",
            children =
            {
                new div
                {
                    style = { display = "flex", justifyContent = "center" },
                    children =
                    {
                        new div
                        {
                            style    = { marginLeftRight = "10px", marginTop = "10px", maxWidth = "800px" },
                            children = { mainContent }
                        }
                    }
                }
            },
            
            style =
            {
                position     = "fixed",
                top          = "0px",
                left         = "0px",
                marginTop    = "50px",
                marginBottom = "27px",

                width     = "100%",
                height    = "calc(100% - 65px)",
                overflowY = "auto"
            }
        };

        return new div { children ={ top, menu, main }, style = { height = "100vh", width = "100%" } };
    }
}