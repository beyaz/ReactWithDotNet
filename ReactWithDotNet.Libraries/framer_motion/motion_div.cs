using System.Dynamic;

namespace ReactWithDotNet.Libraries.framer_motion;

public sealed class motion_div : ElementBase
{
    [React]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic animate { get; } = new ExpandoObject();

    [React]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public dynamic initial { get; } = new ExpandoObject();

    [React]
    [ReactTransformValueInClient(Core__ReplaceNullWhenEmpty)]
    public  dynamic transition { get; } = new ExpandoObject();
}