using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;

namespace ReactWithDotNet.WebSite.Components;

sealed class CSharpCodePanel : PureComponent
{
    public string Code { get; init; } = "using ...";
    
    protected override Element render()
    {
        return new Editor
        {
            defaultLanguage = "csharp",
                
            value = Code,
                
            options =
            {
                renderLineHighlight ="none",
                fontFamily          ="Consolas, monospace",
                fontSize            = 11,
                minimap             = new { enabled = false },
                lineNumbers         = "off",
                unicodeHighlight    = new { showExcludeOptions = false },
                readOnly            = true
            }
        };
    }
}