using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public abstract class ReactPureComponent : Element
{
    internal Func<Element> _designerCustomizedRender;
    internal Style _styleForRootElement;

    internal List<IModifier> modifiers;

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public Style style
    {
        get
        {
            _styleForRootElement ??= new Style();

            return _styleForRootElement;
        }
    }

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    protected internal ReactContext Context { get; internal set; }

    internal Element InvokeRender() => _designerCustomizedRender == null ? render() : _designerCustomizedRender();

    protected abstract Element render();


    protected static IModifier Modify<TPureComponent>(Action<TPureComponent> modifyAction)
        where TPureComponent : ReactPureComponent => CreatePureComponentModifier(modifyAction);
}