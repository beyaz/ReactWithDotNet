using System.Reflection;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace ReactWithDotNet.Libraries.UIDesigner.Components;

public sealed class ValueInfo
{
    public string Label { get; set; }
    public string Value { get; set; }
    
    public override string ToString()
    {
        return $"{Label}:{Value}";
    }
}

class ValueInfoStringEditor : ReactComponent
{
    public ValueInfo Model { get; set; } = new();

    public Expression<Func<string>> valueBind;

    protected override Element render()
    {
        return new FlexRow(WidthMaximized)
        {
            new span(Model.Label+":"), new input{type = "text", valueBind = valueBind}
        };
    }
}

static class TypeInspector
{
    public static IReadOnlyList<ValueInfo> GetValueInfoList(Type instanceType,object instance, string prefix )
    {
        var bindings = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        
        var items    = new List<ValueInfo>();
        
        foreach (var propertyInfo in instanceType.GetProperties(bindings))
        {
            var type = propertyInfo.PropertyType;
            var name = propertyInfo.Name;
            
            if (type.IsPrimitive() )
            {
                items.Add(new ValueInfo { Label = prefix + name, Value = JsonConvert.SerializeObject(propertyInfo.GetValue(instance)) });
                continue;
            }

            if (type.IsCollectionType())
            {
                continue;
            }
            var subProperties = GetValueInfoList(type, propertyInfo.GetValue(instance), prefix + name + ".");
            
            items.AddRange(subProperties);
        }

        foreach (var fieldInfo in instanceType.GetFields(bindings))
        {
            // disable backing fields
            if (fieldInfo.GetCustomAttribute<CompilerGeneratedAttribute>() is not null)
            {
                continue;
            }

            var type = fieldInfo.FieldType;
            var name = fieldInfo.Name;
            
            if (type.IsPrimitive())
            {
                items.Add(new ValueInfo { Label = prefix + name, Value = JsonConvert.SerializeObject(fieldInfo.GetValue(instance)) });
                continue;
            }

            if (type.IsCollectionType())
            {
                continue;
            }

            var subProperties = GetValueInfoList(type, fieldInfo.GetValue(instance), prefix + name + ".");

            items.AddRange(subProperties);
        }

        return items;
        
        
    }
    static bool IsCollectionType(this Type type)
    {
        if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IReadOnlyList<>))
        {
            return true;
        }

        return false;
    }
    static bool IsPrimitive(this Type type)
    {
        return type == typeof(string) ||
            type == typeof(bool) ||
            type == typeof(bool?) ||
            type == typeof(int) ||
            type == typeof(int?);
    }
}