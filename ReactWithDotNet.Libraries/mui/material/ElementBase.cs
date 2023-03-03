

namespace ReactWithDotNet.Libraries.mui.material;

public class ElementBase : ThirdPartyReactComponent
{
    [React]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
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

static class DoNotSendToClientWhenEmpty
{
    public static TransformValueInServerSideResponse Transform(object value, TransformValueInServerSideContext transformContext)
    {
        var expandoObject = value as IDictionary<String, object>;

        if (expandoObject == null || expandoObject.Count == 0)
        {
            return new(false);
        }

        return new(needToExport: true, value);
    }
}