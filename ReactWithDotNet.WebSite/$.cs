namespace ReactWithDotNet.__designer__; // ReSharper disable All

static class Designer
{
    public static IReadOnlyList<DesignerComponentInfo> GetStyle(Type type)
    {
        return new DesignerComponentInfo[]
        {
            new()
            {
                TargetType = typeof(ReactWithDotNet.WebSite.Components.ElementTreeTestcomponent),

                VisualTree =
                [
                    new ()
                    {
                        VisualTreePath = [0],

                        Modifiers = new[]
                        {
                            Background("green"),
                            Padding(0)
                        }
                    }
                ]
            }
        };
    }
}

