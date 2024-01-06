namespace ReactWithDotNet;

public sealed class DesignerElementInfo
{
    public Type TargetType { get; set; }
    
    public IReadOnlyList<int> VisualTreePath { get; set; }
    
    public IReadOnlyList<StyleModifier> Modifiers { get; set; }
}