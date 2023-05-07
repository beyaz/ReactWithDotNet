
using System.Collections;

namespace ReactWithDotNet.ThirdPartyLibraries.PrimeReact;


[Serializable]
public class TreeNode
{
    public string key { get; set; }
    public string label { get; set; }
    public string data { get; set; }
    public string icon { get; set; }
    public string className { get; set; }
    public bool expanded { get; set; }

    public Style style { get; } = new();


}


[Serializable]
public sealed class SingleSelectionTreeSelectionParams
{
    public string value { get; set; }
}

public abstract class Tree : ElementBase
{
}

[ReactRealType(typeof(Tree))]
public class SingleSelectionTree<TTreeNode> : Tree where TTreeNode: TreeNode, new()
{
    /// <summary>
    ///     Selected value to display.
    /// </summary>
    [ReactProp]
    public IEnumerable<TTreeNode> value { get; set; }

    /// <summary>
    ///     When specified, displays an input field to filter the items.
    /// </summary>
    [ReactProp]
    public bool filter { get; set; }

    /// <summary>
    ///     When filtering is enabled, the value of input field.
    /// </summary>
    [ReactProp]
    public string filterValue { get; set; }


    [ReactProp]
    [ReactBind(targetProp = nameof(filterValue), jsValueAccess = "e.value", eventName = "onFilterValueChange")]
    public Expression<Func<string>> filterValueBind { get; set; }


    

    /// <summary>
    ///     Placeholder text to show when filter input is empty.
    /// </summary>
    [ReactProp]
    public string filterPlaceholder { get; set; }

    /// <summary>
    ///     When filtering is enabled, filterBy decides which field or fields (comma separated) to search against.
    ///     <para>Default: label</para>
    /// </summary>
    [ReactProp]
    public string filterBy { get; set; }

    [ReactProp]
    [ReactTemplate(nameof(GetItemSourceForCalculatingTemplates))]
    public Func<TTreeNode, Element> nodeTemplate { get; set; }

    [ReactProp]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + GrabOnlyValueParameterFromCommonPrimeReactEvent)]
    public Action<SingleSelectionTreeSelectionParams> onSelectionChange { get; set; }

    [ReactProp]
    public string selectionKeys { get; set; }

    /// <summary>
    /// An array of keys to represent the state of the tree expansion state in controlled mode.
    /// </summary>
    [ReactProp]
    public IReadOnlyDictionary<string,bool> expandedKeys { get; set; }

    [ReactProp]
    public string selectionMode { get; set; } = "single";

    public static TTreeNode FindNodeByKey(IReadOnlyList<TTreeNode> nodes, string treeNodeKey)
    {
        if (nodes == null)
        {
            throw new ArgumentNullException(nameof(nodes));
        }

        if (treeNodeKey == null)
        {
            throw new ArgumentNullException(nameof(treeNodeKey));
        }

        TTreeNode current = null;
        foreach (var index in treeNodeKey.Split('|').Select(int.Parse))
        {
            if (nodes.Count <= index)
            {
                return null;
            }
            current = nodes[index];
            nodes   = As<TTreeNode>(TryGetChildren(current)).ToArray();
        }

        return current;
    }

    static IEnumerable<T> As<T>(IEnumerable enumerable)
    {
        if (enumerable != null)
        {
            foreach (var value in enumerable)
            {
                yield return (T)value;
            }
        }
        
        yield return default;
    }

    static IEnumerable TryGetChildren(TreeNode treeNode)
    {
        if (treeNode == null)
        {
            return null;
        }

        var propertyInfo = treeNode.GetType().GetProperty("children");
        if (propertyInfo == null)
        {
            return null;
        }

        return (IEnumerable)propertyInfo.GetValue(treeNode);
    }
    
    internal IEnumerable GetItemSourceForCalculatingTemplates()
    {
        initializeKeys(value);

        return toList(value);

        static IReadOnlyList<TreeNode> toList(IEnumerable<TreeNode> nodes)
        {
            var items = new List<TreeNode>();

            foreach (var node in nodes)
            {
                pushToArray(node, items);
            }

            return items;

            static void pushToArray(TreeNode treeNode, List<TreeNode> items)
            {
                items.Add(treeNode);
                var children = TryGetChildren(treeNode);
                if (children != null)
                {
                    foreach (TreeNode child in children)
                    {
                        pushToArray(child, items);
                    }
                }
                
            }
        }

        

        static void initializeKeys(IEnumerable<TreeNode> nodes)
        {
            var i = 0;
            foreach (var treeNode in nodes)
            {
                initializeKey(treeNode, i.ToString());
                i++;
            }

            static void initializeKey(TreeNode treeNode, string key)
            {
                treeNode.key = key;

                var children = TryGetChildren(treeNode);
                if (children != null)
                {
                    var i = 0;
                    foreach (TreeNode child in children)
                    {
                        initializeKey(child, $"{key}|{i++}");
                    }
                }
            }
        }
    }
}