using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public abstract class ReactPureComponent : Element
{
    internal List<IModifier> modifiers;

    internal Element InvokeRender() => render();

    protected abstract Element render();

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    protected internal ReactContext Context { get; internal set; }
}