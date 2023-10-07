using System.Threading.Tasks;
using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;


namespace ReactWithDotNet.WebSite.Showcases;

class MonacoEditorDemoState
{
    public string Content { get; set; }
    
    public int LetterCount { get; set; }
}
class MonacoEditorDemo : Component<MonacoEditorDemoState>
{
    protected override Task constructor()
    {

        state = new MonacoEditorDemoState
        {
            Content = """
                      {
                        "name": "xyz",
                        "year": 6,
                        "hasValue": null
                      }
                      """
        };
        
        OnKeypressFinished();
        
        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return new FlexColumn
        {
            new link{href = "https://fonts.cdnfonts.com/css/ibm-plex-mono-3", rel = "stylesheet"},
            new Editor
            {
                width           = "640px",
                height          = "360px",
                defaultLanguage = "json",
          
                valueBind                = ()=>state.Content,
                valueBindDebounceTimeout = 500,
                valueBindDebounceHandler = OnKeypressFinished,

                options =
                {
                    renderLineHighlight ="none",
                    fontFamily          ="'IBM Plex Mono Medium', 'Courier New', monospace",
                    minimap = new { enabled = false }
                }
            },
            new FlexRow
            {
                (b)"Letter Count:", state.LetterCount
            }
        };
    }

    void OnKeypressFinished()
    {
        state.LetterCount = state.Content.Length;
    }
}