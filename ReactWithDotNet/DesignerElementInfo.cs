using System.Reflection;

namespace ReactWithDotNet;

public sealed class DesignerComponentInfo
{
    public Type TargetType { get; set; }

    public IReadOnlyList<ElementInfo> VisualTree { get; set; }
    
    public sealed class ElementInfo
    {
        public IReadOnlyList<int> VisualTreePath { get; set; }

        public List<string> Lines { get; set; }

        public Style CompiledLines { get; set; }
    }
}

static class DesignerHelper
{
    static readonly LinkedList<CacheItem> Cache = new();

    public static DesignerComponentInfo CreateNewDesignerComponentInfoForType(Type targetType)
    {
        DesignerComponentInfo newItem = new()
        {
            TargetType = targetType,
            VisualTree = []
        };
        
        var cacheKey = targetType.Assembly.GetName().Name;

        var node = Cache.FirstOrDefault(x => x.assemblyFullName == cacheKey);
        if (node == null)
        {
            throw new InvalidOperationException("// todo:?");
        }

        node.value = node.value.NewListWith(newItem);
        
        return newItem;
    }

    public static DesignerComponentInfo GetComponentInfo(Element component)
    {
        var componentType = component.GetType();

        var records = ReadFromAssembly(componentType.Assembly);
        if (records == null)
        {
            return null;
        }

        return records.FirstOrDefault(x => x.TargetType == componentType);
    }
    
    public static void Override(Element component, Element rootNode)
    {
        var record = GetComponentInfo(component);
        if (record == null)
        {
            return;
        }

        foreach (var designerElementInfo in record.VisualTree)
        {
            var node = rootNode;
            var i = 1;
            var len = designerElementInfo.VisualTreePath.Count;

            while (i < len)
            {
                var offset = designerElementInfo.VisualTreePath[i];

                if (node is null)
                {
                    break;
                }

                node = node._children[offset];

                i++;
            }

            if (i < len)
            {
                break;
            }

            ModifyHelper.ProcessModifier(node, designerElementInfo.CompiledLines);
        }
    }

    static IReadOnlyList<DesignerComponentInfo> ReadFromAssembly(Assembly assembly)
    {
        var cacheKey = assembly.GetName().Name;

        var item = Cache.FirstOrDefault(x => x.assemblyFullName == cacheKey);

        if (item == null)
        {
            item = new CacheItem(){assemblyFullName = cacheKey, value = ReadFromAssemblyNoCache(assembly)};

            Cache.AddLast(item);
        }

        return item.value;

        static IReadOnlyList<DesignerComponentInfo> ReadFromAssemblyNoCache(Assembly assembly)
        {
            var designerType = assembly.GetType("ReactWithDotNet.__designer__.Designer");
            if (designerType == null)
            {
                return [];
            }

            var componentInformationListPropertyInfo = designerType.GetProperty("ComponentInformationList", BindingFlags.Static | BindingFlags.Public);

            if (componentInformationListPropertyInfo == null)
            {
                throw new MissingMemberException("componentInformationListPropertyInfo");
            }

            return (IReadOnlyList<DesignerComponentInfo>)componentInformationListPropertyInfo.GetValue(null);
        }
    }

    
}

    class CacheItem
    {
        public string assemblyFullName;
        public IReadOnlyList<DesignerComponentInfo> value;
    }