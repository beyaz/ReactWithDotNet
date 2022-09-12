using ReactWithDotNet.WebSite.Components;

namespace ReactWithDotNet.WebSite;


public class MainWindow: ReactComponent
{
    protected override Element render()
    {
        return new FigmaCss2ReactInlineStyleConverterView();

    }
}