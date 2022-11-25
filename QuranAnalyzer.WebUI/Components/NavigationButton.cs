namespace QuranAnalyzer.WebUI.Components;

public class NavigationButton : ReactComponent
{
    public string Text { get; set; }

    public int Size { get; set; } = 45;

  
    
    protected override Element render()
    {
        return new FlexRowCentered
        {
            Text(Text),
            Background("#EEF2FF"),
            BorderRadius("50%"),
            wh(Size),
            Hover(Color("#1e0ee7"), CursorPointer)
        };
    }
}