namespace ReactWithDotNet.WebSite;

class Extensions
{
    public static IEnumerable<Element> PrimeReactCssLibs => new[]
    {
        new link { rel = "stylesheet", href = "https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/themes/saga-blue/theme.css" },
        new link { rel = "stylesheet", href = "https://cdn.jsdelivr.net/npm/primereact@8.2.0/resources/primereact.min.css" },
        new link { rel = "stylesheet", href = "https://cdn.jsdelivr.net/npm/primeicons@5.0.0/primeicons.css" },

    };

    public static string Asset(string fileName) => $"wwwroot/assets/{fileName}";
}