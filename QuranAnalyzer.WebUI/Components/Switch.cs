namespace QuranAnalyzer.WebUI.Components;

class SwitchWithLabel: ReactComponent
{
    public string label;
    public bool value;
    public Action<bool> valueChange;
    public bool IsDisabled;

    protected override Element render()
    {
        return new FlexRow(AlignItemsCenter, Gap(5))
        {
            new Switch
            {
                IsChecked = value, ValueChange = valueChange, IsDisabled = IsDisabled
            },
            label
        };
    }
}
public class Switch : ReactComponent
{
    public bool IsChecked { get; set; }
    public bool IsDisabled { get; set; }

    [ReactCustomEvent]
    public Action<bool> ValueChange { get; set; }
    
    protected override Element render()
    {
        Style style = new()
        {
            //ComponentBorder,
            Background("#ced4da"),
            BorderRadius(30),
            CursorPointer,
            Transition("background-color 0.2s, color 0.2s, border-color 0.2s, box-shadow 0.2s"),

            Hover(Background("#b6bfc8")),

            Width("3rem"),
            Height("1.75rem"),
            PositionRelative,
        };

        var before = new Style
        {
            content            = "",
            background         = "white",
            width              = "1.25rem",
            height             = "1.25rem",
            left               = "0.25rem",
            marginTop          = "-0.625rem",
            borderRadius       = "50%",
            transitionDuration = "0.2s",
            position           = "absolute",
            top                = "50%"
        };

        if (IsChecked)
        {
            style.background       = "#6366F1";
            style.hover.background = "#4f46e5";
            before.transform       = "translateX(1.25rem)";
        }
        else
        {
            before.transform = "translateX(0rem)";
        }
        
        style.before.Import(before);

        return new div(style)
        {
            When(!IsDisabled,OnClick(OnClickHandler))
        };
    }

    void OnClickHandler(MouseEvent _)
    {
        IsChecked = !IsChecked;
        DispatchEvent(()=>ValueChange,IsChecked);
    }
}