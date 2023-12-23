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
            StyleForRootElement ??= new ();

            return StyleForRootElement;
        }
    }

    [JsonIgnore]
    protected internal ReactContext Context { get; internal set; }

    internal async Task<Element> InvokeRender()
    {
        if (DesignerCustomizedRender != null)
        {
            return Task.FromResult(DesignerCustomizedRender());
        }

        // ReSharper disable once MethodHasAsyncOverload
        var renderResult = render();

        if (!NoneOfRender.IsNoneOfRender(renderResult))
        {
            return renderResult;
        }
        
        var renderAsyncResult = await renderAsync();
            
        if (!NoneOfRender.IsNoneOfRender(renderAsyncResult))
        {
            return renderAsyncResult;
        }

        return null;
    }

    protected static IModifier Modify<TPureComponent>(Action<TPureComponent> modifyAction)
        where TPureComponent : PureComponent
    {
        return CreatePureComponentModifier(modifyAction);
    }

    protected virtual Element render()
    {
        return NoneOfRender.Value;
    }
    
    protected virtual Task<Element> renderAsync()
    {
        return Task.FromResult(NoneOfRender.Value);
    }
}

static class NoneOfRender
{
    public static readonly Element Value = new div();

    public static bool IsNoneOfRender(Element element)
    {
        return ReferenceEquals(Value, element);
    }
        
}