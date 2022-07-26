using System;
using System.Net.Mime;
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

class LargeTitle : ReactComponent
{
    readonly string text;

    public LargeTitle(string text)
    {
        this.text = text;
    }
    
    public override Element render()
    {
        return new div
        {
            style    = { fontSize = "18px", textAlign = "center", fontWeight = "500"},
            text = text
        };
    }
}


class Important : ReactComponent
{
    readonly string text;

    public Important(string text)
    {
        this.text = text;
    }

    public override Element render()
    {
        return new div
        {
            style = { fontWeight = "500" },
            text  = text
        };
    }
}


class Title : div
{
    public Title(string innerText)
    {
        this.innerText = innerText;

        style.fontSize  = "18px";
        style.textAlign = "center";
    }
}

class SubTitle : div
{
    public SubTitle(string innerText)
    {
        this.innerText = innerText;

        style.fontSize  = "16px";
        style.textAlign = "center";
    }
}

class Article : ReactComponent
{
    public override Element render()
    {
        return new article
        {
            style = { marginLeftRight = "8px", paddingLeftRight = "16px"},
            Children = children
        };
    }
}

class Backdrop: ReactComponent
{
    public Action<string> onClick { get; set; }
    
    public override Element render()
    {
        return new div
        {
            onClick = onClick,
            style =
            {
                position = "absolute",
                top="0",
                left="0",
                zIndex = "1040",
                width = "100vw",
                height = "100vh",
                background = "#f6fbff",
                opacity = "0.3"
            },
            Children = children
        };
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