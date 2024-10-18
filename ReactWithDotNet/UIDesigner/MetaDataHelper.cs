using System.Collections.Immutable;
using System.Reflection;

namespace ReactWithDotNet.UIDesigner;

static class MetadataHelper
{
    public static MethodInfo FindMethodInfo(Assembly assembly, MetadataNode node)
    {
        MethodInfo returnMethodInfo = null;

        VisitTypes(assembly, visitType);

        return returnMethodInfo;

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
                if (ConvertToMetadataNode(methodInfo).MethodReference.UUID == node.MethodReference.UUID)
                {
                    returnMethodInfo = methodInfo;
                }
            }
        }
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
                    classNodes = classNodes.Where(classNode => classNode.HasChild).Take(3).OrderByDescending(classNode => classNode.Children.Count).ToList();
                }

                if (classNodes.Count > 0)
                {
                    items.Add(new()
                    {
                        NamespaceReference = namespaceName,
                        IsNamespace        = true,
                        label              = namespaceName,
                        Children           = classNodes.ToImmutableList()
                    });
                }
            }

            return items.Take(3).ToList();
        }

        static bool ignoreClass(MetadataNode classNode)
        {
            if (classNode.Children.Count == 0)
            {
                // is generic class we have no generic arguments so we need to ignore
                if (classNode.label.IndexOf('`') > 0)
                {
                    return true;
                }

                if (classNode.TypeReference.IsStaticClass ||
                    classNode.TypeReference.IsAbstract)
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
                    if (classNode.Children.Count < 5)
                    {
                        if (m.Name.IndexOf(methodFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                        {
                            classNode = classNode.AddChild(ConvertToMetadataNode(m));
                        }
                    }

                    return;
                }

                classNode = classNode.AddChild(ConvertToMetadataNode(m));
            });

            if (classNode.HasChild is false)
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

    static MetadataNode AddChild(this MetadataNode node, MetadataNode child)
    {
        return node with { Children = node.Children.Add(child) };
    }

    static MetadataNode ConvertToMetadataNode(MethodInfo methodInfo)
    {
        return new()
        {
            IsMethod        = true,
            MethodReference = methodInfo.AsReference(),
            label           = methodInfo.AsReference().FullNameWithoutReturnType
        };
    }

    static List<Type> GetAllTypes(Assembly assembly, string classFilter)
    {
        var types = new List<Type>();

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

        void visit(Type type)
        {
            if (!string.IsNullOrWhiteSpace(classFilter))
            {
                var classFilters = classFilter.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim()).ToArray();
                foreach (var filter in classFilters)
                {
                    if (type.FullName?.IndexOf(filter, StringComparison.OrdinalIgnoreCase) < 0)
                    {
                        return;
                    }
                }
            }

            types.Add(type);
        }
    }

    static bool IsValidForExport(MethodInfo methodInfo)
    {
        if (methodInfo.IsAbstract)
        {
            return false;
        }

        if (methodInfo.Name == "render" ||
            methodInfo.Name == "renderAsync" ||
            methodInfo.Name == "InvokeRender" ||
            methodInfo.Name == "componentDidCatch")
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
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Task<>))
            {
                type = type.GetGenericArguments()[0];
            }

            if (type.IsGenericType)
            {
                var genericTypeDefinition = type.GetGenericTypeDefinition();

                var genericArguments = type.GetGenericArguments();

                if (genericArguments.Length == 1)
                {
                    var genericEnumerableInterface = typeof(IEnumerable<>).MakeGenericType(genericArguments);

                    if (genericTypeDefinition == typeof(Task<>) ||
                        genericTypeDefinition == typeof(IEnumerable<>) ||
                        type.GetInterfaces().Contains(genericEnumerableInterface))
                    {
                        type = genericArguments[0];
                    }
                }
            }

            return type == typeof(Element) || type.IsSubclassOf(typeof(Element));
        }

        static bool isNotValidForJson(Type parameterType)
        {
            if (parameterType.IsAnonymousType())
            {
                return true;
            }

            if (parameterType == typeof(object))
            {
                return true;
            }

            if (parameterType == typeof(Element) || parameterType.BaseType == typeof(HtmlElement) ||
                parameterType == typeof(Element[]) || parameterType == typeof(IEnumerable<Element>))
            {
                return true;
            }

            if (typeof(Element).IsAssignableFrom(parameterType.BaseType))
            {
                return true;
            }

            if (typeof(ReactComponentBase).IsAssignableFrom(parameterType.BaseType))
            {
                return true;
            }

            if (typeof(PureComponent).IsAssignableFrom(parameterType.BaseType))
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