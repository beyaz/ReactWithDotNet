using System.Reflection;

namespace ReactWithDotNet.UIDesigner;

[Serializable]
public sealed class AssemblyReference
{
    public string Name { get; set; }

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
    public AssemblyReference Assembly { get; set; }

    public string FullName { get; set; }

    public string Name { get; set; }

    public string NamespaceName { get; set; }

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
    public TypeReference DeclaringType { get; set; }

    public string FullNameWithoutReturnType { get; set; }

    public bool IsStatic { get; set; }

    public int MetadataToken { get; set; }

    public string Name { get; set; }

    public IReadOnlyList<ParameterReference> Parameters { get; set; }

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
    public string Name { get; set; }

    public TypeReference ParameterType { get; set; }

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
            Assembly      = x.Assembly.AsReference()
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
            Name                      = methodInfo.Name,
            IsStatic                  = methodInfo.IsStatic,
            FullNameWithoutReturnType = string.Join(" ", methodInfo.ToString()!.Split(new[] { ' ' }).Skip(1)),
            MetadataToken             = methodInfo.MetadataToken,

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


    public static Type TryLoadFrom(Assembly assembly, TypeReference typeReference)
    {
        if (assembly == null)
        {
            throw new ArgumentNullException(nameof(assembly));
        }

        if (typeReference == null)
        {
            throw new ArgumentNullException(nameof(typeReference));
        }

        foreach (var type in assembly.GetTypes())
        {
            var value = findMatchedType(type);
            if (value is not null)
            {
                return value;
            }
        }

        return null;

        Type findMatchedType(Type type)
        {
            if (typeReference.Equals(type.AsReference()))
            {
                return type;
            }

            foreach (var nestedType in type.GetNestedTypes())
            {
                var value = findMatchedType(nestedType);
                if (value is not null)
                {
                    return value;
                }
            }

            return null;
        }
    }
}