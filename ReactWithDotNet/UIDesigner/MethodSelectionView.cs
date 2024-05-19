using System.Collections.Immutable;
using static ReactWithDotNet.UIDesigner.Extensions;

namespace ReactWithDotNet.UIDesigner;

sealed record MetadataNode
{
    public ImmutableList<MetadataNode> Children { get; init; } = ImmutableList.Create<MetadataNode>();

    public bool IsClass { get; init; }
    public bool IsMethod { get; init; }
    public bool IsNamespace { get; init; }

    public string label { get; set; }

    public MethodReference MethodReference { get; init; }

    public string NamespaceReference { get; init; }
    public TypeReference TypeReference { get; init; }

    public bool HasChild => Children.Count > 0;
}

sealed class MethodSelectionView : Component
{
    public required string AssemblyFilePath { get; init; }

    public required string ClassFilter { get; init; }

    public required string MethodFilter { get; init; }

    public required string SelectedMethodTreeNodeKey { get; init; }

    [CustomEvent]
    public required Func<string, Task> SelectionChanged { get; init; }

    public static MetadataNode FindTreeNode(string assemblyFilePath, string treeNodeKey, string classFilter, string methodFilter)
    {
        if (string.IsNullOrWhiteSpace(assemblyFilePath) || string.IsNullOrWhiteSpace(treeNodeKey))
        {
            return null;
        }

        if (!File.Exists(assemblyFilePath))
        {
            return null;
        }

        var nodes = MetadataHelper.GetMetadataNodes(assemblyFilePath, classFilter, methodFilter).ToArray();

        return FindTreeNode(nodes, x => HasMatch(x, treeNodeKey));
    }

    protected override Element render()
    {
        var nodes = GetNodes().ToList();

        Element content;

        if (nodes.Count == 0)
        {
            content = new div(WordWrapBreakWord, Color("#c04747"), FontSize11)
            {
                $"No records found in assembly. {AssemblyFilePath}"
            };
        }
        else
        {
            content = AsTreeView(nodes);
        }

        return new div(HeightFull, MarginLeftRight(3), OverflowYScroll, CursorPointer, Padding(5), Border(Solid(1, "rgb(217, 217, 217)")), BorderRadius(3))
        {
            content
        };
    }

    static Element AsTreeItem(MetadataNode node, string SelectedMethodTreeNodeKey, MouseEventHandler OnTreeItemClicked)
    {
        if (node.IsMethod)
        {
            return new FlexRow(AlignItemsCenter)
            {
                new img { Src(GetSvgUrl("Method")), Size(11), MarginTop(5), MarginLeft(20) },

                new div { Text(node.label), MarginLeft(5), FontSize13 },

                Id(node.MethodReference.UUID),

                arrangeBackground
            };
        }

        if (node.IsClass)
        {
            return new FlexRow(AlignItemsCenter)
            {
                new img { Src(GetSvgUrl("Class")), Size(14), MarginLeft(10) },

                new div { Text(node.label), MarginLeft(5), FontSize13 },

                Id(node.TypeReference.FullName),

                arrangeBackground
            };
        }

        if (node.IsNamespace)
        {
            return new FlexRow(AlignItemsCenter)
            {
                new img { Src(GetSvgUrl("Namespace")), Size(14) },

                new div { Text(node.label), MarginLeft(5), FontSize13 }
            };
        }

        return new div();

        void arrangeBackground(HtmlElement el)
        {
            var isSelected = HasMatch(node, SelectedMethodTreeNodeKey);

            if (isSelected)
            {
                el += BackgroundImage(linear_gradient(90, rgb(136, 195, 242), rgb(242, 246, 249))) + BorderRadius(3);
            }
            else
            {
                el += Hover(BackgroundImage(linear_gradient(90, rgb(190, 220, 244), rgb(242, 246, 249))) + BorderRadius(3));
            }

            el.onClick = OnTreeItemClicked;
        }
    }

    static MetadataNode FindTreeNode(IEnumerable<MetadataNode> nodes, Func<MetadataNode, bool> hasMatch)
    {
        if (nodes == null)
        {
            return null;
        }

        foreach (var childNode in nodes)
        {
            var found = FindTreeNode(childNode, hasMatch);
            if (found is not null)
            {
                return found;
            }
        }

        return null;
    }

    static MetadataNode FindTreeNode(MetadataNode node, Func<MetadataNode, bool> hasMatch)
    {
        if (node == null)
        {
            return null;
        }

        if (hasMatch(node))
        {
            return node;
        }

        return FindTreeNode(node.Children, hasMatch);
    }

    static bool HasMatch(MetadataNode node, string treeNodeKey)
    {
        if (node.IsClass)
        {
            return node.TypeReference.FullName == treeNodeKey;
        }

        if (node.IsMethod)
        {
            return node.MethodReference.UUID == treeNodeKey;
        }

        return false;
    }

    Element AsTreeItem(MetadataNode node)
    {
        return AsTreeItem(node, SelectedMethodTreeNodeKey, OnTreeItemClicked);
    }

    Element AsTreeView(IEnumerable<MetadataNode> nodes)
    {
        return new Fragment
        {
            nodes.Select(toItem)
        };

        Element toItem(MetadataNode node)
        {
            if (node.HasChild)
            {
                var parent = AsTreeItem(node);
                var childrenOfParent = AsTreeView(node.Children);

                return new Fragment
                {
                    parent + OnClick(OnTreeItemClicked),
                    childrenOfParent
                };
            }

            return AsTreeItem(node);
        }
    }

    IEnumerable<MetadataNode> GetNodes()
    {
        if (!string.IsNullOrWhiteSpace(AssemblyFilePath) && File.Exists(AssemblyFilePath))
        {
            return MetadataHelper.GetMetadataNodes(AssemblyFilePath, ClassFilter, MethodFilter);
        }

        return new List<MetadataNode>();
    }

    Task OnTreeItemClicked(MouseEvent e)
    {
        DispatchEvent(SelectionChanged, [e.currentTarget.id]);

        return Task.CompletedTask;
    }
}