namespace QuranAnalyzer.WebUI.Pages.PageCharacterCounting;

class CharacterCountingOptionState
{
    public bool? ShowKeyborad { get; set; }

    public bool? ShowMushafOptions { get; set; }
}

class CharacterCountingOptionView : ReactComponent<CharacterCountingOptionState>
{
    public MushafOption MushafOption { get; set; } = new();
    
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
        var iconSize = 23;

        var headerColor = "#1976d2";

        return new div(Role(nameof(CharacterCountingOptionView)))
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
                                    src    = FileAtImgFolder("Keyboard.svg"),
                                    width  = iconSize,
                                    height = iconSize
                                },
                                new div(Text("Arapça Klavye"))
                                {
                                    style =
                                    {
                                        Padding(10),
                                        Color(headerColor),
                                        Hover(Color("rgb(40 15 229)"))
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
                                    src = FileAtImgFolder("Options.svg"), 
                                    width = iconSize,
                                    height = iconSize
                                },
                                new div(Text("Mushaf Ayarları"))
                                {
                                    style =
                                    {
                                        Padding(10),
                                        Color(headerColor),
                                        Hover(Color("rgb(40 15 229)"))
                                    }
                                }
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
                        },
                        new div
                        {
                            style    = { display = state.ShowMushafOptions == true ? null : "none" },
                            children = { new MushafOptionsView{ Model = MushafOption, MushafOptionChanged = OnMushafOptionChanged } }
                        }
                    }
                }
            }
        };
    }

    void OnMushafOptionChanged(MushafOption mushafOption)
    {
        MushafOption = mushafOption;
        DispatchEvent(() => MushafOptionChanged, mushafOption);
    }

    [ReactCustomEvent]
    public Action<MushafOption> MushafOptionChanged { get; set; }
}