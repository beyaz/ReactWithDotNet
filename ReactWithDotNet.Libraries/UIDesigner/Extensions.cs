using System.IO;
using System.Text;

namespace ReactWithDotNet.UIDesigner;

static class Extensions
{
    public static (T value, Exception exception) Try<T>(Func<T> func)
    {
        try
        {
            return (func(), null);
        }
        catch (Exception exception)
        {
            return (default, exception);
        }
    }

    public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);

    public static string GetSvgUrl(string svgFileName) => SvgFileToDataUri($"wwwroot/integration/ReactWithDotNet-UIDesigner/{svgFileName}.svg");

    static string SvgFileToDataUri(string filePath)
    {
        var imageBytes  = File.ReadAllBytes(filePath);
        
        var imageBase64 = Convert.ToBase64String(imageBytes);
        
        return "data:image/svg+xml;base64," + imageBase64;
    }

    static byte[] GetEmbeddedFile(string resourceFilePathInAssembly)
    {
        var assembly = typeof(Extensions).Assembly;
        
        var resourceStream = assembly.GetManifestResourceStream(resourceFilePathInAssembly);
        if (resourceStream == null)
        {
            return null;
        }

        using var memoryStream = new MemoryStream();
        
        resourceStream.CopyTo(memoryStream);
        
        return memoryStream.ToArray();
    }
}