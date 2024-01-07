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
                    Medias = 
                    [
                        new ()
                        {
                            Pseudos = 
                            [
                                new ()
                                {
                                    Text = ":hover",
                                    Modifiers = 
                                    [
                                        new()
                                        {
                                            Text = "background: green",
                                
                                            StyleModifier = Background("green")
                                        },
                        
                                        new()
                                        {
                                            Text = "padding-left: 0",
                            
                                            StyleModifier = PaddingLeft(0)
                                        }
                                    ]
                                }
                            ]
                        }
                    
                    ],
                    
                }
            ]
        }
    ];
}

