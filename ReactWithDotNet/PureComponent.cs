using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public abstract class PureComponent : Element
{
    internal Func<Element> DesignerCustomizedRender;

    internal List<IModifier> Modifiers;
    
    internal Style StyleForRootElement;

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

    internal Element InvokeRender()
    {
        return DesignerCustomizedRender == null ? render() : DesignerCustomizedRender();
    }

    protected static IModifier Modify<TPureComponent>(Action<TPureComponent> modifyAction)
        where TPureComponent : PureComponent
    {
        return CreatePureComponentModifier(modifyAction);
    }

    protected abstract Element render();
}