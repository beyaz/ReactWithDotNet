using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;

namespace ReactWithDotNet.WebSite.Components;

sealed class CodeViewer : PureComponent
{
    public required string Code { get; init; }

    public bool LanguageIsTypeScript { get; init; }
    
    public bool LanguageIsCSharp { get; init; }

    protected override Element render()
    {

        var language = "csharp";
        if (LanguageIsTypeScript)
        {
            language = "typescript";
        }
        
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
            defaultLanguage = language,
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

sealed class CSharpCodePanel : PureComponent
{
    public required string Code { get; init; }

    protected override Element render()
    {
        return new CodeViewer { Code = Code, LanguageIsCSharp = true };
    }
}

