using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;

namespace ReactWithDotNet.WebSite.Components;

sealed class CSharpCodePanel : PureComponent
{
    public required string Code { get; init; } = "using System;";

    protected override Element render()
    {
        var code = Code;
        if (DesignMode && code is null)
        {
            code = """"
                   using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;
                   
                   namespace ReactWithDotNet.WebSite.Components;
                   
                   sealed class CSharpCodePanel : PureComponent
                   {
                       public required string Code { get; init; } = "using System;";
                   
                       protected override Element render()
                       {
                           return new Editor
                           {
                               defaultLanguage = "csharp",
                               value = Code,
                               options =
                               {
                                   renderLineHighlight = "none",
                                   fontFamily          = "Consolas, monospace",
                                   fontSize            = 11,
                                   minimap             = new { enabled = false },
                                   // lineNumbers         = "off",
                                   unicodeHighlight    = new { showExcludeOptions = false },
                                   readOnly            = true
                               }
                           };
                       }
                   }
                   """";
        }
        return new Editor
        {
            defaultLanguage = "csharp",
            value = code,
            options =
            {
                renderLineHighlight = "none",
                fontFamily          = "Consolas, monospace",
                fontSize            = 11,
                minimap             = new { enabled = false },
                // lineNumbers         = "off",
                unicodeHighlight    = new { showExcludeOptions = false },
                readOnly            = true
            }
        };
    }
}