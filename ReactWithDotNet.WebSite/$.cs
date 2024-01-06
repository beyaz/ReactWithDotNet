namespace ReactWithDotNet.__designer__; // ReSharper disable All

static class Designer
{
    public static IReadOnlyList<DesignerElementInfo> GetStyle(Type type)
    {
        return new DesignerElementInfo[]
        {
            new()
            {
                TargetType = typeof(ReactWithDotNet.WebSite.Components.ElementTreeTestcomponent),

                VisualTreePath = [0],

                Modifiers = new[]
                {
                    Background("green"),
                    Padding(0)
                }
            }
        };
    }
}

