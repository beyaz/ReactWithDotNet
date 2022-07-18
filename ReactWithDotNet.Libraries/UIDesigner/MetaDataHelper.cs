
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ReactWithDotNet.UIDesigner;

class MetaDataHelper
{
    public static MetadataNode[] GetMetadataNodes(string assemblyFilePath)
    {
		var resolver = new PathAssemblyResolver(new List<string>(Directory.GetFiles(RuntimeEnvironment.GetRuntimeDirectory(), "*.dll"))
        {
            assemblyFilePath
        });


        var items = new List<MetadataNode>();
        
        using (var metadataContext = new MetadataLoadContext(resolver))
        {
            Assembly assembly = metadataContext.LoadFromAssemblyPath(assemblyFilePath);

            var types = getAllTypes(assembly);

            foreach (var namespaceName in types.Select(t => t.Namespace).Distinct())
            {
                var nodeForNamespace = new MetadataNode
                {
                    Name        = namespaceName,
                    label        = namespaceName,
                    IsNamespace = true
                };
                
                nodeForNamespace.children.AddRange(types.Where(x => x.Namespace == namespaceName).Select(x => new MetadataNode { IsClass = true,
                                                                                                             label                       = x.Name,
                                                                                                             Name = x.Name }));
                
                items.Add(nodeForNamespace);
            }
        }

        return items.ToArray();

        static List<Type> getAllTypes(Assembly assembly)
        {
            List<Type> types = new List<Type>();

            foreach (var type in assembly.GetTypes())
            {
                add(types,type);
            }

            return types;
        }

        static void add(List<Type> types, Type type)
        {
            types.Add(type);
            foreach (var nestedType in type.GetNestedTypes())
            {
                add(types,nestedType);
            }
        }
    }
}