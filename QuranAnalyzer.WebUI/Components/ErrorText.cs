namespace QuranAnalyzer.WebUI.Components;

class ErrorText : ReactComponent
{
    public string Text { get; set; }

    void ClearMessage()
    {
        Text = null;
    }

    protected override Element render()
    {
        if (Text.HasValue())
        {
            ClientTask.GotoMethod(5000, ClearMessage);
        }

        var element = new small
        {
            text = Text,
            style =
            {
                color     = "#e24c4c",
                marginTop = "5px"
            }
        };

        if (Text.HasNoValue())
        {
            element.style.display = "none";
        }

        return element;
    }
}