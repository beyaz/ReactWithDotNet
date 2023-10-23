using System.Text.Json.Serialization;

namespace ReactWithDotNet;

/// <summary>
/// PureComponent is similar to Component but it skips re-renders for same props and state.
/// <br/>
/// https://react.dev/reference/react/PureComponent
/// </summary>
public abstract class PureComponent : Element
{
    internal Func<Element> DesignerCustomizedRender;

    internal List<IModifier> Modifiers;
    
    internal Style StyleForRootElement;

    [JsonIgnore]
    public Style style
    {
        get
        {
            StyleForRootElement ??= new Style();

            return StyleForRootElement;
        }
    }

    [JsonIgnore]
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