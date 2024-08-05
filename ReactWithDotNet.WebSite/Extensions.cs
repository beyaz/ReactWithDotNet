using System.IO;
using Microsoft.AspNetCore.Http;
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


    public static Style PrimaryButtonStyle => new()
    {
        backgroundImage = "linear-gradient(to right, #DA22FF 0%, #9733EE  51%, #DA22FF  100%)",
        padding         = "15px 45px",
        textAlign       = "center",
        transition      = "0.5s",
        backgroundSize  = "200% auto",
        color           = "white",
        boxShadow       = "0 0 20px #eee",
        borderRadius    = "10px",

        textDecoration = "none",

        hover =
        {
            backgroundPosition = "right center",

        }
    };

}