using System.Text;
using System.Web;
using Microsoft.Net.Http.Headers;

namespace ReactWithDotNet.WebSite.Components;

sealed class LivePreview : Component
{
    public const string QueryParameterNameOfGuid = "guid";

    static ScriptManager Scripts => ScriptManager.Instance;

    string GuidParameter => GetQuery(QueryParameterNameOfGuid);

    public static void RefreshComponentPreview(Client client, string guid)
    {
        var jsCode =
            $"var eventName = '{GetRefreshPreviewEventName(guid)}';"
            + Environment.NewLine +
            $"var frame = document.getElementById('{guid}');"
            + Environment.NewLine +
            """

            if(frame)
            {
              var reactWithDotNet = frame.contentWindow.ReactWithDotNet;
              if(reactWithDotNet)
              {
                reactWithDotNet.DispatchEvent(eventName, []);
              }
            }
            """;

        client.RunJavascript(jsCode);
    }

    protected override Task constructor()
    {
        Client.ListenEvent(GetRefreshPreviewEventName(GuidParameter), Refresh);

        return Task.CompletedTask;
    }

    protected override Element render()
    {
        return CreatePreview(GuidParameter);
    }

    static Element CreatePreview(string guid)
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

        var fullCSharpCode = GetFullCSharpCodeByRenderPartOfCode(cacheItem.RenderPartOfCSharpCode);

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

    static string GetFullCSharpCodeByRenderPartOfCode(string renderPartOfCSharpCode)
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

    static string GetRefreshPreviewEventName(string guid)
    {
        return "RefreshComponentPreview" + guid;
    }

    string GetQuery(string name)
    {
        var value = Context.HttpContext.Request.Query[name].FirstOrDefault();
        if (value != null)
        {
            return value;
        }

        var referer = Context.HttpContext.Request.Headers[HeaderNames.Referer];
        if (string.IsNullOrWhiteSpace(referer))
        {
            return null;
        }

        var nameValueCollection = HttpUtility.ParseQueryString(new Uri(referer).Query);

        return nameValueCollection[name];
    }

    Task Refresh()
    {
        return Task.CompletedTask;
    }
}