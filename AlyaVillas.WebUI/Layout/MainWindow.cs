using AlyaVillas.WebUI.Components;
using AlyaVillas.WebUI.Views.Home;

namespace AlyaVillas.WebUI.Layout;

class MainWindow : ReactComponent
{
    protected override Element render()
    {
        
        return new div
        {
            new Header{IsTransparent = true},
            new HomeView(),
            new Footer()
        };

    }
}