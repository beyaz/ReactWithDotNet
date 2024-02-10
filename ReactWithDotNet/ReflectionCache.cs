using System.Reflection;

namespace ReactWithDotNet;

static class ReflectionCache
{
    static readonly Dictionary<Assembly, AssemblyNode> AssemblyCache = [];

    public static MethodInfoNode Get(MethodInfo methodInfo)
    {
        var assemblyNode = Get(methodInfo.Module.Assembly);

        if (!assemblyNode.Methods.TryGetValue(new(methodInfo), out var node))
        {
            node = new(methodInfo);

            node.Initialize();

            assemblyNode.Methods.Add(node);
        }

        return node;
    }

    static AssemblyNode Get(Assembly assembly)
    {
        if (!AssemblyCache.TryGetValue(assembly, out var assemblyNode))
        {
            assemblyNode = new();

            AssemblyCache.Add(assembly, assemblyNode);
        }

        return assemblyNode;
    }

    internal class MethodInfoNode
    {
        internal static IComparer<MethodInfoNode> ComparerInstance = new Comparer();
        readonly MethodInfo _methodInfo;

        public MethodInfoNode(MethodInfo methodInfo)
        {
            _methodInfo = methodInfo;
        }

        public bool HasStopPropagationAttribute { get; private set; }
        public IReadOnlyList<string> KeyboardEventCallOnlyAttribute { get; private set; }

        public void Initialize()
        {
            HasStopPropagationAttribute    = _methodInfo.GetCustomAttributes<ReactStopPropagationAttribute>().Any();
            KeyboardEventCallOnlyAttribute = _methodInfo.GetCustomAttributes<ReactKeyboardEventCallOnlyAttribute>().FirstOrDefault()?.Keys;
        }

        class Comparer : IComparer<MethodInfoNode>
        {
            public int Compare(MethodInfoNode x, MethodInfoNode y)
            {
                if (x == null || y == null)
                {
                    return 0;
                }

                return x._methodInfo.GetMetadataToken().CompareTo(y._methodInfo.GetMetadataToken());
            }
        }
    }

    class AssemblyNode
    {
        public readonly SortedSet<MethodInfoNode> Methods = new(MethodInfoNode.ComparerInstance);
    }
}