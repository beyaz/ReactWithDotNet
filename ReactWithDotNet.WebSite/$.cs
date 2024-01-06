namespace ReactWithDotNet.__designer__; // ReSharper disable All

static class Designer
{
    public static List<(List<int> treePath, List<StyleModifier> modifiers)> GetStyle(Type type)
    {
        if (type == typeof(ReactWithDotNet.WebSite.Components.GetStartedButton))
        {
            return 
            [
                ([0],
                [
                    TextAlignCenter,
                    DisplayBlock,
                    BorderRadius(10),
                    Hover(BorderRadius(5))
                ])
            ];
        }
        
        return null;
    }
}