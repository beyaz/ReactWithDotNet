using System.Reflection;

namespace ReactWithDotNet.UIDesigner;

static class Extensions
{
    public static string GetSvgUrl(string svgFileName)
    {
        var resourceFilePathInAssembly = $"ReactWithDotNet.UIDesigner.Resources.{svgFileName}.svg";

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

    public static bool HasValue(this string value)
    {
        return !string.IsNullOrWhiteSpace(value);
    }

    public static void IgnoreException(Action action)
    {
        try
        {
            action();
        }
        catch (Exception)
        {
            // ignored
        }
    }

    public static void RefreshComponentPreview(this Client client)
    {
        const string jsCode =
            """
            var frame = window.frames[0];
            if(frame)
            {
              var reactWithDotNet = frame.ReactWithDotNet;
              if(reactWithDotNet)
              {
                reactWithDotNet.DispatchEvent('RefreshComponentPreview', []);
              }
            }
            """;
        
        client.RunJavascript(jsCode);
    }
    
    public static void RefreshComponentPreviewCompleted(this Client client)
    {
        const string jsCode =
            """
            var parentWindow = window.parent;
            if(parentWindow)
            {
              var reactWithDotNet = parentWindow.ReactWithDotNet;
              if(reactWithDotNet)
              {
                reactWithDotNet.DispatchEvent('RefreshComponentPreviewCompleted', []);
              }
            }
            """;
        
        client.RunJavascript(jsCode);
    }
}