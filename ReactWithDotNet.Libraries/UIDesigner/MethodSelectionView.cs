using System.Collections.Generic;
using ReactWithDotNet.PrimeReact;

namespace ReactWithDotNet.UIDesigner;

class MetadataNode:TreeNode
{
    public string Name { get; set; }
    public bool IsNamespace { get; set; }
    public bool IsClass { get; set; }
    public bool IsMethod { get; set; }
    public string FullNameWithoutReturnType { get; set; }
    public int MetadataToken { get; set; }
}

class MethodSelectionViewModel
{
    public string AssemblyFilePath { get; set; }

    public string SelectedTreeNodeKeys { get; set; }
}

class MethodSelectionView : ReactComponent<MethodSelectionViewModel>
{
    public MethodSelectionView()
    {
        state = new MethodSelectionViewModel();
    }

    IEnumerable<MetadataNode> GetNodes()
    {
        if (!string.IsNullOrWhiteSpace(state.AssemblyFilePath))
        {
            return MetaDataHelper.GetMetadataNodes(state.AssemblyFilePath);
        }

        return new List<MetadataNode>();
    }


    public void ComponentDidMount()
    {
        Context.ClientTasks = new[] { new ClientTaskListenEvent { EventName = Events.AssemblyChanged, RouteToMethod = nameof(OnAssemblyFileChanged) } };
    }

    public void OnAssemblyFileChanged(string assemblyFilePath)
    {
        state.AssemblyFilePath = assemblyFilePath;
    }


    Element nodeTemplate(MetadataNode node)
    {

        if (node.IsMethod)
        {
            return new HPanel
            {
                new img { src                  = "img/Method.svg", width = 20, height = 20 },
                new div(node.FullNameWithoutReturnType) { style = { marginLeft = "5px" } },
                //new div(node.FullName) { style = { marginLeft = "5px", fontSize = "10px"} }
            };
        }
        
        if (node.IsClass == true)
        {
            return new HPanel
            {
                new img { src              = "img/Class.svg", width = 20, height = 20 }, 
                new div(node.Name) { style = { marginLeft = "5px" } }
            };
        }

        if (node.IsNamespace == true)
        {
            return new HPanel
            {
                new img { src              = "img/Namespace.svg", width = 20, height = 20 },
                new div(node.Name) { style = { marginLeft = "5px" } }
            };
        }

        return new HPanel
        {
            new img { src            = "img/Namespace.svg", width = 20, height = 20 },
            new div("aloha") { style = { marginLeft = "5px" } }
        };
    }

    public override Element render()
    {
        return new div
        {
            style = { display = "flex", flexDirection = "column"},
            children =
            {
                new SingleSelectionTree<MetadataNode>
                {
                    
                    filter            = true,
                    filterBy          = nameof(MetadataNode.Name),
                    filterPlaceholder = "Search Method",
                    nodeTemplate      = nodeTemplate,
                    value             = GetNodes(),
                    onSelectionChange = e => { state.SelectedTreeNodeKeys = e.value; },
                    selectionKeys     = state.SelectedTreeNodeKeys
                }

            }
        };



        


        
    }
}