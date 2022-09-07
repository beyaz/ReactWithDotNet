
namespace QuranAnalyzer.WebUI.Components;

class BackdropView: ReactComponent
{
    public bool IsActive { get; set; }

    protected override Element render()
    {
        return new div
        {
            className = "p-blockui p-component-overlay p-component-overlay-enter", 
            style = { zIndex = "3" , display = IsActive ? "": "none"},
            onClick = OnBackdropClicked
        };
    }

    void OnBackdropClicked(string _)
    {
        ClientTask.DispatchEvent(ApplicationEventName.OnHamburgerMenuClosed);
    }
}