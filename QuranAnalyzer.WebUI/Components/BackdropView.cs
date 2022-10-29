
namespace QuranAnalyzer.WebUI.Components;

class BackdropView: ReactComponent
{
    public bool IsActive { get; set; }

    protected override Element render()
    {
        if (IsActive is false)
        {
            return null;
        }
        
        return new div
        {
            style =
            {
                Zindex(3),
                PositionAbsolute,
                TopBottom(0),LeftRight(0),
                BackgroundColor("rgba(0, 0, 0, 0.5)")
            },
            onClick = OnBackdropClicked
        };
    }

    [CacheThisMethod]
    void OnBackdropClicked(MouseEvent e)
    {
        Client.HamburgerMenuClosed();
    }
}