using ReactWithDotNet.ThirdPartyLibraries.MonacoEditorReact;

namespace ReactWithDotNet.WebSite.Components;

sealed class CodeViewer : PureComponent
{
    public CodeViewer(params Modifier[] modifiers)
    {
        Add(modifiers);
    }

    public static Modifier LangCSharp => CreatePureComponentModifier<CodeViewer>(x => { x.LanguageIsCSharp = true; });

    public static Modifier LangTypeScript => CreatePureComponentModifier<CodeViewer>(x => { x.LanguageIsTypeScript = true; });

    public string Code { get; init; }

    public bool LanguageIsCSharp { get; set; }

    public bool LanguageIsTypeScript { get; set; }

    protected override Element render()
    {
        var language = "csharp";
        if (LanguageIsTypeScript)
        {
            language = "typescript";
        }

        var code = Code ?? (children.FirstOrDefault() as HtmlTextNode)?.text;

        if (DesignMode && code is null)
        {
            Add(SizeFull);
            
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

        return new FlexRowCentered(Border(1,solid,Gray200), BorderRadius(4), Padding(4), SizeFull)
        {
            SpaceY(4),
            new Editor
            {
                defaultLanguage = language,
                value           = code,
                options =
                {
                    renderLineHighlight = "none",
                    fontFamily          = "Consolas, monospace",
                    fontSize            = 11,
                    minimap             = new { enabled = false },
                    // lineNumbers         = "off",
                    unicodeHighlight = new { showExcludeOptions = false },
                    readOnly         = true
                }
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