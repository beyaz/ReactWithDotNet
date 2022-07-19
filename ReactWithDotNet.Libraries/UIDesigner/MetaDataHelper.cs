using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ReactWithDotNet.UIDesigner;

class MetadataHelper
{
    public static List<Type> GetAllTypes(Assembly assembly)
    {
        List<Type> types = new List<Type>();

        foreach (var type in assembly.GetTypes())
        {
            add(types, type);
        }

        return types;

        static void add(List<Type> types, Type type)
        {
            types.Add(type);
            foreach (var nestedType in type.GetNestedTypes())
            {
                add(types, nestedType);
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
                Name    = x.Name
            };

            foreach (var methodInfo in x.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (methodInfo.Name.StartsWith("get_") || methodInfo.Name.StartsWith("set_"))
                {
                    continue;
                }

                classNode.children.Add(createFromMethod(methodInfo));
            }

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