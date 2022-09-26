namespace QuranAnalyzer.WebUI.Components;

class ErrorText : ReactComponent
{
    public string Text { get; set; }

    protected override Element render()
    {
        var element = new small(DisplayNoneWhen(Text.HasNoValue()))
        {
            text = Text,
            style =
            {
                color     = "#e24c4c",
                marginTop = "5px"
            }
        };

        return element;
    }
}