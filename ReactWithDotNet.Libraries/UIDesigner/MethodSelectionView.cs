using System.IO;
using ReactWithDotNet.PrimeReact;
using static ReactWithDotNet.UIDesigner.Extensions;

namespace ReactWithDotNet.UIDesigner;

class MetadataNode : TreeNode
{
    public string DeclaringTypeFullName { get; set; }
    public string DeclaringTypeNamespaceName { get; set; }
    public string FullNameWithoutReturnType { get; set; }
    public bool IsClass { get; set; }
    public bool IsMethod { get; set; }
    public bool IsNamespace { get; set; }
    public int MetadataToken { get; set; }
    public string Name { get; set; }
    public string NamespaceName { get; set; }
}

class MethodSelectionModel
{
    public string Filter { get; set; }
}

class MethodSelectionView : ReactComponent<MethodSelectionModel>
{
    public string AssemblyFilePath { get; set; }
    
    public string Filter { get; set; }

    public string SelectedMethodTreeNodeKey { get; set; }

    [ReactCustomEvent]
    public Action<(string value, string filter)> SelectionChanged { get; set; }

    public static MetadataNode FindTreeNode(string AssemblyFilePath, string treeNodeKey)
    {
        if (string.IsNullOrWhiteSpace(AssemblyFilePath) || string.IsNullOrWhiteSpace(treeNodeKey))
        {
            return null;
        }

        if (!File.Exists(AssemblyFilePath))
        {
            return null;
        }

        var nodes = MetadataHelper.GetMetadataNodes(AssemblyFilePath).ToArray();

        MetadataNode current = null;
        foreach (var index in treeNodeKey.Split('|').Select(int.Parse))
        {
            current = nodes[index];
            nodes   = current.children.Select(x => (MetadataNode)x).ToArray();
        }

        return current;
    }

    protected override void constructor()
    {
        state = new MethodSelectionModel { Filter = Filter };
    }

    protected override Element render()
    {
        return new div(Padding(3))
        {
          
            new SingleSelectionTree<MetadataNode>
            {
                filterValueBind   = () => state.Filter,
                filter            = true,
                filterBy          = nameof(MetadataNode.Name),
                filterPlaceholder = "Search react components or methods which returns Element",
                nodeTemplate      = nodeTemplate,
                value             = GetNodes(),
                onSelectionChange = OnSelectionChanged,
                selectionKeys     = SelectedMethodTreeNodeKey,
                style             = { MaxHeight(250), OverflowScroll, PrimaryBackground },
            },

              new style{Text($@"

.p-tree-toggler-icon{{ {new Style { FontSize11 }.ToCssWithImportant()} }}

.p-tree-toggler{{ {new Style { MarginRight("0.3rem"), wh(11) }.ToCssWithImportant()} }}

.p-tree-filter{{ {new Style{ FontSize13 } }  }}

") }
        };
    }

    static Element nodeTemplate(MetadataNode node)
    {
        if (node.IsMethod)
        {
            return new FlexRow(AlignItemsCenter)
            {
                new img { Src(GetSvgUrl("Method")), wh(14), mt(5) },

                new div {Text(node.FullNameWithoutReturnType), MarginLeft(5), FontSize13 }
            };
        }

        if (node.IsClass)
        {
            return new FlexRow(AlignItemsCenter)
            {
                new img { Src(GetSvgUrl("Class")), wh(14) },

                new div {Text(node.Name), MarginLeft(5), FontSize13 }
            };
        }

        if (node.IsNamespace)
        {
            return new FlexRow(AlignItemsCenter)
            {
                new img { Src(GetSvgUrl("Namespace")), wh(14) },

                new div {Text(node.Name), MarginLeft(5), FontSize13 }
            };
        }

        return new div();
    }

    IEnumerable<MetadataNode> GetNodes()
    {
        if (!string.IsNullOrWhiteSpace(AssemblyFilePath) && File.Exists(AssemblyFilePath))
        {
            return MetadataHelper.GetMetadataNodes(AssemblyFilePath);
        }

        return new List<MetadataNode>();
    }

    void OnSelectionChanged(SingleSelectionTreeSelectionParams e)
    {
        DispatchEvent(() => SelectionChanged, (e.value, state.Filter));
    }
}