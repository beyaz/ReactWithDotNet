namespace ReactWithDotNet.__designer__; // ReSharper disable All

static class Designer
{
    public static IReadOnlyList<DesignerElementInfo> GetStyle(Type type)
    {
        return new DesignerElementInfo[]
        {
            new DesignerElementInfo()
            {
                TargetType = typeof(ReactWithDotNet.WebSite.Components.ElementTreeTestcomponent),
                
                VisualTreePath = [0],
                
                Modifiers = new []
                {
                    Background("green")
                }
            }
        };
    }
}

public sealed class DesignerElementInfo
{
    public Type TargetType { get; set; }
    
    public IReadOnlyList<int> VisualTreePath { get; set; }
    
    public IReadOnlyList<StyleModifier> Modifiers { get; set; }
}
