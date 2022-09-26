namespace QuranAnalyzer.WebUI.Components;

class ErrorText : ReactComponent
{
    public string Text { get; set; }

    protected override Element render()
    {
        var element = new small
        {
            text = Text,
            style =
            {
                Color("#e24c4c"),
                MarginTop(5),
                DisplayNoneWhen(Text.HasNoValue())
            }
        };

        return element;
    }
}