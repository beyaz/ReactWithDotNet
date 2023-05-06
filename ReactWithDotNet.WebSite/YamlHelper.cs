using YamlDotNet.Core.Events;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace ReactWithDotNet.WebSite;

static class YamlHelper
{
    public static T DeserializeFromYaml<T>(string yamlContent)
    {
        var deserializer = new DeserializerBuilder()
                          .WithNamingConvention(CamelCaseNamingConvention.Instance)
                          .WithNodeTypeResolver(new ReadOnlyCollectionNodeTypeResolver())
                          .Build();

        return deserializer.Deserialize<T>(yamlContent);
    }

    sealed class ReadOnlyCollectionNodeTypeResolver : INodeTypeResolver
    {
        static readonly IReadOnlyDictionary<Type, Type> CustomGenericInterfaceImplementations = new Dictionary<Type, Type>
        {
            { typeof(IReadOnlyCollection<>), typeof(List<>) },
            { typeof(IReadOnlyList<>), typeof(List<>) },
            { typeof(IReadOnlyDictionary<,>), typeof(Dictionary<,>) }
        };

        public bool Resolve(NodeEvent nodeEvent, ref Type type)
        {
            if (type.IsInterface && type.IsGenericType && CustomGenericInterfaceImplementations.TryGetValue(type.GetGenericTypeDefinition(), out var concreteType))
            {
                type = concreteType.MakeGenericType(type.GetGenericArguments());
                return true;
            }

            return false;
        }
    }
}