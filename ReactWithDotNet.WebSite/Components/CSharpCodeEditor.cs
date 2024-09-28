using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;

namespace ReactWithDotNet.WebSite.Components;

sealed class CSharpCodeEditor : Component
{
    public Expression<Func<string>> valueBind { get; init; }
    public Func<Task> valueBindDebounceHandler { get; init; }

    protected override Element render()
    {
        return new Editor
        {
            valueBind                = valueBind,
            valueBindDebounceHandler = valueBindDebounceHandler,
            valueBindDebounceTimeout = 500,
            defaultLanguage          = "html",
            options =
            {
                renderLineHighlight = "none",
                fontFamily          = "consolas, 'IBM Plex Mono Medium', 'Courier New', monospace",
                fontSize            = 11,
                minimap             = new { enabled = false },
                lineNumbers         = "off",
                unicodeHighlight    = new { showExcludeOptions = false }
            }
        };
    }
}