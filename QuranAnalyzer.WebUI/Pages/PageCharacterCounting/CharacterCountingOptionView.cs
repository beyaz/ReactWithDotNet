namespace QuranAnalyzer.WebUI.Pages.PageCharacterCounting;

class CharacterCountingOptionState
{
    public bool? ShowKeyborad { get; set; }

    public bool? ShowMushafOptions { get; set; }
}

class CharacterCountingOptionView : ReactComponent<CharacterCountingOptionState>
{
    public MushafOption MushafOption { get; set; } = new();

    [ReactCustomEvent]
    public Action<MushafOption> MushafOptionChanged { get; set; }

    protected override Element render()
    {
        var iconSize = 23;

        var headerColor = "#1976d2";

        return new FlexColumn(Role(nameof(CharacterCountingOptionView)))
        {
            // Header
            new FlexRow(Color("rgba(0, 0, 0, 0.6)"), TextAlignCenter, CursorPointer, Gap(10))
            {
                new FlexRowCentered
                {
                    onClick = KeyboardClicked,
                    children =
                    {
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

                new FlexRowCentered
                {
                    onClick = OnMushafOptionClicked,
                    children =
                    {
                        new img
                        {
                            src    = FileAtImgFolder("Options.svg"),
                            width  = iconSize,
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
            },

            // content
            new div(PaddingLeft(20))
            {
                new div
                {
                    style    = { display = state.ShowKeyborad == true ? null : "none" },
                    children = { new ArabicKeyboard() }
                },
                new div
                {
                    style    = { display = state.ShowMushafOptions == true ? null : "none" },
                    children = { new MushafOptionsView { Model = MushafOption, MushafOptionChanged = OnMushafOptionChanged } }
                }
            }
        };
    }

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

    void OnMushafOptionChanged(MushafOption mushafOption)
    {
        MushafOption = mushafOption;
        DispatchEvent(() => MushafOptionChanged, mushafOption);
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
}