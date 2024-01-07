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
                    Lines = 
                    [
                        "border-radius: 5",
                        "background: green",
                        ":hover",
                        "  border-radius: 3",
                        "  background: yellow",
                        
                    ],
                    CompiledLines = 
                    [
                        BorderRadius(5),
                        Background("green"),
                        Hover([
                            BorderRadius(3),
                            Background("yellow")
                        ])
                    ]
                }
            ]
        }
    ];
}

