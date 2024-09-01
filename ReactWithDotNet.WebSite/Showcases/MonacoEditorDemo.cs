using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;

namespace ReactWithDotNet.WebSite.Showcases;

sealed class MonacoEditorDemo : Component<MonacoEditorDemo.State>
{
    protected override Task constructor()
    {
        const string sampleJsonContent =
            """
            {
              "name": "xyz",
              "year": 6,
              "hasValue": false
            }
            """;

        state = new()
        {
            Content = sampleJsonContent
        };

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return new FlexColumn
        {
            new link { href = "https://fonts.cdnfonts.com/css/ibm-plex-mono-3", rel = "stylesheet" },
            new Editor
            {
                width           = "300px",
                height          = "350px",
                defaultLanguage = "json",

                valueBind                = () => state.Content,
                valueBindDebounceTimeout = 500,
                valueBindDebounceHandler = OnKeypressFinished,

                options =
                {
                    renderLineHighlight = none,
                    fontFamily          = "'IBM Plex Mono Medium', 'Courier New', monospace",
                    minimap = new
                    {
                        enabled = false
                    },
                    unicodeHighlight = new
                    {
                        showExcludeOptions = false
                    }
                }
            },
            new FlexRow
            {
                (b)"Letter Count: ", state.LetterCount
            }
        };
    }

    Task OnKeypressFinished()
    {
        state = state with
        {
            LetterCount = state.Content.Length
        };

        return Task.CompletedTask;
    }

    internal sealed record State
    {
        public string Content { get; init; }

        public int LetterCount { get; init; }
    }
}