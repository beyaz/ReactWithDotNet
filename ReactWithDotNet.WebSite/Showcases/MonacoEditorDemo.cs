using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;
using ReactWithDotNet.ThirdPartyLibraries.React_Player;

namespace ReactWithDotNet.WebSite.Showcases;

public class MonacoEditorDemo : ReactComponent
{
    
    
    protected override Element render()
    {
        return new Editor
        {
            style =
            {
                width  = "640px",
                height = "360px"
            },
            defaultLanguage = "json",
            defaultValue    = "{yy:6}",
            options =
            {
                renderLineHighlight ="none"
            }
        };
    }

    
}