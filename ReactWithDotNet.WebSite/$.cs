namespace ReactWithDotNet.__designer__; // ReSharper disable All

static class Designer
{
    public static List<(List<int> treePath, List<StyleModifier> modifiers)> GetStyle(Type type)
    {
        if (type == typeof(ReactWithDotNet.WebSite.Components.ElementTreeTestcomponent))
        {
            return 
            [
                ([0],
                [
                    Background("green")
                ])
            ];
        }
        
        return null;
    }
}