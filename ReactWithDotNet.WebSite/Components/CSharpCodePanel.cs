using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;
using ReactWithDotNet.ThirdPartyLibraries.ReactFreeScrollbar;
using ReactWithDotNet.ThirdPartyLibraries.UIW.ReactCodemirror;

namespace ReactWithDotNet.WebSite.Components;

class CSharpCodePanel : PureComponent
{
    public string Code { get; set; }
    
    protected override Element render()
    {
        return new FreeScrollBar
        {
            WidthMaximized,
            Height(300),
            FontSize12,
            
            //new style
            //{
            //    $".cm-editor{{ {new Style
            //    {
            //        Background(Theme.EditorBackground),
            //        BoxShadow(0, 4, 8, -3, rgba(0,0,0,0.1))

            //    }.ToCss()
            //    } }}"
            //},
            
            new Editor
            {
                defaultLanguage          = "csharp",
                
                value                = Code,
                options =
                {
                    renderLineHighlight ="none",
                    fontFamily          ="'IBM Plex Mono Medium', 'Courier New', monospace",
                    fontSize            = 11,
                    minimap             = new { enabled = false },
                    lineNumbers         = "off"
                }
            }
            
            //new CodeMirror
            //{
            //    extensions = { "java", "githubLight" },
            //    value = Code,
            //    basicSetup =
            //    {
            //        highlightActiveLine       = false,
            //        highlightActiveLineGutter = false,
            //    }
            //}
        };
    }
}