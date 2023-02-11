namespace QuranAnalyzer.WebUI.Components;

public class ActionButton : ReactComponent
{
    public bool IsProcessing { get; set; }
    
    public string Label { get; set; }

    [ReactCustomEvent]
    public Action OnClick { get; set; }

    protected override Element render()
    {
        return new FlexRowCentered
        {
            children =
            {
                When(IsProcessing, new LoadingIcon { wh(17) }),
                When(!IsProcessing, new div(Label))
            },
            onClick = ActionButtonOnClick,
            style =
            {
                Color(BluePrimary),
                Border($"1px solid {BluePrimary}"),
                Background("transparent"),
                BorderRadius(5),
                Padding(10, 30),
                CursorPointer
            }
        };
    }

    void ActionButtonOnClick(MouseEvent _)
    {
        IsProcessing = true;

        DispatchEvent(() => OnClick);
    }
}