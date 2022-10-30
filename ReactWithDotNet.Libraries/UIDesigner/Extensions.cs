using System.IO;
using System.Reflection;

namespace ReactWithDotNet.UIDesigner;

static class Extensions
{

    public static void OnBrowserInactive(this Client client)
    {
        client.DispatchEvent(nameof(OnBrowserInactive));
    }
    public static void OnBrowserInactive(this Client client, Action handlerAction)
    {
        client.ListenEvent(OnBrowserInactive, handlerAction);
    }
    
    public static string GetSvgUrl(string svgFileName)
    {
        var resourceFilePathInAssembly = $"ReactWithDotNet.Libraries.UIDesigner.Resources.{svgFileName}.svg";

        return getDataUriFromSvgBytes(getEmbeddedFile(resourceFilePathInAssembly));

        static string getDataUriFromSvgBytes(byte[] bytesOfSvgFile)
        {
            var imageBase64 = Convert.ToBase64String(bytesOfSvgFile);

            return "data:image/svg+xml;base64," + imageBase64;
        }

        static byte[] getEmbeddedFile(string resourceFilePathInAssembly)
        {
            var assembly = Assembly.GetExecutingAssembly();

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

    public static bool HasValue(this string value) => !string.IsNullOrWhiteSpace(value);

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

    public static StyleModifier PrimaryBackground => Background("rgb(249, 249, 249");
}