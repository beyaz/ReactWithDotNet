using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;

namespace ReactWithDotNet.WebSite.Showcases;

class MonacoEditorDemo : Component<MonacoEditorDemo.State>
{
    protected override Task constructor()
    {
        state = new()
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
            new link { href = "https://fonts.cdnfonts.com/css/ibm-plex-mono-3", rel = "stylesheet" },
            new Editor
            {
                width           = "640px",
                height          = "360px",
                defaultLanguage = "json",

                valueBind                = () => state.Content,
                valueBindDebounceTimeout = 500,
                valueBindDebounceHandler = OnKeypressFinished,

                options =
                {
                    renderLineHighlight = "none",
                    fontFamily          = "'IBM Plex Mono Medium', 'Courier New', monospace",
                    minimap             = new { enabled            = false },
                    unicodeHighlight    = new { showExcludeOptions = false }
                }
            },
            new FlexRow
            {
                (b)"Letter Count:", state.LetterCount
            }
        };
    }

    Task OnKeypressFinished()
    {
        state.LetterCount = state.Content.Length;

        return Task.CompletedTask;
    }

    internal sealed record State
    {
        public string Content { get; set; }

        public int LetterCount { get; set; }
    }
}