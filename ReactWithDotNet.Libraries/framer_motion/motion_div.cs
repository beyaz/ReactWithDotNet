using System.Dynamic;

namespace ReactWithDotNet.Libraries.framer_motion;

public sealed class motion_div : ElementBase
{
    [React]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceNullWhenEmpty")]
    public dynamic animate { get; } = new ExpandoObject();

    [React]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceNullWhenEmpty")]
    public dynamic initial { get; } = new ExpandoObject();

    [React]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceNullWhenEmpty")]
    public  dynamic transition { get; } = new ExpandoObject();
}