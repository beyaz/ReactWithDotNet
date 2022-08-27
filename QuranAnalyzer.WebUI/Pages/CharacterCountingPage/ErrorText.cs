namespace QuranAnalyzer.WebUI.Pages.CharacterCountingPage;


class ErrorTextModel
{
    public string Text { get; set; }
}

class ErrorText: ReactComponent<ErrorTextModel>
{
    public string Text { get; set; }
    
    public ErrorText()
    {
        state = new ErrorTextModel();
        
        StateInitialized += () => state.Text = Text;
    }
    

    public void ClearMessage()
    {
        state.Text = null;
        Text       = null;
    }
    
    public override Element render()
    {
        if (state.Text.HasValue())
        {
            ClientTask.GotoMethod(3000, nameof(ClearMessage));
        }
        
        var element = new small
        {
            text = state.Text,
            style =
            {
                color     = "#e24c4c",
                marginTop = "5px"
            }
        };

        if (state.Text.HasNoValue())
        {
            element.style.display = "none";
        }
        
        return element;
        
    }
}