using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public abstract class ThirdPartyReactComponent : Element
{
    [JsonPropertyName("$type")]
    public virtual string Type
    {
        get
        {
            var type = GetType();

            var reactRealType = type.GetCustomAttributes<ReactRealTypeAttribute>().FirstOrDefault()?.Type;
            
            return (reactRealType ?? type).FullName;
        }
    }

    /// <summary>
    ///     Gets the style.
    /// </summary>
    [React]
    public Style style { get; } = new();

    protected ThirdPartyReactComponent()
    {
        
    }

    protected ThirdPartyReactComponent(params Modifier[] modifiers)
    {
        style.Apply(modifiers);
    }

    /// <summary>
    ///     Imports filled values given style
    /// </summary>
    [JsonIgnore]
    public Style Style
    {
        set => style.Import(value);
    }
}