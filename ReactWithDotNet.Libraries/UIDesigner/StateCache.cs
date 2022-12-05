using System.IO;
using System.Reflection;
using System.Text.Json;

namespace ReactWithDotNet.UIDesigner;

class StateCache
{
    #region Properties
    static string StateFilePath => Path.Combine(CacheDirectory, $@"{nameof(ReactWithDotNetDesignerModel)}.json");
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

    public static ReactWithDotNetDesignerModel ReadState()
    {
        if (File.Exists(StateFilePath))
        {
            var json = File.ReadAllText(StateFilePath);

            try
            {
                var state= JsonSerializer.Deserialize<ReactWithDotNetDesignerModel>(json) ?? new ReactWithDotNetDesignerModel();
                if (state.SelectedMethod is not null)
                {
                    return TryRead(state.SelectedMethod) ?? state;
                }

                if (state.SelectedType is not null)
                {
                    return TryRead(state.SelectedType) ?? state;
                }

                return state;
            }
            catch (Exception)
            {
                return new ReactWithDotNetDesignerModel();
            }
        }

        return new ReactWithDotNetDesignerModel();
    }

    public static void SaveState(ReactWithDotNetDesignerModel state)
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
    
    public static void Save(TypeReference typeReference, ReactWithDotNetDesignerModel state)
    {
        var jsonContent = JsonSerializer.Serialize(state, new JsonSerializerOptions
        {
            WriteIndented    = true,
            IgnoreNullValues = true
        });
        
        SaveFileToCache(GetFileName(typeReference), jsonContent);
    }

    public static void Save(MethodReference methodReference, ReactWithDotNetDesignerModel state)
    {
        var jsonContent = JsonSerializer.Serialize(state, new JsonSerializerOptions
        {
            WriteIndented    = true,
            IgnoreNullValues = true
        });

        SaveFileToCache(GetFileName(methodReference), jsonContent);
    }


    static string GetFileName(MethodReference methodReference) => methodReference.ToString().GetHashCode().ToString();
    static string GetFileName(TypeReference typeReference) => typeReference.ToString().GetHashCode().ToString();

    public static ReactWithDotNetDesignerModel TryRead(MethodReference methodReference)
    {
        var filePath = GetCacheFilePath(GetFileName(methodReference));
        if (!File.Exists(filePath))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<ReactWithDotNetDesignerModel>(File.ReadAllText(filePath));
        }
        catch (Exception)
        {
            return null;
        }
    }

    public static ReactWithDotNetDesignerModel TryRead(TypeReference typeReference)
    {
        var filePath = GetCacheFilePath(GetFileName(typeReference));
        if (!File.Exists(filePath))
        {
            return null;
        }

        try
        {
            return JsonSerializer.Deserialize<ReactWithDotNetDesignerModel>(File.ReadAllText(filePath));
        }
        catch (Exception)
        {
            return null;
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