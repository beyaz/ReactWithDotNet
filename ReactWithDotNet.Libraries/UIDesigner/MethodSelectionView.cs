using System.IO;
using ReactWithDotNet.Libraries.PrimeReact;
using static ReactWithDotNet.UIDesigner.Extensions;
namespace ReactWithDotNet.UIDesigner;

class MetadataNode : TreeNode
{
    public bool IsClass { get; set; }
    public bool IsMethod { get; set; }
    public bool IsNamespace { get; set; }
    
    public string NamespaceReference { get; set; }
    public MethodReference MethodReference { get; set; }
    public TypeReference TypeReference { get; set; }

    public List<MetadataNode> children { get; } = new();
}



class MethodSelectionView : ReactComponent
{
    public int Width { get; set; }
    
    public string AssemblyFilePath { get; set; }

    public string SelectedMethodTreeNodeKey { get; set; }

    [ReactCustomEvent]
    public Action<string> SelectionChanged { get; set; }

    public string ClassFilter { get; set; }
    
    public string MethodFilter { get; set; }

    public static MetadataNode FindTreeNode(string AssemblyFilePath, string treeNodeKey, string classFilter, string methodFilter)
    {
        if (string.IsNullOrWhiteSpace(AssemblyFilePath) || string.IsNullOrWhiteSpace(treeNodeKey))
        {
            return null;
        }

        if (!File.Exists(AssemblyFilePath))
        {
            return null;
        }

        var nodes = MetadataHelper.GetMetadataNodes(AssemblyFilePath, classFilter,methodFilter).ToArray();

        return SingleSelectionTree<MetadataNode>.FindNodeByKey(nodes, treeNodeKey);
    }

   

    protected override Element render()
    {
        var nodes = GetNodes().ToList();
       
        foreach (var namespaceNode in nodes.Take(2))
        {
            namespaceNode.expanded = true;

            foreach (var classNode in namespaceNode.children.Take(3))
            {
                classNode.expanded = true;
            }
        }
        
        var tree = new SingleSelectionTree<MetadataNode>
        {
            nodeTemplate      = nodeTemplate,
            value             = nodes,
            onSelectionChange = OnSelectionChanged,
            selectionKeys     = SelectedMethodTreeNodeKey,
            style             = { PrimaryBackground, Width(Width) },
        };

        var csscustomizeForTree = new style
        {
            Text($@"

.p-tree-toggler-icon{{ {new Style { FontSize11 }.ToCssWithImportant()} }}

.p-tree-toggler{{ {new Style { MarginRight("0.3rem"), wh(11) }.ToCssWithImportant()} }}

.p-tree-filter{{ {new Style { FontSize13 }}  }}

.custom .p-scrollpanel-bar{{ {new Style { BackgroundColor("#a6a6a6") }}  }}

.p-inputtext:enabled:focus{{box-shadow:none !important;}}

.p-tree-filter.p-inputtext.p-component{{ {new Style { Padding(5) }}  }}
.p-tree-filter-icon.pi.pi-search{{ {new Style { FontSize(15) }}  }}

.p-tree .p-tree-container .p-treenode .p-treenode-content:focus {{
  box-shadow: none;
}}

.p-tree .p-tree-container .p-treenode .p-treenode-content.p-highlight{{
background:#c8d3db !important;
}}

")
        };

        return new ScrollPanel
        {
            className = "custom",
            style =
            {
                Padding(3), Height(250), 
            },
            children = { tree, csscustomizeForTree }
        };
    }

    static Element nodeTemplate(MetadataNode node)
    {
        if (node.IsMethod)
        {
            return new FlexRow(AlignItemsCenter)
            {
                new img { Src(GetSvgUrl("Method")), wh(14), mt(5) },

                new div { Text(node.label), MarginLeft(5), FontSize13 }
            };
        }

        if (node.IsClass)
        {
            return new FlexRow(AlignItemsCenter)
            {
                new img { Src(GetSvgUrl("Class")), wh(14) },

                new div { Text(node.label), MarginLeft(5), FontSize13 }
            };
        }

        if (node.IsNamespace)
        {
            return new FlexRow(AlignItemsCenter)
            {
                new img { Src(GetSvgUrl("Namespace")), wh(14) },

                new div { Text(node.label), MarginLeft(5), FontSize13 }
            };
        }

        return new div();
    }

    IEnumerable<MetadataNode> GetNodes()
    {
        if (!string.IsNullOrWhiteSpace(AssemblyFilePath) && File.Exists(AssemblyFilePath))
        {
            return MetadataHelper.GetMetadataNodes(AssemblyFilePath, ClassFilter,MethodFilter);
        }

        return new List<MetadataNode>();
    }

    void OnSelectionChanged(SingleSelectionTreeSelectionParams e)
    {
        DispatchEvent(() => SelectionChanged, e.value);
    }
}