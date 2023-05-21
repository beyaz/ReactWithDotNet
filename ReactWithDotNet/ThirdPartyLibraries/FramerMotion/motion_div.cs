namespace ReactWithDotNet.ThirdPartyLibraries.FramerMotion;

public sealed class motion_div : ElementBase
{
    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic animate { get; } = new ExpandoObject();

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic initial { get; } = new ExpandoObject();

    [ReactProp]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public  dynamic transition { get; } = new ExpandoObject();
}