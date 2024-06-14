//using System.Collections.Immutable;
//using System.Xml.Linq;

//namespace ReactWithDotNet.WebSite.Components;

//sealed record PropertyGridNode
//{
//    public string Label { get; init; }
    
//    public string Value { get; init; }
    
//    public string Editor { get; init; }

//    public ImmutableList<PropertyGridNode> Children { get; init; } = ImmutableList<PropertyGridNode>.Empty;
    
//}

//sealed class PropertyGridView : Component
//{
//    public PropertyGridNode RootNode { get; init; }

//    protected override Element render()
//    {
//        var node = RootNode;
//        if (DesignMode)
//        {
//            node = new()
//            {
//                Label  = "A",
//                Value  = "ValueA",
//                Editor = "string",
//                Children = new PropertyGridNode []
//                {
//                    new ()
//                    {
//                        Label  = "C1",
//                        Value  = "C1Val",
//                        Editor = "string"
//                    },
//                    new ()
//                    {
//                        Label  = "C2",
//                        Value  = "Value2",
//                        Editor = "string"
//                    },
//                    new ()
//                    {
//                        Label  = "Users",
//                        Children =  new PropertyGridNode []
//                        {
//                            new ()
//                            {
//                                Label  = "0",
//                                Children = new PropertyGridNode[]
//                                {
//                                    new ()
//                                    {
//                                        Label  = "C1",
//                                        Value  = "C1Val",
//                                        Editor = "string"
//                                    },
//                                    new ()
//                                    {
//                                        Label  = "C2",
//                                        Value  = "Value2",
//                                        Editor = "string"
//                                    },
//                                }.ToImmutableList()
//                            },
//                            new ()
//                            {
//                                Label  = "1",
//                                Children = new PropertyGridNode[]
//                                {
//                                    new ()
//                                    {
//                                        Label  = "C3",
//                                        Value  = "C1Val",
//                                        Editor = "string"
//                                    },
//                                    new ()
//                                    {
//                                        Label  = "C2",
//                                        Value  = "Value2",
//                                        Editor = "string"
//                                    },
//                                }.ToImmutableList()
//                            }
                            
                            
//                        }.ToImmutableList()
//                    }
//                }.ToImmutableList()
//            };
//        }

//        return CreateNodeView(node);
//    }

//    static Element CreateNodeView(PropertyGridNode Node)
//    {
//        var isCollapsed = true;
        
        
         
            
//        if (Node.Children.Any())
//        {
            
//            return FC(cmp =>
//            {

                
//                return new fieldset(Padding(8), Background("white"))
//                {
//                    Border(0.5, "dotted","#d9d9d9"),
//                    BorderRadius(4),
                
//                    isCollapsed ? OnMouseEnter(onMouseEnter) : null,
//                    new legend(DisplayFlexRow, AlignItemsCenter, PaddingLeftRight(1), FontSize12, FontWeight600)
//                    {
//                        Node.Label, new ArrowUpDownIcon { IsArrowUp = isCollapsed , Size = 16} 
//                        ,OnClick(toggleCollapse)
//                    },
                
//                    new FlexColumn(Gap(4) , isCollapsed ? DisplayNone : DisplayFlexColumn)
//                    {
//                        Node.Children.Select(CreateNodeView)
//                    }
//                };
                
//                Task toggleCollapse(MouseEvent e)
//                {
//                    isCollapsed = !isCollapsed;
                    
//                    return Task.CompletedTask;
//                }
                
//                [StopPropagation]
//                Task onMouseEnter(MouseEvent e)
//                {
//                    isCollapsed = false;
                    
//                    return Task.CompletedTask;
//                }
//        });
//        }
        
//        return new FlexColumn
//        {
//            new label{ Node.Label, FontSize12, FontWeight600, Color("blue")},
            
//            StringEditor("ABC2", Node.Value)
//        };
        
        
        
       
//    }
//    static Element StringEditor(string Path, string Value)
//    {
//        var value = Value;

//        return FC(cmp =>
//        {
//            return new input
//            {
//                type                     = "text", 
//                valueBind = () => value, valueBindDebounceTimeout = 500,
//                valueBindDebounceHandler = onKeypressFinished,
//                style =
//                {
//                    FontSize12, Padding(4), Border(Solid(0.5, "#ced4da")), Focus(OutlineNone), BorderRadius(2), Color("#495057")
//                }
//            };

//            Task onKeypressFinished()
//            {
//                return Task.CompletedTask;
//            }
//        });
//    }
//}