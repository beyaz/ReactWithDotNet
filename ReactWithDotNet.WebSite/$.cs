namespace ReactWithDotNet.__designer__; // ReSharper disable All

static class Designer
{
    public static IReadOnlyList<DesignerComponentInfo> ComponentInformationList =>
    [
        new()
        {
            TargetType = typeof(ReactWithDotNet.WebSite.Components.ElementTreeTestcomponent),

            VisualTree =
            [
                new()
                {
                    VisualTreePath = [0],

                    Modifiers = new[]
                    {
                        Background("green"),
                        PaddingLeft(0)
                    }
                }
            ]
        }
    ];
}

