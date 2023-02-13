namespace ReactWithDotNet.Libraries.framer_motion;

public sealed class motion_div : ElementBase
{
    [React]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceNullWhenEmpty")]
    public animate animate { get; } = new();

    [React]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceNullWhenEmpty")]
    public initial initial { get; } = new();

    [React]
    [ReactTransformValueInClient("ReactWithDotNet::Core::ReplaceNullWhenEmpty")]
    public transition transition { get; } = new();
    

}
public sealed class animate
{
    public double? x { get; set; }
    public double? opacity { get; set; }
    public double? scale { get; set; }
    public double? rotate { get; set; }
    
}




public sealed class initial
{
    public double? opacity { get; set; }
    public double? scale { get; set; }
    public double? rotate { get; set; }
}


public sealed class transition
{
    public double? duration { get; set; }
}