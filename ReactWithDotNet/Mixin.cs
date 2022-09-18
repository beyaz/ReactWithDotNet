using System.Text.Json;

namespace ReactWithDotNet;

public static class Mixin
{
    public static string ToJson(this ComponentResponse value)
    {
        var options = new JsonSerializerOptions();

        options = options.ModifyForReactWithDotNet();
            
        return JsonSerializer.Serialize(value, options);
    }

    public static JsonSerializerOptions ModifyForReactWithDotNet(this JsonSerializerOptions options)
    {
        return JsonSerializationOptionHelper.Modify(options);
    }

    public static TParent appendChild<TParent,TChild>(this TParent element, TChild child) where TParent : Element where TChild : Element
    {
        element.children.Add(child);

        return element;
    }
}