using System;
using System.IO;
using System.Text.Json;

namespace ReactWithDotNet.UIDesigner;

class StateCache
{

    public static UIDesignerModel ReadState()
    {
        if (File.Exists(@"d:\\temp\\UIDesignerModel.json"))
        {
            var json = File.ReadAllText(@"d:\\temp\\UIDesignerModel.json");

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

    static readonly object fileLock = new();
    public static  void SaveState(object state)
    {
        lock (fileLock)
        {
            File.WriteAllText(@"d:\\temp\\UIDesignerModel.json", JsonSerializer.Serialize(state));
        }
    }
}