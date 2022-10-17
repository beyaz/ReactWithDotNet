using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ReactWithDotNet.UIDesigner;

class MetadataHelper
{
    static bool IsReactComponent(Type type)
    {
        return type.IsSubclassOf(typeof(ReactStatefulComponent));
    }

    public static MethodInfo FindMethodInfo(Assembly assembly, MetadataNode node)
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
                if (JsonConvert.SerializeObject(createFromMethod(methodInfo)) == JsonConvert.SerializeObject(node))
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
            if (type.IsAbstract)
            {
                continue;
            }

            if (!IsReactComponent(type))
            {
                continue;
            }

            visit(type);
            foreach (var nestedType in type.GetNestedTypes())
            {
                if (!IsReactComponent(nestedType))
                {
                    continue;
                }
                
                visit(nestedType);
            }
        }
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
                    Name        = namespaceName,
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
                Name          = x.IsNested ? x.DeclaringType?.Name + "+" + x.Name : x.Name,
                NamespaceName = x.Namespace
            };


            VisitMethods(x, m =>
            {
                var node = createFromMethod(m);
                if (node != null)
                {
                    classNode.children.Add(node);
                }
                
            });

            return classNode;
        }


    }

    static MetadataNode createFromMethod(MethodInfo methodInfo)
    {
        if (methodInfo.Name == "render" || methodInfo.Name == "InvokeRender")
        {
            return null;
        }
        
        // is function component
        if (methodInfo.ReturnType == typeof(Element) || methodInfo.ReturnType.IsSubclassOf(typeof(Element)))
        {
            return new MetadataNode
            {
                IsMethod                   = true,
                Name                       = methodInfo.Name,
                FullNameWithoutReturnType  = string.Join(" ", methodInfo.ToString()!.Split(new[] { ' ' }).Skip(1)),
                MetadataToken              = methodInfo.MetadataToken,
                DeclaringTypeFullName      = methodInfo.DeclaringType?.FullName,
                DeclaringTypeNamespaceName = methodInfo.DeclaringType?.Namespace
            };
        }

        return null;
    }

    static void VisitMethods(Type type, Action<MethodInfo> visit)
    {
        foreach (var methodInfo in type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
        {
            if (methodInfo.Name.Contains("|") || methodInfo.Name.StartsWith("set_"))
            {
                continue;
            }

            if (methodInfo.GetParameters().Any(p=> isNotValidForJson(p.ParameterType)))
            {
                continue;
            }

            visit(methodInfo);
        }

        bool isNotValidForJson(Type t)
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
}