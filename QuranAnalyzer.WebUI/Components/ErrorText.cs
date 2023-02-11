namespace QuranAnalyzer.WebUI.Components;

class ErrorText : ReactPureComponent
{
    public string Text { get; set; }

    protected override Element render()
    {
        return new small
        {
            text = Text,
            style =
            {
                Color("#e24c4c"),
                MarginTop(5),
                When(Text.HasNoValue(), DisplayNone)
            }
        };
    }
}