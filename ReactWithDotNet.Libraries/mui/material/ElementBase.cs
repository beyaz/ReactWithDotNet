using System.Dynamic;

namespace ReactWithDotNet.Libraries.mui.material;

public class ElementBase : ThirdPartyReactComponent
{
    [React]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceNullWhenEmpty")]
    public dynamic sx { get; } = new ExpandoObject();
}

static class convert_mui_style_map_to_class_map
{
    public static TransformValueInServerSideResponse Transform(object value, TransformValueInServerSideContext transformContext)
    {
        var dictionary = value as Dictionary<string, Style>;

        if (dictionary == null || dictionary.Count == 0)
        {
            return new(false);
        }

        var map = new Dictionary<string, string>();

        foreach (var (key, style) in dictionary)
        {
            map.Add(key, transformContext.ConvertStyleToCssClass(style));
        }

        return new(true, map);
    }
}