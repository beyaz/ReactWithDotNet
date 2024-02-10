using System.Reflection;

namespace ReactWithDotNet;

static class ReflectionCache
{
    static readonly Dictionary<Assembly, AssemblyNode> AssemblyCache = [];

    public static MethodInfoNode Cached(this MethodInfo methodInfo)
    {
        var assemblyNode = Get(methodInfo.Module.Assembly);

        if (!assemblyNode.Methods.TryGetValue(new(methodInfo), out var node))
        {
            node = new(methodInfo)
            {
                Calculated = MethodInfoCalculated.From(methodInfo)
            };

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

        public MethodInfoCalculated Calculated { get;  set; }

        

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

sealed class MethodInfoCalculated
{
    // todo: cache more accessed values
    
    public required MethodInfo MethodInfo { get; init; }
    public required bool HasStopPropagationAttribute { get; init; }
    public required IReadOnlyList<string> KeyboardEventCallOnlyAttribute { get; init; }
    public required string NameWithToken { get; init; }
    
    public static MethodInfoCalculated From(MethodInfo methodInfo)
    {
        return new()
        {
            MethodInfo                     = methodInfo,
            HasStopPropagationAttribute    = methodInfo.GetCustomAttributes<ReactStopPropagationAttribute>().Any(),
            KeyboardEventCallOnlyAttribute = methodInfo.GetCustomAttributes<ReactKeyboardEventCallOnlyAttribute>().FirstOrDefault()?.Keys,
            NameWithToken                  = methodInfo.GetNameWithToken(),
        };
    }
}

partial class Mixin
{
    internal static MethodInfoCalculated GetCalculated(this MethodInfo methodInfo)
    {
        return methodInfo.Cached().Calculated;
    }
}