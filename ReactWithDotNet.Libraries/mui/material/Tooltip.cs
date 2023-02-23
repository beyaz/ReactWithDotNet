using ReactWithDotNet.Libraries.ReactWithDotNetSkeleton;

namespace ReactWithDotNet.Libraries.mui.material;

public sealed class Tooltip : ElementBase
{
    [React]
    public bool? arrow { get; set; }

    [React]
    public Element title { get; set; }

    [React]
    public string placement { get; set; }

    [React]
    [ReactTransformValueInServerSide(typeof(convert_mui_style_map_to_class_map))]
    public Dictionary<string, Style> classes { get; private set; } = new();

    protected override Element GetSuspenseFallbackElement()
    {
        return _children.FirstOrDefault() ?? new Skeleton();
    }
}