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

                    Modifiers = new DesignerStyleModifierInfo[]
                    {
                        new()
                        {
                            StyleModifier = Background("green"),
                            Text = "background: green"
                        },
                        
                        new()
                        {
                            StyleModifier = PaddingLeft(0),
                            Text          = "padding-left: 0"
                        }
                    }
                }
            ]
        }
    ];
}

