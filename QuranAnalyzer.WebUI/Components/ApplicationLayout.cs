
namespace QuranAnalyzer.WebUI.Components;

class BackdropModel
{
    
    
}

class BackdropView: ReactComponent<BackdropModel>
{
    public bool IsActive { get; set; }
    
    public override Element render()
    {
        return new div
        {
            className = "p-blockui p-component-overlay p-component-overlay-enter", 
            style = { zIndex = "3" , display = IsActive ? "": "none"},
            onClick = _=>ClientTask.DispatchEvent(ApplicationEventName.OnHamburgerMenuClosed)
        };
    }
}
