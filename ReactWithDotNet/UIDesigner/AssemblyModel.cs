using System.Reflection;

namespace ReactWithDotNet.UIDesigner;

[Serializable]
public sealed class AssemblyReference
{
    public string Name { get; init; }

    public bool Equals(AssemblyReference other)
    {
        return Name == other?.Name;
    }

    public override string ToString()
    {
        return Name;
    }
}

[Serializable]
public sealed class TypeReference
{
    public AssemblyReference Assembly { get; init; }

    public string FullName { get; init; }

    public string Name { get; init; }

    public string NamespaceName { get; init; }
    
    public bool IsStaticClass { get; init; }

    public bool Equals(TypeReference other)
    {
        return Assembly.Equals(other?.Assembly) && FullName == other?.FullName;
    }

    public override string ToString()
    {
        return $"{FullName},{Assembly}";
    }
}

[Serializable]
public sealed class MethodReference
{
    public TypeReference DeclaringType { get; init; }

    public string FullNameWithoutReturnType { get; init; }

    public bool IsStatic { get; init; }

    public int MetadataToken { get; init; }

    public string Name { get; init; }

    public IReadOnlyList<ParameterReference> Parameters { get; init; }

    public bool Equals(MethodReference other)
    {
        if (DeclaringType is not null)
        {
            if (DeclaringType.Equals(other?.DeclaringType) == false)
            {
                return false;
            }
        }

        if (FullNameWithoutReturnType != other?.FullNameWithoutReturnType)
        {
            return false;
        }

        return true;
    }

    public override string ToString()
    {
        return $"{DeclaringType}::{Name}({string.Join(", ", Parameters)})";
    }
}

[Serializable]
public sealed class ParameterReference
{
    public string Name { get; init; }

    public TypeReference ParameterType { get; init; }

    public override string ToString()
    {
        return $"{ParameterType} {Name}";
    }
}

static class AssemblyModelHelper
{
    public static AssemblyReference AsReference(this Assembly assembly)
    {
        return new AssemblyReference { Name = assembly.GetName().Name };
    }

    public static TypeReference AsReference(this Type x)
    {
        return new TypeReference
        {
            FullName      = x.FullName,
            Name          = GetName(x),
            NamespaceName = x.Namespace,
            Assembly      = x.Assembly.AsReference(),
            IsStaticClass = x.IsStaticClass()
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

    public static MethodReference AsReference(this MethodInfo methodInfo)
    {
        return new MethodReference
        {
            Name     = methodInfo.Name,
            IsStatic = methodInfo.IsStatic,
            FullNameWithoutReturnType = string.Join(string.Empty, new List<string>
            {
                methodInfo.Name,
                "(",
                string.Join(", ", methodInfo.GetParameters().Select(parameterInfo => parameterInfo.ParameterType.Name + " " + parameterInfo.Name)),
                ")"
            }),
            MetadataToken = methodInfo.MetadataToken,

            DeclaringType = methodInfo.DeclaringType.AsReference(),
            Parameters    = methodInfo.GetParameters().Select(AsReference).ToList()
        };
    }

    public static ParameterReference AsReference(this ParameterInfo parameterInfo)
    {
        return new ParameterReference
        {
            Name          = parameterInfo.Name,
            ParameterType = parameterInfo.ParameterType.AsReference()
        };
    }

    public static Type TryLoadFrom(this Assembly assembly, TypeReference typeReference)
    {
        if (assembly == null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }

        if (typeReference == null)
        {
            throw new ArgumentNullException(nameof(typeReference));
        }

        Type foundedType = null;

        assembly.VisitTypes(type =>
        {
            if (foundedType == null)
            {
                if (typeReference.Equals(type.AsReference()))
                {
                    foundedType = type;
                }
            }
        });

        return foundedType;
    }

    public static MethodInfo TryLoadFrom(this Assembly assembly, MethodReference methodReference)
    {
        if (assembly == null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }

        if (methodReference == null)
        {
            throw new ArgumentNullException(nameof(methodReference));
        }

        MethodInfo foundedMethodInfo = null;

        assembly.VisitTypes(type =>
        {
            if (foundedMethodInfo == null)
            {
                type.VisitMethods(methodInfo =>
                {
                    if (foundedMethodInfo == null)
                    {
                        if (methodReference.Equals(methodInfo.AsReference()))
                        {
                            foundedMethodInfo = methodInfo;
                        }
                    }
                });
            }
        });

        return foundedMethodInfo;
    }

    public static void VisitMethods(this Type type, Action<MethodInfo> visitAction)
    {
        var flags = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        if (type.IsStaticClass())
        {
            flags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
        }

        foreach (var methodInfo in type.GetMethods(flags))
        {
            if (methodInfo.DeclaringType == typeof(object))
            {
                continue;
            }

            visitAction(methodInfo);
        }
    }

    public static void VisitTypes(this Assembly assembly, Action<Type> visitAction)
    {
        if (assembly == null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }

        if (visitAction == null)
        {
            throw new ArgumentNullException(nameof(visitAction));
        }

        foreach (var type in assembly.GetTypes())
        {
            visitType(type);
        }

        void visitType(Type type)
        {
            visitAction(type);

            foreach (var nestedType in type.GetNestedTypes())
            {
                visitType(nestedType);
            }
        }
    }
}