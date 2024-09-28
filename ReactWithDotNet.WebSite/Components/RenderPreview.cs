using System.Text;

namespace ReactWithDotNet.WebSite.Components;

sealed class RenderPreview : Component<RenderPreview.Model>
{
    public string RenderPartOfCSharpCode { get; init; }

    protected override Task constructor()
    {
        state = new()
        {
            RenderPartOfCSharpCode = RenderPartOfCSharpCode,
            
            Guid = Guid.NewGuid().ToString("N")
        };
        
        return Task.CompletedTask;
    }

    public static string GetRefreshPreviewEventName(string guid)
    {
        return "RefreshComponentPreview" + guid;
    }
    
    protected override Element render()
    {
        var width = Width(100 * percent) + MD(Width(50 * percent));
        
        return new FlexRow(SizeFull, BoxShadow("rgb(0 0 0 / 34%) 0px 2px 5px 0px"), BorderRadius(3), CursorDefault, JustifyContentSpaceBetween, FlexWrap)
        {
            new FlexRowCentered(width, BorderRight(Solid(1, rgb(235, 236, 240))))
            {
                new CSharpCodePanel
                {
                    Code = state.RenderPartOfCSharpCode
                }
            },
            
            new FlexRowCentered(width, Background(rgb(246, 247, 249)), MinSize(200))
            {
                CreatePreview(state.Guid)
            }
        };
    }

    static ScriptManager Scripts=>ScriptManager.Instance;
    
    public static Element CreatePreview(string guid)
    {
        if (guid is null)
        {
            return "guid is null";
        }

        var cacheItem = Scripts[guid];
        if (string.IsNullOrWhiteSpace(cacheItem?.RenderPartOfCSharpCode))
        {
            return new pre
            {
                "Empty CSharp Code"
            };
        }

        var assemblyName = guid;

        var fullCSharpCode = RenderPreview.GetFullCSharpCodeByRenderPartOfCode(cacheItem.RenderPartOfCSharpCode);

        var (isTypeFound, type, assemblyLoadContext, sourceCodeHasError, sourceCodeError) = DynamicCode.LoadAndFindType(assemblyName, [fullCSharpCode], "Preview.SampleComponent");
        if (sourceCodeHasError)
        {
            return new pre(Color(Red300)) { sourceCodeError };
        }

        if (!isTypeFound)
        {
            assemblyLoadContext?.Unload();

            return new pre
            {
                "Unexpected error. class not found."
            };
        }

        Scripts[assemblyName] = cacheItem with
        {
            AssemblyLoadContext = assemblyLoadContext,
            Type = type
        };

        var instance = type.Assembly.CreateInstance("Preview.SampleComponent");

        return (ReactWithDotNet.Component)instance;
    }
    
    public static string GetFullCSharpCodeByRenderPartOfCode(string renderPartOfCSharpCode)
    {
        var sb = new StringBuilder();
        sb.AppendLine("using ReactWithDotNet;");
        sb.AppendLine("using static ReactWithDotNet.Mixin;");
        sb.AppendLine();
        sb.AppendLine("namespace Preview;");
        sb.AppendLine();
        sb.AppendLine("class SampleComponent: Component");
        sb.AppendLine("{");

        sb.AppendLine("  protected override Element render()");
        sb.AppendLine("  {");
        sb.AppendLine("    return ");

        sb.AppendLine("      // s t a r t ");
        foreach (var line in renderPartOfCSharpCode.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            sb.AppendLine("      " + line);
        }

        sb.AppendLine("      // e n d");

        sb.AppendLine("    ;");

        sb.AppendLine("  }");

        sb.AppendLine("  protected override Element componentDidCatch(Exception exceptionOccurredInRender)");
        sb.AppendLine("  {");
        sb.AppendLine("    return new pre(Color(Red300))");
        sb.AppendLine("    {");
        sb.AppendLine("      exceptionOccurredInRender.ToString()");
        sb.AppendLine("    };");
        sb.AppendLine("  }");

        sb.AppendLine("}");

        return sb.ToString();
    }
    
    
    
    internal record Model
    {
        public string RenderPartOfCSharpCode { get; init; }
        public string Guid { get; init; }
    }
}