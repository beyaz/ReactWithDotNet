using System.Reflection;

namespace ReactWithDotNet.UIDesigner;

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
                if (ConvertToMetadataNode(methodInfo).MethodReference.MetadataToken == node.MethodReference.MetadataToken)
                {
                    returnMethodInfo = methodInfo;
                }
            }
        }

        return returnMethodInfo;
    }

    static void AddChild(this MetadataNode node, MetadataNode child)
    {
        (node.Children ??= new List<MetadataNode>()).Add(child);
    }
    
    static bool HasChild(this MetadataNode node)
    {
        return node.Children?.Count > 0;
    }
    
    public static IEnumerable<MetadataNode> GetMetadataNodes(string assemblyFilePath, string classFilter, string methodFilter)
    {
        return getNamespaceNodes(GetAllTypes(LoadAssembly(assemblyFilePath), classFilter));

        IReadOnlyList<MetadataNode> getNamespaceNodes(IReadOnlyList<Type> types)
        {
            var items = new List<MetadataNode>();

            foreach (var namespaceName in types.Select(t => t.Namespace).Distinct())
            {
                var classNodes = types.Where(x => x.Namespace == namespaceName).Select(classToMetaData).Where(x => x is not null && !ignoreClass(x)).ToList();

                if (!string.IsNullOrWhiteSpace(methodFilter))
                {
                    classNodes = classNodes.Where(classNode => classNode.HasChild()).Take(3).OrderByDescending(classNode => classNode.Children.Count).ToList();
                }

                if (classNodes.Count > 0)
                {
                    items.Add(new MetadataNode
                    {
                        NamespaceReference = namespaceName,
                        IsNamespace        = true,
                        label              = namespaceName,
                        Children = classNodes
                    });
                }
            }

            return items.Take(2).ToList();
        }

        static bool ignoreClass(MetadataNode classNode)
        {
            if (classNode.Children?.Count == 0)
            {
                if (classNode.TypeReference.IsStaticClass)
                {
                    return true;
                }
            }

            return false;
        }

        MetadataNode classToMetaData(Type type)
        {
            var classNode = new MetadataNode
            {
                IsClass       = true,
                TypeReference = type.AsReference(),
                label         = type.Name
            };

            if (type.IsNested)
            {
                classNode.label = type.DeclaringType?.Name + "+" + classNode.label;
            }

            VisitMethods(type, m =>
            {
                if (!string.IsNullOrWhiteSpace(methodFilter))
                {
                    if (classNode.Children is null || classNode.Children?.Count < 5)
                    {
                        if (m.Name.IndexOf(methodFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            classNode.AddChild(ConvertToMetadataNode(m));
                        }
                    }

                    return;
                }

                classNode.AddChild(ConvertToMetadataNode(m));
            });

            if (classNode.HasChild() is false)
            {
                if (!(type.IsSubclassOf(typeof(ReactComponentBase)) || type.IsSubclassOf(typeof(PureComponent))))
                {
                    return null;
                }
            }

            return classNode;
        }
    }

    public static Assembly LoadAssembly(string assemblyFilePath)
    {
        if (Assembly.GetEntryAssembly()?.Location == assemblyFilePath)
        {
            return Assembly.GetEntryAssembly();
        }

        return Assembly.LoadFile(assemblyFilePath);
    }

    static MetadataNode ConvertToMetadataNode(MethodInfo methodInfo)
    {
        return new MetadataNode
        {
            IsMethod        = true,
            MethodReference = methodInfo.AsReference(),
            label           = methodInfo.AsReference().FullNameWithoutReturnType
        };
    }

    static List<Type> GetAllTypes(Assembly assembly, string classFilter)
    {
        var types = new List<Type>();

        void visit(Type type)
        {
            if (!string.IsNullOrWhiteSpace(classFilter))
            {
                if (type.Name.IndexOf(classFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    types.Add(type);
                }

                return;
            }

            types.Add(type);
        }

        VisitTypes(assembly, visit);

        if (types.Count == 0 && !string.IsNullOrWhiteSpace(classFilter))
        {
            // try search by namespace

            void filterByNamespaceName(Type type)
            {
                if (type.FullName?.IndexOf(classFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    types.Add(type);
                }
            }

            VisitTypes(assembly, filterByNamespaceName);
        }

        return types;
    }

    
    
    static bool IsValidForExport(MethodInfo methodInfo)
    {
        if (methodInfo.Name == "render" || methodInfo.Name == "InvokeRender")
        {
            return false;
        }

        if ( /*methodInfo.Name.Contains("|") ||*/ methodInfo.Name.StartsWith("set_"))
        {
            return false;
        }

        if (methodInfo.GetParameters().Any(p => isNotValidForJson(p.ParameterType)))
        {
            return false;
        }

        // is function component
        if (IsElement(methodInfo.ReturnType))
        {
            return true;
        }

        return false;

        static bool IsElement(Type type)
        {
            return type == typeof(Element) || type.IsSubclassOf(typeof(Element));
        }
        
        static bool isNotValidForJson(Type t)
        {
            if (t == typeof(object))
            {
                return true;
            }
            
            if (t == typeof(Element) || t.BaseType == typeof(HtmlElement) ||
                t == typeof(Element[]) || t == typeof(IEnumerable<Element>))
            {
                return true;
            }

            if (typeof(Element).IsAssignableFrom(t.BaseType))
            {
                return true;
            }

            if (typeof(ReactComponentBase).IsAssignableFrom(t.BaseType))
            {
                return true;
            }
            if (typeof(PureComponent).IsAssignableFrom(t.BaseType))
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
        type.VisitMethods(methodInfo =>
        {
            if (IsValidForExport(methodInfo))
            {
                visit(methodInfo);
            }
        });
    }

    static void VisitTypes(Assembly assembly, Action<Type> visit)
    {
        assembly.VisitTypes(visit);
    }
}