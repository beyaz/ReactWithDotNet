using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;

class CharacterCountingOptionView : ReactComponent
{
    public bool? ShowKeyborad { get; set; }
    
    public bool? ShowMushafOptions { get; set; }

    void KeyboardClicked(string _)
    {
        if (ShowKeyborad == true)
        {
            ShowKeyborad = null;
        }
        else
        {
            ShowKeyborad = true;
        }

        ShowMushafOptions = null;
    }

    void OnMushafOptionClicked(string _)
    {
        if (ShowMushafOptions == true)
        {
            ShowMushafOptions = null;
        }
        else
        {
            ShowMushafOptions = true;
        }

        ShowKeyborad = null;
    }

    protected override Element render()
    {
        var iconSize = 18;

        var headerColor = "#1976d2";

        return new div
        {
            style = { display = "flex", flexDirection = "column" },
            children =
            {
                // Header
                new div
                {
                    style =
                    {
                        display       = "flex",
                        flexDirection = "row",
                        color         = "rgba(0, 0, 0, 0.6)",
                        textAlign     = "center",
                        cursor        = "pointer"
                    },
                    children =
                    {
                        new HPanel
                        {
                            onClick = KeyboardClicked,
                            children =
                            {
                                new HSpace(20),
                                new img
                                {
                                    src    = "wwwroot/img/Keyboard.svg",
                                    width  = iconSize,
                                    height = iconSize
                                },
                                new div("Arapça Klavye")
                                {
                                    style =
                                    {
                                        padding = "10px",
                                        color   = headerColor,

                                    }
                                },
                            },
                            style = { opacity = ShowKeyborad == true ? "0.2" : null }
                        },

                        new HSpace(10),

                        new HPanel
                        {
                            onClick = OnMushafOptionClicked,
                            children =
                            {
                                new HSpace(10),
                                new img
                                {
                                    src    = "wwwroot/img/Options.svg",
                                    width  = iconSize,
                                    height = iconSize
                                },
                                new div("Mushaf Ayarları")
                                {
                                    style =
                                    {
                                        padding = "10px",
                                        color   = headerColor,

                                    }
                                },
                            },
                            style = { opacity = ShowMushafOptions == true ? "0.2" : null }
                        }

                    }
                },

                // content
                new div
                {
                    style = { paddingLeft = "20px" },
                    children =
                    {
                        BuildContent()
                    }
                }
            }
        };

    }

    Element BuildContent()
    {
        if (ShowKeyborad == true)
        {
            return ArabicKeyboard.Content;
        }

        if (ShowMushafOptions == true)
        {
            return new MushafOptionsView();
        }

        return null;
    }
}