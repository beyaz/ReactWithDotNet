using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public abstract class ReactPureComponent : Element
{
    internal Func<Element> DesignerCustomizedRender;
    internal Style StyleForRootElement;

    internal List<IModifier> Modifiers;

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public Style style
    {
        get
        {
            StyleForRootElement ??= new Style();

            return StyleForRootElement;
        }
    }

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    protected internal ReactContext Context { get; internal set; }

    internal Element InvokeRender() => DesignerCustomizedRender == null ? render() : DesignerCustomizedRender();

    protected abstract Element render();


    protected static IModifier Modify<TPureComponent>(Action<TPureComponent> modifyAction)
        where TPureComponent : ReactPureComponent => CreatePureComponentModifier(modifyAction);
}