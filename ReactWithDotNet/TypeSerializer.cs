namespace ReactWithDotNet;

static class TypeSerializer
{
    public static Type DeserializeToType(string typeFullNameWithAssembly)
    {
        return Type.GetType(typeFullNameWithAssembly);

        //var genericIdentifierCharIndex = typeFullNameWithAssembly.IndexOf('`',StringComparison.OrdinalIgnoreCase);
        //if (genericIdentifierCharIndex < 0)
        //{
        //    return Type.GetType(typeFullNameWithAssembly);
        //}

        //var leftColumnIndex = typeFullNameWithAssembly.IndexOf('[', genericIdentifierCharIndex);
        //if (leftColumnIndex > 0)
        //{
        //    var lastCommaIndex = typeFullNameWithAssembly.LastIndexOf(',');
        //    if (lastCommaIndex > 0)
        //    {
        //        var elementTypeName = typeFullNameWithAssembly.Substring(0, leftColumnIndex);

        //        var assemblyName = typeFullNameWithAssembly.Substring(lastCommaIndex+1);

        //        var elementType = Type.GetType($"{elementTypeName},{assemblyName}");
        //        if (elementType is not null)
        //        {
        //            leftColumnIndex
        //        }
        //        return DeserializeToType($"{elementTypeName},{assemblyName}");

        //    }
        //}

        //return null;
    }

    public static string SerializeToString(this Type type)
    {
        return $"{type.FullName},{type.Assembly.GetName().Name}";
    }
}