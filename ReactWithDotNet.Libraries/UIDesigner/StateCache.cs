using System.IO;
using System.Reflection;
using System.Text.Json;

namespace ReactWithDotNet.UIDesigner;

class StateCache
{
    #region Properties
    static string StateFilePath => Path.Combine(CacheDirectory, $@"{nameof(UIDesignerModel)}.json");
    #endregion

    #region Static Fields
    static readonly string CacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ReactWithDotNetDesigner") +
                                            Path.DirectorySeparatorChar +
                                            (Assembly.GetEntryAssembly()?.GetName().Name ?? "UnknowAssembly") +
                                            Path.DirectorySeparatorChar;

    static readonly object fileLock = new();
    #endregion

    #region Public Methods
    public static string ReadFromCache(string type)
    {
        if (File.Exists(GetCacheFilePath(type)))
        {
            return File.ReadAllText(GetCacheFilePath(type));
        }

        return null;
    }

    public static UIDesignerModel ReadState()
    {
        if (File.Exists(StateFilePath))
        {
            var json = File.ReadAllText(StateFilePath);

            try
            {
                return JsonSerializer.Deserialize<UIDesignerModel>(json);
            }
            catch (Exception)
            {
                return new UIDesignerModel();
            }
        }

        return new UIDesignerModel();
    }

    public static void SaveState(UIDesignerModel state)
    {
        lock (fileLock)
        {
            WriteAllText(StateFilePath, JsonSerializer.Serialize(state, new JsonSerializerOptions { WriteIndented = true, IgnoreNullValues = true }));
        }
    }

    public static void SaveFileToCache(string fileNameWithoutExtension, string jsonContent)
    {
        lock (fileLock)
        {
            WriteAllText(GetCacheFilePath(fileNameWithoutExtension), jsonContent);
        }
    }
    #endregion

    #region Methods
    static string GetCacheFilePath(string type) => $@"{CacheDirectory}{type}.json";

    static void WriteAllText(string path, string contents)
    {
        if (!Directory.Exists(Path.GetDirectoryName(path)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path));
        }

        File.WriteAllText(path, contents);
    }
    #endregion
}