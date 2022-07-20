using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ReactWithDotNet.UIDesigner;

class MetadataHelper
{
    static IEnumerable<ReactComponentInfo> GetComponents(Assembly assembly)
    {
        foreach (var type in assembly.GetTypes())
        {
            if (type.IsAbstract)
            {
                continue;
            }

            if (IsReactComponent(type))
            {
                yield return new ReactComponentInfo { Name = type.GetFullName(), Value = type.GetFullName() };
            }
        }
    }

    static bool IsReactComponent(Type type)
    {
        if (type.IsSubclassOf(typeof(ReactComponent)))
        {
            return true;
        }

        return IsReactStatefulComponent(type);
    }

    public static bool IsReactStatefulComponent(Type type)
    {
        type = type.BaseType;

        if (type?.IsGenericType == true)
        {
            var typeDefinition = type.GetGenericTypeDefinition();
            if (typeDefinition == typeof(ReactComponent<>) || typeDefinition.IsSubclassOf(typeof(ReactComponent<>)))
            {
                return true;
            }
        }

        return false;
    }

    public static MethodInfo FindMethodInfo(Assembly assembly, int metadataToken)
    {
        MethodInfo returnMethodInfo = null;


        VisitTypes(assembly, visitType);

        void visitType(Type type)
        {
            if (returnMethodInfo == null)
            {
              VisitMethods(type,visitMethodInfo);
            }
        }

        void visitMethodInfo(MethodInfo methodInfo)
        {
            if (returnMethodInfo == null)
            {
                if (methodInfo.MetadataToken == metadataToken)
                {
                    returnMethodInfo = methodInfo;
                }
            }
            
        }


        return returnMethodInfo;
    }

    public static List<Type> GetAllTypes(Assembly assembly)
    {
        var types = new List<Type>();

        void visit(Type type) => types.Add(type);

        VisitTypes(assembly, visit);

        return types;
    }

    static void VisitTypes(Assembly assembly, Action<Type> visit)
    {
        foreach (var type in assembly.GetTypes())
        {
            visit(type);
            foreach (var nestedType in type.GetNestedTypes())
            {
                visit(nestedType);
            }
        }
    }

    public static MetadataNode[] GetMetadataNodes(string assemblyFilePath)
    {
        var (assembly, metadataLoadContext) = ReadAssembly(assemblyFilePath);

        var items = new List<MetadataNode>();

        using (metadataLoadContext)
        {
            var types = GetAllTypes(assembly);

            foreach (var namespaceName in types.Select(t => t.Namespace).Distinct())
            {
                var nodeForNamespace = new MetadataNode
                {
                    Name        = namespaceName,
                    IsNamespace = true
                };

                nodeForNamespace.children.AddRange(types.Where(x => x.Namespace == namespaceName).Select(classToMetaData));

                items.Add(nodeForNamespace);
            }
        }

        return items.ToArray();

        static MetadataNode classToMetaData(Type x)
        {
            var classNode = new MetadataNode
            {
                IsClass = true,
                Name    = x.Name,
                NamespaceName = x.Namespace
            };


            VisitMethods(x, m => classNode.children.Add(createFromMethod(m)));
            

            return classNode;
        }

        static MetadataNode createFromMethod(MethodInfo methodInfo)
        {
            return new MetadataNode
            {
                IsMethod                  = true,
                Name                      = methodInfo.Name,
                FullNameWithoutReturnType = string.Join(" ", methodInfo.ToString()!.Split(new[] { ' ' }).Skip(1)),
                MetadataToken             = methodInfo.MetadataToken,
            };
        }
    }


    static void VisitMethods(Type type, Action<MethodInfo> visit)
    {
        foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
        {
            if (methodInfo.Name.StartsWith("get_") || methodInfo.Name.StartsWith("set_"))
            {
                continue;
            }

            visit(methodInfo);
        }
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
}