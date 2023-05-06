using System.IO;
using System.Reflection;
using ReactWithDotNet.Libraries.mui.material;
using ReactWithDotNet.WebSite.Content;

namespace ReactWithDotNet.WebSite;

static partial class Extensions
{
    public static ReactContextKey<LightTheme> ThemeKey = new(nameof(ThemeKey));

    public static IModifier GetPageLink(string pageName) => Href($"/?p={pageName}");



    public static StyleModifier BorderRadiusForPaper => BorderRadius(4);

    public static StyleModifier DisplayNoneWhenMobile => MediaQueryOnMobile(DisplayNone);
    public static StyleModifier DisplayNoneWhenNotMobile => MediaQueryOnTabletOrDesktop(DisplayNone);

    public static IEnumerable<Element> PrimeReactCssLibs => new[]
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

    static readonly Dictionary<string, ReactPureComponent> svgFileCache = new();
    
    public static ReactPureComponent GetSvgByClassName(string className)
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
        
        instance = (ReactPureComponent)Activator.CreateInstance(type);

        svgFileCache.TryAdd(className, instance);


        return instance;
    }
}