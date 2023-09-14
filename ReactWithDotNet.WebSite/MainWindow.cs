using ReactWithDotNet.WebSite.HeaderComponents;
using ReactWithDotNet.WebSite.Pages;
using ReactWithDotNet.WebSite.Showcases;

namespace ReactWithDotNet.WebSite;

public class MainWindow : ReactPureComponent
{
    protected override Element render()
    {
        return new OnClickPreviewDemo();
    }
}