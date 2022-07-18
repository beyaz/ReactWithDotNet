using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ReactWithDotNet.PrimeReact;

[Serializable]
public class TreeNode
{
    public string key { get; set; }
    public string label { get; set; }
    public string data { get; set; }
    public string icon { get; set; }
    public List<TreeNode> children { get; } = new List<TreeNode>();
}

[Serializable]
public sealed class SingleSelectionTreeSelectionParams
{
    public string value { get; set; }
}

public class Tree : ElementBase
{
}

public class SingleSelectionTree : Tree
{
    [JsonPropertyName("$type")]
    public override string Type => GetType().FullName?.Replace(nameof(SingleSelectionTree), nameof(Tree));

    /// <summary>
    ///     Selected value to display.
    /// </summary>
    [React]
    public IEnumerable<TreeNode> value { get; set; }

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
    [ReactTemplate]
    public Func<TreeNode, Element> nodeTemplate { get; set; }

    [React]
    public Action<SingleSelectionTreeSelectionParams> onSelectionChange { get; set; }

    [React]
    public string selectionKeys { get; set; }

    [React]
    public string selectionMode { get; set; } = "single";

    internal List<KeyValuePair<object, object>> GetItemTemplates(Func<object, IReadOnlyDictionary<string, object>> toMap)
    {
        initializeKeys(value);

        var nodeList = toList(value);

        var map = new List<KeyValuePair<object, object>>();

        foreach (var node in nodeList)
        {
            map.Add(new KeyValuePair<object, object>(node, toMap(node)));
        }

        return map;

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