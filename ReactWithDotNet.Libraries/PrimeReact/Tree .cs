
using System.Collections;
using ReactWithDotNet.PrimeReact;

namespace ReactWithDotNet.Libraries.PrimeReact;





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
    [React]
    public IEnumerable<TTreeNode> value { get; set; }

    /// <summary>
    ///     When specified, displays an input field to filter the items.
    /// </summary>
    [React]
    public bool filter { get; set; }

    /// <summary>
    ///     When filtering is enabled, the value of input field.
    /// </summary>
    [React]
    public string filterValue { get; set; }


    [React]
    [ReactBind(targetProp = nameof(filterValue), jsValueAccess = "e.value", eventName = "onFilterValueChange")]
    public Expression<Func<string>> filterValueBind { get; set; }


    

    /// <summary>
    ///     Placeholder text to show when filter input is empty.
    /// </summary>
    [React]
    public string filterPlaceholder { get; set; }

    /// <summary>
    ///     When filtering is enabled, filterBy decides which field or fields (comma separated) to search against.
    ///     <para>Default: label</para>
    /// </summary>
    [React]
    public string filterBy { get; set; }

    [React]
    [ReactTemplate(nameof(GetItemSourceForCalculatingTemplates))]
    public Func<TTreeNode, Element> nodeTemplate { get; set; }

    [React]
    [ReactGrabEventArgumentsByUsingFunction(Prefix + GrabOnlyValueParameterFromCommonPrimeReactEvent)]
    public Action<SingleSelectionTreeSelectionParams> onSelectionChange { get; set; }

    [React]
    public string selectionKeys { get; set; }

    /// <summary>
    /// An array of keys to represent the state of the tree expansion state in controlled mode.
    /// </summary>
    [React]
    public IEnumerable<string> expandedKeys { get; set; }

    [React]
    public string selectionMode { get; set; } = "single";

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
                foreach (var child in treeNode.children)
                {
                    pushToArray(child, items);
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

                var i = 0;
                foreach (var child in treeNode.children)
                {
                    initializeKey(child, $"{key}|{i++}");
                }
            }
        }
    }
}