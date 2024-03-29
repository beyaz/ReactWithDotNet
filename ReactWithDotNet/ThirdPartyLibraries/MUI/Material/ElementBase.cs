﻿

namespace ReactWithDotNet.ThirdPartyLibraries.MUI.Material;

public class ElementBase : ThirdPartyReactComponent
{
    ///// <summary>
    /////     The system prop that allows defining system overrides as well as additional CSS styles.
    ///// </summary>
    //[ReactProp]
    //[ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    //public dynamic sx { get; } = new ExpandoObject();
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
        var expandoObject = value as IDictionary<string, object>;

        if (expandoObject == null || expandoObject.Count == 0)
        {
            return new(false);
        }

        return new(needToExport: true, value);
    }
}