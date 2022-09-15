using System;
using System.IO;
using System.Reflection;
using System.Text.Json;

namespace ReactWithDotNet.UIDesigner;

class StateCache
{
    #region Static Fields
    static readonly string CacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ReactWithDotNet.UIDesigner") + 
                                            Path.DirectorySeparatorChar +
                                            (System.Reflection.Assembly.GetEntryAssembly()?.GetName().Name ?? "UnknowAssembly")+
                                            Path.DirectorySeparatorChar;

    static readonly object fileLock = new();
    #endregion

    #region Properties
    static string StateFilePath => Path.Combine(CacheDirectory, $@"{nameof(UIDesignerModel)}.json");
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
            WriteAllText(StateFilePath, JsonSerializer.Serialize(state, new JsonSerializerOptions{ WriteIndented = true, IgnoreNullValues = true}));
        }
    }

    public static void SaveToCache(string type, string json)
    {
        lock (fileLock)
        {
            WriteAllText(GetCacheFilePath(type), json);
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
        
        File.WriteAllText(path,contents);
    }
    #endregion
}