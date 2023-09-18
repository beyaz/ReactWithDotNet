using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;


namespace ReactWithDotNet.WebSite.Showcases;

public class MonacoEditorDemo : ReactComponent
{
    
    
    protected override Element render()
    {
        return new Editor
        {
            width           = "640px",
            height          = "360px",
            defaultLanguage = "json",
          

            options =
            {
                renderLineHighlight ="none",
                matchBrackets       ="always"
            }
        };
    }

    
}