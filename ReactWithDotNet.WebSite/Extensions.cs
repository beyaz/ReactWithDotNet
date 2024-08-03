using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using ReactWithDotNet.WebSite.Content;

namespace ReactWithDotNet.WebSite;

static partial class Extensions
{
    public static readonly ReactContextKey<LightTheme> ThemeKey = new(nameof(ThemeKey));
    public static readonly ReactContextKey<HttpContext> KeyForHttpContext = new(nameof(KeyForHttpContext));
        

    public static Modifier GetPageLink(string pageName) => Href($"/?p={pageName}");



    public static StyleModifier BorderRadiusForPaper => BorderRadius(4);

    public static StyleModifier DisplayNoneWhenMobile => WhenMediaSizeLessThan(MD,DisplayNone);
    public static StyleModifier DisplayNoneWhenNotMobile => MD(DisplayNone);

    public static IEnumerable<Element> PrimeReactCssLibs => new[] // TODO: can be remove
    {
        new link { rel = "stylesheet", href = "https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/themes/saga-blue/theme.css" },
        new link { rel = "stylesheet", href = "https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/primereact.min.css" },
        new link { rel = "stylesheet", href = "https://cdn.jsdelivr.net/npm/primeicons@5.0.0/primeicons.css" },
    };

    internal static SiteRawData RawData => ReadContent<SiteRawData>("SiteRawData");

    public static string Asset(string fileName) => $"wwwroot/assets/{fileName}";

    public static T ReadContent<T>(string fileName)
    {
        var path = Path.Combine("Content", $"{fileName}.yml");

        return YamlHelper.DeserializeFromYaml<T>(File.ReadAllText(path));
    }

    public static string GetRootFolder(this ReactContext context)
    {
        var path = getRequestPath(context.HttpContext);
        
        var deep = (path + "").Split('/', StringSplitOptions.RemoveEmptyEntries).Length;
        if (deep > 1)
        {
            deep--;
        }

        return string.Join(string.Empty, Enumerable.Range(0, deep).Select(_ => "../")) + "wwwroot";
        
        static string getRequestPath(HttpContext httpContext)
        {
            if (httpContext.Request.Path == "/" + "HandleReactWithDotNetRequest")
            {
                if (httpContext.Request.Headers.TryGetValue(HeaderNames.Referer, out var referer) &&
                    httpContext.Request.Headers.TryGetValue(HeaderNames.Host, out var host) &&
                    referer[0] is not null &&
                    host[0] is not null)
                {
                    var path = referer[0].Substring(referer[0].IndexOf(host[0], StringComparison.OrdinalIgnoreCase));

                    path = path.Substring(path.IndexOf('/'));
                    if (path.Length > 1)
                    {
                        return path;
                    }
                }
            }

            return httpContext.Request.Path;
        }
    }
    
    public static string GetArticleHtmlContent(string filePathInContentFolder)
    {
        var path = Path.Combine("Content", filePathInContentFolder);

        return File.ReadAllText(path);
    }

    public static List<B> ToListOf<A, B>(this IEnumerable<A> enumerable, Func<A, B> convert)
    {
        if (enumerable == null)
        {
            throw new ArgumentNullException(nameof(enumerable));
        }

        if (convert == null)
        {
            throw new ArgumentNullException(nameof(convert));
        }

        return enumerable.Select(convert).ToList();
    }

    static readonly Dictionary<string, PureComponent> svgFileCache = new();
    
    public static PureComponent GetSvgByClassName(string className)
    {
        className = className.Replace(".svg","");
        
        if (svgFileCache.TryGetValue(className, out var instance))
        {
            return instance;
        }
        
        var type = typeof(Extensions).Assembly.GetType($"ReactWithDotNet.WebSite.SvgFiles.{className}",false);
        if (type == null)
        {
            throw new TypeLoadException(className);
        }
        
        instance = (PureComponent)Activator.CreateInstance(type);

        svgFileCache.TryAdd(className, instance);


        return instance;
    }

    public static Style ContainerStyle => new()
    {
        WidthFull,
        MaxWidth(1200),
        DisplayFlexRow,
        JustifyContentCenter,

        PaddingLeftRight(24)
    };
    
    public static string Grey50 ="#F3F6F9";
    public static string Grey100="#E5EAF2";
    public static string Grey200="#DAE2ED";
    public static string Grey300="#C7D0DD";
    public static string Grey400="#B0B8C4";
    public static string Grey500="#9DA8B7";
    public static string Grey600="#6B7A90";
    public static string Grey700="#434D5B";
    public static string Grey800="#303740";
    public static string Grey900="#1C2025";
}