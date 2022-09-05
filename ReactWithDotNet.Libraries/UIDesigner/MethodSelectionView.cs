using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using ReactWithDotNet.PrimeReact;

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

class MethodSelectionView 
{
    public Action<SingleSelectionTreeSelectionParams> OnSelectionChange { get; set; }

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
                new img { src                                   = "wwwroot/img/Method.svg", width = 20, height = 20 },
                new div(node.FullNameWithoutReturnType) { style = { marginLeft = "5px" } },
                //new div(node.FullName) { style = { marginLeft = "5px", fontSize = "10px"} }
            };
        }

        if (node.IsClass)
        {
            return new HStack
            {
                new img { src              = "wwwroot/img/Class.svg", width = 20, height = 20 },
                new div(node.Name) { style = { marginLeft = "5px" } }
            };
        }

        if (node.IsNamespace)
        {
            return new HStack
            {
                new img { src              = "wwwroot/img/Namespace.svg", width = 20, height = 20 },
                new div(node.Name) { style = { marginLeft = "5px" } }
            };
        }

        return new HStack
        {
            new img { src            = "wwwroot/img/Namespace.svg", width = 20, height = 20 },
            new div("aloha") { style = { marginLeft = "5px" } }
        };
    }

    public  Element render()
    {
        return new div
        {
            style = { padding = "3px" },
            children =
            {
                new SingleSelectionTree<MetadataNode>
                {
                    filter            = true,
                    filterBy          = nameof(MetadataNode.Name),
                    filterPlaceholder = "Search Method",
                    nodeTemplate      = nodeTemplate,
                    value             = GetNodes(),
                    onSelectionChange = OnSelectionChange,
                    selectionKeys     = SelectedMethodTreeNodeKey
                }
            }
        };
    }
}