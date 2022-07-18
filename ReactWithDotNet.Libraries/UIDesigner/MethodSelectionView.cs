using System;
using System.Collections.Generic;
using System.Linq;
using ReactWithDotNet.PrimeReact;

namespace ReactWithDotNet.UIDesigner;

class MetadataNode:TreeNode
{
    public string Name { get; set; }
    public bool IsNamespace { get; set; }
    public bool IsClass { get; set; }
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


    Element nodeTemplate(TreeNode nodee)
    {
        var node = nodee as MetadataNode;
        
        if (node?.IsClass == true)
        {
            return new HPanel
            {
                new img { src = "img/Class.svg", width = 30, height = 30 }, 
                new div(node.Name) { style = { marginLeft = "5px" } }
            };
        }

        if (node?.IsNamespace == true)
        {
            return new HPanel
            {
                new img { src = "img/Namespace.svg", width = 30, height = 30 },
                new div(node.Name) { style = { marginLeft = "5px" } }
            };
        }

        return new HPanel
        {
            new img { src = "img/Namespace.svg", width = 30, height = 30 },
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
                new SingleSelectionTree
                {
                    
                    filter            = true,
                    // filterBy          = nameof(MetadataNode.Name),
                    //filterValue          = nameof(MetadataNode.Name),
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