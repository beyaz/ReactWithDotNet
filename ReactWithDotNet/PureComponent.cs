using System.Text.Json.Serialization;
using ReactWithDotNet.UIDesigner;

namespace ReactWithDotNet;

/// <summary>
///     PureComponent is similar to Component but it skips re-renders for same props and state.
///     <br />
///     https://react.dev/reference/react/PureComponent
/// </summary>
public abstract class PureComponent : Element
{
    
    /// <summary>
    ///   Indicates component is in design mode.
    /// </summary>
    [JsonIgnore]
    protected bool DesignMode => ReactWithDotNetDesigner.IsInDesignMode(Context.HttpContext);
    
    
    internal List<Style> classNameList;

    internal int ComponentUniqueIdentifier;
    internal Func<Task<Element>> DesignerCustomizedRender;

    internal List<Modifier> Modifiers;

    internal Style StyleForRootElement;
    
    ClientForPureComponent _client;

    [JsonIgnore]
    public ClientForPureComponent Client
    {
        get { return _client ??= new(Context); }
    }

    [JsonIgnore]
    public Style style
    {
        get
        {
            StyleForRootElement ??= new();

            return StyleForRootElement;
        }
    }

    [JsonIgnore]
    protected internal ReactContext Context { get; internal set; }

    internal async Task<Element> InvokeRender()
    {
        try
        {
            if (DesignerCustomizedRender != null)
            {
                return DesignerCustomizedRender();
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
        catch (Exception exception)
        {
            var renderResult = componentDidCatch(exception);

            if (!NoneOfRender.IsNoneOfRender(renderResult))
            {
                return renderResult;
            }

            throw;
        }
    }

    protected static Modifier Modify<TPureComponent>(Action<TPureComponent> modifyAction)
        where TPureComponent : PureComponent
    {
        return CreatePureComponentModifier(modifyAction);
    }

    /// <summary>
    ///     When any exception occurred in render method then this method will be call.
    ///     <code>
    ///     protected override Element componentDidCatch(Exception exceptionOccurredInRender)
    ///     {
    ///         return new div(Color("red"))
    ///         {
    ///             exceptionOccurredInRender.ToString()
    ///         };
    ///     }
    ///     </code>
    /// </summary>
    protected virtual Element componentDidCatch(Exception exceptionOccurredInRender)
    {
        return NoneOfRender.Value;
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

public sealed class ClientForPureComponent
{
    readonly ReactContext _reactContext;

    internal ClientForPureComponent(ReactContext reactContext)
    {
        _reactContext = reactContext;
    }

    /// <summary>
    ///     Client size information will be available after the first request.
    /// </summary>
    public double? Height => _reactContext.ClientWidth;

    /// <summary>
    ///     Client size information will be available after the first request.
    /// </summary>
    public double? Width => _reactContext.ClientWidth;

    /// <summary>
    ///  Sample usage:
    ///     <code>
    ///     
    ///      if (Client.WidthHasMatch(SM))
    ///      {
    ///          // 
    ///      }
    ///     
    ///     </code>
    /// </summary>
    public bool WidthHasMatch(Func<StyleModifier[],StyleModifier> mediaSizeFunction)
    {
        if (mediaSizeFunction == SM)
        {
            return Width >= 640;
        }
        
        if (mediaSizeFunction == MD)
        {
            return Width >= 768;
        }
        
        if (mediaSizeFunction == LG)
        {
            return Width >= 1024;
        }
        
        if (mediaSizeFunction == XL)
        {
            return Width >= 1280;
        }
        
        if (mediaSizeFunction == XXL)
        {
            return Width >= 1536;
        }

        throw DeveloperException("Use common media size functions: SM, MD, LG, XL, XXL");
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