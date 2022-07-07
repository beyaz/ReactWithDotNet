using ReactDotNet.Html5;
using static ReactDotNet.Mixin;

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
    public SiteTitle(string text)
    {
        this.text = text;
        
        style.fontSize = "22px";
    }
}

class LargeTitle : div
{
    public LargeTitle(string text)
    {
        this.text = text;

        style.fontSize = "20px";
    }
}

class Title : div
{
    public Title(string text)
    {
        this.text = text;

        style.fontSize = "18px";
    }
}

class SubTitle : div
{
    public SubTitle(string text)
    {
        this.text = text;

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
        var top = new div { topContent } + new Style
        {
            position = Position.@fixed,
            top      = px(0),
            left     = px(0),

            width        = "100%",
            height       = px(50),
            zIndex       = "1",
            borderBottom = "1px solid #dadce0"
        };

        if (mainDivScrollY > 0)
        {
            top += new Style
            {
                borderBottom = "",
                boxShadow    = "0 1px 2px hsla(0,0%,0%,0.05),0 1px 4px hsla(0,0%,0%,0.05),0 2px 8px hsla(0,0%,0%,0.05)"
            };
        }

        var main = new div
        {
            id = "main",
            children =
            {
                new div
                {
                    style = { display = Display.flex, justifyContent = JustifyContent.center },
                    children =
                    {
                        new div
                        {
                            style    = { marginLeftRight = px(10), marginTop = px(10), maxWidth = px(800) },
                            children = { mainContent }
                        }
                    }
                }
            },
            
            style =
            {
                position     = Position.@fixed,
                top          = px(0),
                left         = px(0),
                marginTop    = px(50),
                marginBottom = px(27),

                width     = "100%",
                height    = "calc(100% - 65px)",
                overflowY = "auto"
            }
        };

        return new div { top, menu, main } + new Style { height = "100vh", width = "100%", backgroundColor = "rgb(245, 245, 245)" };
    }
}