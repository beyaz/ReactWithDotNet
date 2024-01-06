using System.Reflection;

namespace ReactWithDotNet;

public sealed class DesignerComponentInfo
{
    public Type TargetType { get; set; }
    
    public IReadOnlyList<DesignerElementInfo> VisualTree { get; set; }
}

public sealed class DesignerElementInfo
{
    public IReadOnlyList<int> VisualTreePath { get; set; }
    
    public IReadOnlyList<StyleModifier> Modifiers { get; set; }
}

static class DesignerHelper
{
    public static void Override(Type componentType, Element rootNode)
    {
        var designerType = componentType.Assembly.GetType("ReactWithDotNet.__designer__.Designer");
        if (designerType == null)
        {
            return;
        }
        

        var getStyleMethodInfo = designerType.GetMethod("GetStyle", BindingFlags.Static | BindingFlags.Public);

        if (getStyleMethodInfo == null)
        {
            throw new MissingMethodException("GetStyle");
        }
        

        var records = (IReadOnlyList<DesignerComponentInfo>)getStyleMethodInfo.Invoke(null, [componentType]);
        if (records == null)
        {
            return;
        }
        
        var record = records.FirstOrDefault(x => x.TargetType == componentType);
        if (record == null)
        {
            return;
        }
        
        
        foreach (var item in record.VisualTree)
        {
            
        }
    }
    
}