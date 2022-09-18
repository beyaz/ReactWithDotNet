using QuranAnalyzer.WebUI.Components;

namespace QuranAnalyzer.WebUI.Pages.WordSearchingPage;

class CharacterCountingOptionState
{
    public bool? ShowKeyborad { get; set; }

    public bool? ShowMushafOptions { get; set; }
}

class CharacterCountingOptionView : ReactComponent<CharacterCountingOptionState>
{
    void KeyboardClicked(MouseEvent e)
    {
        if (state.ShowKeyborad == true)
        {
            state.ShowKeyborad = null;
        }
        else
        {
            state.ShowKeyborad = true;
        }

        state.ShowMushafOptions = null;
    }

    void OnMushafOptionClicked(MouseEvent e)
    {
        if (state.ShowMushafOptions == true)
        {
            state.ShowMushafOptions = null;
        }
        else
        {
            state.ShowMushafOptions = true;
        }

        state.ShowKeyborad = null;
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
                            style = { opacity = state.ShowKeyborad == true ? "0.2" : null }
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
                            style = { opacity = state.ShowMushafOptions == true ? "0.2" : null }
                        }
                    }
                },

                // content
                new div
                {
                    style = { paddingLeft = "20px" },
                    children =
                    {
                        new div
                        {
                            style    = { display = state.ShowKeyborad == true ? null : "none" },
                            children = { new ArabicKeyboard() }
                        }
                    }
                }
            }
        };
    }
}