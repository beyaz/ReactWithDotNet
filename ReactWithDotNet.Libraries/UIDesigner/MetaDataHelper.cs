using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ReactWithDotNet.UIDesigner;

public  struct AssemblyReference
{
    public string Name { get; set; }
}
public struct TypeReference
{
    public string Name { get; set; }

    public string NamespaceName { get; set; }

    public AssemblyReference Assembly{ get; set; }
}

public struct MethodReference
{
    public string Name { get; set; }

    public TypeReference DeclaringType { get; set; }

    public bool IsStatic { get; set; }

    public IReadOnlyList<ParameterReference> Parameters { get; set; }
    public string FullNameWithoutReturnType { get; set; }
    public int MetadataToken { get; set; }
}

public struct ParameterReference
{
    public string Name { get; set; }

    public TypeReference ParameterType { get; set; }
}

static class MetadataHelper
{
    public static MethodInfo FindMethodInfo(Assembly assembly, MetadataNode node)
    {
        MethodInfo returnMethodInfo = null;

        VisitTypes(assembly, visitType);

        void visitType(Type type)
        {
            if (returnMethodInfo == null)
            {
                VisitMethods(type, visitMethodInfo);
            }
        }

        void visitMethodInfo(MethodInfo methodInfo)
        {
            if (returnMethodInfo == null)
            {
                if (ConvertToMetadataNode(methodInfo)?.MetadataToken == node.MetadataToken)
                {
                    returnMethodInfo = methodInfo;
                }
            }
        }

        return returnMethodInfo;
    }

    public static IEnumerable<MetadataNode> GetMetadataNodes(string assemblyFilePath)
    {
        return getNamespaceNodes(GetAllTypes(LoadAssembly(assemblyFilePath)));

        static IReadOnlyList<MetadataNode> getNamespaceNodes(IReadOnlyList<Type> types)
        {
            var items = new List<MetadataNode>();

            foreach (var namespaceName in types.Select(t => t.Namespace).Distinct())
            {
                var nodeForNamespace = new MetadataNode
                {
                    Name = namespaceName,

                    IsNamespace = true
                };

                nodeForNamespace.children.AddRange(types.Where(x => x.Namespace == namespaceName).Select(classToMetaData));

                items.Add(nodeForNamespace);
            }

            return items;
        }

        static MetadataNode classToMetaData(Type x)
        {
            var classNode = new MetadataNode
            {
                IsClass       = true,
                Name          = GetName(x),
                NamespaceName = x.Namespace,
                AssemblyName      = x.Assembly.GetName().Name
            };

            VisitMethods(x, m => { classNode.children.Add(ConvertToMetadataNode(m)); });

            return classNode;
        }

        static string GetName(Type x)
        {
            if (x.IsNested)
            {
                return GetName(x.DeclaringType )+ "+" + x.Name;
            }

            return x.Name;
        }
        
    }

    static AssemblyReference AsReference(this Assembly assembly)
    {
        return new AssemblyReference { Name = assembly.GetName().Name };
    }
    static TypeReference AsReference(this Type x)
    {
        return new TypeReference
        {
            Name          = GetName(x),
            NamespaceName = x.Namespace,
            Assembly = x.Assembly.AsReference()
        };

        static string GetName(Type x)
        {
            if (x.IsNested)
            {
                return GetName(x.DeclaringType) + "+" + x.Name;
            }

            return x.Name;
        }
    }

    static MethodReference AsReference(this MethodInfo methodInfo)
    {
        return new MethodReference
        {
            
            Name     = methodInfo.Name,
            IsStatic = methodInfo.IsStatic,
            FullNameWithoutReturnType  = string.Join(" ", methodInfo.ToString()!.Split(new[] { ' ' }).Skip(1)),
            MetadataToken              = methodInfo.MetadataToken,
            
            DeclaringType = methodInfo.DeclaringType.AsReference(),
            Parameters = methodInfo.GetParameters().Select(AsReference).ToList()

        };
    }

    static ParameterReference AsReference(this ParameterInfo parameterInfo)
    {
        return new ParameterReference
        {
            Name                      = parameterInfo.Name,
            ParameterType             = parameterInfo.ParameterType.AsReference()
        };
    }

    public static Assembly LoadAssembly(string assemblyFilePath)
    {
        return Assembly.LoadFile(assemblyFilePath);
    }

    public static (Assembly assembly, MetadataLoadContext metadataLoadContext) ReadAssembly(string assemblyFilePath)
    {
        var coreAssemblies = Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll");

        var optionalLibraries = Directory.GetFiles(Path.GetDirectoryName(assemblyFilePath), "*.dll");

        var libraries = new List<string>();
        libraries.AddRange(coreAssemblies);
        libraries.AddRange(optionalLibraries);

        var resolver = new PathAssemblyResolver(libraries);

        var metadataContext = new MetadataLoadContext(resolver);

        return (metadataContext.LoadFromAssemblyPath(assemblyFilePath), metadataContext);
    }

    static MetadataNode ConvertToMetadataNode(MethodInfo methodInfo)
    {
        return new MetadataNode
        {
            IsMethod                   = true,
            Name                       = methodInfo.Name,
            FullNameWithoutReturnType  = string.Join(" ", methodInfo.ToString()!.Split(new[] { ' ' }).Skip(1)),
            MetadataToken              = methodInfo.MetadataToken,
            DeclaringTypeFullName      = methodInfo.DeclaringType?.FullName,
            DeclaringTypeNamespaceName = methodInfo.DeclaringType?.Namespace,
            IsStaticMethod = methodInfo.IsStatic
        };
    }

    static List<Type> GetAllTypes(Assembly assembly)
    {
        var types = new List<Type>();

        void visit(Type type) => types.Add(type);

        VisitTypes(assembly, visit);

        return types;
    }

    static bool IsValidForExport(MethodInfo methodInfo)
    {
        if (methodInfo.Name == "render" || methodInfo.Name == "InvokeRender")
        {
            return false;
        }

        if (methodInfo.Name.Contains("|") || methodInfo.Name.StartsWith("set_"))
        {
            return false;
        }

        if (methodInfo.GetParameters().Any(p => isNotValidForJson(p.ParameterType)))
        {
            return false;
        }

        // is function component
        if (methodInfo.ReturnType == typeof(Element) || methodInfo.ReturnType.IsSubclassOf(typeof(Element)))
        {
            return true;
        }

        return false;

        static bool isNotValidForJson(Type t)
        {
            if (t == typeof(Element) || t.BaseType == typeof(HtmlElement))
            {
                return true;
            }

            if (typeof(Element).IsAssignableFrom(t.BaseType))
            {
                return true;
            }

            if (typeof(ReactStatefulComponent).IsAssignableFrom(t.BaseType))
            {
                return true;
            }

            if (typeof(MulticastDelegate).IsAssignableFrom(t.BaseType))
            {
                return true;
            }

            return false;
        }
    }

    static void VisitMethods(Type type, Action<MethodInfo> visit)
    {
        const BindingFlags AllFlags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        foreach (var methodInfo in type.GetMethods(AllFlags).Where(IsValidForExport))
        {
            visit(methodInfo);
        }
    }

    static void VisitTypes(Assembly assembly, Action<Type> visit)
    {
        foreach (var type in assembly.GetTypes().Where(isValidForExport))
        {
            visit(type);

            foreach (var nestedType in type.GetNestedTypes().Where(isValidForExport))
            {
                visit(nestedType);
            }
        }

        static bool isValidForExport(Type type)
        {
            if (type.IsAbstract)
            {
                return false;
            }

            return type.IsSubclassOf(typeof(ReactStatefulComponent));
        }
    }
}