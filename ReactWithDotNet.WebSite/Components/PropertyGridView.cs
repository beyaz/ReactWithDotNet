using System.Collections.Immutable;
using System.Xml.Linq;

namespace ReactWithDotNet.WebSite.Components;

class DenemeA
{
    public string Prop1 { get; set; }
    public string Prop2 { get; set; }
    public int Prop3 { get; set; } = 5;
    public DenemeA_Nested NestedA { get; set; }
}

class DenemeA_Nested
{
    public string PropA { get; set; }
    public string PropB { get; set; }
    public int PropC { get; set; } = 5;
    
    public DenemeA_Nested_Nested Nested { get; set; }
}
class DenemeA_Nested_Nested
{
    public string PropX { get; set; }
    public string PropY { get; set; }
    public int PropZ { get; set; } = 5;
}

sealed record PropertyGridNode
{
    public string Label { get; init; }
    
    public string Value { get; init; }
    
    public string Editor { get; init; }

    public ImmutableList<PropertyGridNode> Children { get; init; } = ImmutableList<PropertyGridNode>.Empty;
    
}

sealed class PropertyGridView : Component
{
    public PropertyGridNode RootNode { get; init; }
    
    public object Instance { get; set; }

    protected override Element render()
    {
        if (DesignMode)
        {
            Instance = new DenemeA
            {
                Prop1 = "A",
                Prop2 = "g",
                Prop3 = 5
            };
        }

        return CreateNodeView(typeof(DenemeA),"root",Instance);
    }

    static Element CreateNodeView(Type type, string label, object Value)
    {
        if (type == typeof(string)||
            type == typeof(int))
        {
            return new FlexColumn
            {
                new label{ label, FontSize12, FontWeight600, Color("blue")},
            
                StringEditor("ABC2", Value?.ToString())
            };
        }
        
        
        var isCollapsed = true;
         
        return FC(cmp =>
        {
            return new fieldset(Padding(8), Background("white"))
            {
                Border(0.5, "dotted","#d9d9d9"),
                BorderRadius(4),
                
                isCollapsed ? OnMouseEnter(onMouseEnter) : null,
                new legend(DisplayFlexRow, AlignItemsCenter, PaddingLeftRight(1), FontSize12, FontWeight600)
                {
                    label, new ArrowUpDownIcon { IsArrowUp = isCollapsed , Size = 16} 
                    ,OnClick(toggleCollapse)
                },
                
                new FlexColumn(Gap(4) , isCollapsed ? DisplayNone : DisplayFlexColumn)
                {
                    type.GetProperties().Select(p=>CreateNodeView(p.PropertyType, p.Name,  Value is null ? null: p.GetValue(Value)))
                }
            };
                
            Task toggleCollapse(MouseEvent e)
            {
                isCollapsed = !isCollapsed;
                    
                return Task.CompletedTask;
            }
                
            [StopPropagation]
            Task onMouseEnter(MouseEvent e)
            {
                isCollapsed = false;
                    
                return Task.CompletedTask;
            }
        });
        
    }
    static Element StringEditor(string Path, string Value)
    {
        var value = Value;

        return FC(cmp =>
        {
            return new input
            {
                type                     = "text", 
                valueBind = () => value, valueBindDebounceTimeout = 500,
                valueBindDebounceHandler = onKeypressFinished,
                style =
                {
                    FontSize12, Padding(4), Border(Solid(0.5, "#ced4da")), Focus(OutlineNone), BorderRadius(2), Color("#495057")
                }
            };

            Task onKeypressFinished()
            {
                return Task.CompletedTask;
            }
        });
    }
}