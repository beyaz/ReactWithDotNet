using System.IO;

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

    public static string GetSvgUrl(string svgFileName) => GetDataUriFromSvgBytes(GetEmbeddedFile($"ReactWithDotNet.Libraries.UIDesigner.Resources.{svgFileName}.svg"));
    
    static string GetDataUriFromSvgBytes(byte[] bytesOfSvgFile)
    {
        var imageBase64 = Convert.ToBase64String(bytesOfSvgFile);

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