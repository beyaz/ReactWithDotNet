namespace QuranAnalyzer.WebUI.Components;

public class NavigationButton : ReactComponent
{
    public string Text { get; set; }

    protected override Element render()
    {
        return new FlexRowCentered
        {
            Text(Text),
            Background("#EEF2FF"),
            BorderRadius("50%"),
            wh(45),
            Hover(Color("#1e0ee7"), CursorPointer)
        };
    }
}