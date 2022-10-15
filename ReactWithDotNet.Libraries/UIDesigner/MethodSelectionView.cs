using System.IO;
using ReactWithDotNet.PrimeReact;
using static ReactWithDotNet.UIDesigner.Extensions;

namespace ReactWithDotNet.UIDesigner;

class MetadataNode : TreeNode
{
    public string Name { get; set; }
    public bool IsNamespace { get; set; }
    public bool IsClass { get; set; }
    public bool IsMethod { get; set; }
    public string FullNameWithoutReturnType { get; set; }
    public int MetadataToken { get; set; }
    public string NamespaceName { get; set; }
    public string DeclaringTypeFullName { get; set; }
    public string DeclaringTypeNamespaceName { get; set; }
}

class MethodSelectionModel
{
    public string Filter { get; set; }
}
class MethodSelectionView : ReactComponent<MethodSelectionModel>
{
    public string Filter { get; set; }
    protected override void constructor()
    {
        state = new MethodSelectionModel { Filter = Filter };
    }

    [ReactCustomEvent]
    public Action<(string value, string filter)> SelectionChanged { get; set; }

    void OnSelectionChanged(SingleSelectionTreeSelectionParams e)
    {
        DispatchEvent(()=> SelectionChanged,(e.value,state.Filter));
    }

    public string SelectedMethodTreeNodeKey { get; set; }

    public string AssemblyFilePath { get; set; }


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
            nodes   = current.children.Select(x=>(MetadataNode)x).ToArray();
        }

        return current;
    }
    
    IEnumerable<MetadataNode> GetNodes()
    {
        if (!string.IsNullOrWhiteSpace(AssemblyFilePath) && File.Exists(AssemblyFilePath))
        {
            return MetadataHelper.GetMetadataNodes(AssemblyFilePath);
        }

        return new List<MetadataNode>();
    }

    static Element nodeTemplate(MetadataNode node)
    {
        if (node.IsMethod)
        {
            return new HStack
            {
                new img { src = GetSvgUrl("Method"), width = 20, height = 20 },
                
                new div(node.FullNameWithoutReturnType) { style = { marginLeft = "5px" } }
            };
        }

        if (node.IsClass)
        {
            return new HStack
            {
                new img { src              = GetSvgUrl("Class"), width = 20, height = 20 },
                new div(node.Name) { style = { marginLeft = "5px" } }
            };
        }

        if (node.IsNamespace)
        {
            return new HStack
            {
                new img { src = GetSvgUrl("Namespace"), width = 20, height = 20 },
                
                new div(node.Name) { style = { marginLeft = "5px" } }
            };
        }

        return new HStack
        {
            new img { src = GetSvgUrl("Namespace"), width = 20, height = 20 },
            
            new div("aloha") { style = { marginLeft = "5px" } }
        };
    }

    protected  override Element render()
    {
        return new div
        {
            style = { padding = "3px" },
            children =
            {
                new SingleSelectionTree<MetadataNode>
                {
                    filterValueBind = ()=>state.Filter,
                    filter            = true,
                    filterBy          = nameof(MetadataNode.Name),
                    filterPlaceholder = "Search react components or methods which returns Element",
                    nodeTemplate      = nodeTemplate,
                    value             = GetNodes(),
                    onSelectionChange = OnSelectionChanged,
                    selectionKeys     = SelectedMethodTreeNodeKey,
                    style = { MaxHeight(250), OverflowScroll }
                }
            }
        };
    }
}