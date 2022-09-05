namespace QuranAnalyzer.WebUI.Components;


class ErrorTextModel
{
}

class ErrorText: ReactComponent<ErrorTextModel>
{
    public string Text { get; set; }
    
    public ErrorText()
    {
        state = new ErrorTextModel();
        
    }
    

    public void ClearMessage()
    {
        Text       = null;
    }

    protected override Element render()
    {
        if (Text.HasValue())
        {
            ClientTask.GotoMethod(3000, ClearMessage);
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