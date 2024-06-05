using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ReactWithDotNet.UIDesigner;

static class StateCache
{
    static readonly string CacheDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ReactWithDotNetDesigner") +
                                            Path.DirectorySeparatorChar +
                                            (Assembly.GetEntryAssembly()?.GetName().Name ?? "UnknowAssembly") +
                                            Path.DirectorySeparatorChar;

    static readonly object fileLock = new();
    static string StateFilePath => Path.Combine(CacheDirectory, $@"{nameof(ReactWithDotNetDesignerModel)}.json");

    public static ReactWithDotNetDesignerModel ReadState()
    {
        if (File.Exists(StateFilePath))
        {
            var json = File.ReadAllText(StateFilePath);

            try
            {
                var state = JsonSerializer.Deserialize<ReactWithDotNetDesignerModel>(json);
                if (state is not null)
                {
                    if (state.SelectedMethod is not null)
                    {
                        return TryRead(state.SelectedMethod) ?? state;
                    }

                    if (state.SelectedType is not null)
                    {
                        return TryRead(state.SelectedType) ?? state;
                    }
                }

                return state;
            }
            catch (Exception)
            {
                return new ReactWithDotNetDesignerModel();
            }
        }

        return null;
    }

    public static Task Save(ReactWithDotNetDesignerModel state)
    {
        lock (fileLock)
        {
            var jsonContent = JsonSerializer.Serialize(state, new JsonSerializerOptions
            {
                WriteIndented    = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            });

            return WriteAllText(StateFilePath, jsonContent);
        }
    }

    public static Task Save(TypeReference typeReference, ReactWithDotNetDesignerModel state)
    {
        var jsonContent = JsonSerializer.Serialize(state, new JsonSerializerOptions
        {
            WriteIndented          = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        return SaveToFile(GetFileName(typeReference), jsonContent);
    }

    public static Task Save(MethodReference methodReference, ReactWithDotNetDesignerModel state)
    {
        var jsonContent = JsonSerializer.Serialize(state, new JsonSerializerOptions
        {
            WriteIndented          = true,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
        });

        return SaveToFile(GetFileName(methodReference), jsonContent);
    }

    public static Task SaveToFile(string fileNameWithoutExtension, string jsonContent)
    {
        lock (fileLock)
        {
            return WriteAllText(GetCacheFilePath(fileNameWithoutExtension), jsonContent);
        }
    }

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

    static string GetCacheFilePath(string type) => $@"{CacheDirectory}{type}.json";

    static string GetFileName(MethodReference methodReference) => methodReference.ToString().GetHashString();
    static string GetFileName(TypeReference typeReference) => typeReference.ToString().GetHashString();

    static Task WriteAllText(string path, string contents)
    {
        var directoryName = Path.GetDirectoryName(path);

        if (!string.IsNullOrWhiteSpace(directoryName))
        {
            if (!Directory.Exists(directoryName))
            {
                Directory.CreateDirectory(directoryName);
            }
        }

        return File.WriteAllTextAsync(path, contents);
    }

}