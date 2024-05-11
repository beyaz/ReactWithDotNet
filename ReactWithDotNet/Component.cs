using ReactWithDotNet.UIDesigner;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace ReactWithDotNet;

public interface IReactComponent
{
    Client Client { get; }
    ReactContext Context { get; }
}
public abstract class ReactComponentBase : Element, IReactComponent
{

    /// <summary>
    ///   Indicates component is in design mode.
    /// </summary>
    [JsonIgnore]
    protected bool DesignMode => ReactWithDotNetDesigner.IsInDesignMode(Context.HttpContext);
    
    internal Func<Task<Element>> DesignerCustomizedRender;
    
    internal Style StyleForRootElement;
    
    internal List<Style> classNameList;

    internal List<Modifier> Modifiers;

    [System.Text.Json.Serialization.JsonIgnore]
    public Style style
    {
        get
        {
            StyleForRootElement ??= new Style();

            return StyleForRootElement;
        }
    }

    internal abstract bool IsStateNull { get; }

    [System.Text.Json.Serialization.JsonIgnore]
    public Client Client 
    {
        get
        {
            return _client ??= new (Context);
        }
    }

    internal Client _client;

    internal int ComponentUniqueIdentifier;

    [System.Text.Json.Serialization.JsonIgnore]
    public ReactContext Context { get; internal set; }

    public static ReactComponentBase operator +(ReactComponentBase component, Style style)
    {
        component.style.Import(style);

        return component;
    }

    [SuppressMessage("ReSharper", "ParameterHidesMember")]
    public void Add(Style style)
    {
        this.style.Import(style);
    }

    internal object Clone()
    {
        return MemberwiseClone();
    }

    internal Task InvokeConstructor() => constructor();

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

    protected virtual Task componentDidMount()
    {
        return Task.CompletedTask;
    }

    protected abstract Task constructor();
    
    protected internal virtual Task OverrideStateFromPropsBeforeRender()
    {
        return Task.CompletedTask;
    }

    /// <summary>
    ///    Dispatch given <paramref name="handlerFunc"/> as event.
    ///    <br/>
    ///    Sample usage:
    /// <code> 
    ///    DispatchEvent(OnUserChanged, [new UserInfo { Name = '..'}] );
    /// </code>
    /// </summary>
    protected void DispatchEvent(Delegate handlerFunc, [CallerArgumentExpression(nameof(handlerFunc))] string handlerFuncName = null)
    {
        if (handlerFuncName is null)
        {
            throw new ArgumentNullException(nameof(handlerFuncName));
        }

        var propertyName = handlerFuncName.Split('.').Last();

        var senderInfo = GetEventSenderInfo(this, propertyName);
        
        Client.DispatchDotNetCustomEvent(senderInfo);
    }
    
    /// <summary>
    ///    Dispatch given <paramref name="handlerFunc"/> as event.
    ///    <br/>
    ///    Sample usage:
    /// <code> 
    ///    DispatchEvent(OnUserChanged, [new UserInfo { Name = '..'}] );
    /// </code>
    /// </summary>
    protected void DispatchEvent(Delegate handlerFunc, object[] parameters, [CallerArgumentExpression(nameof(handlerFunc))] string handlerFuncName = null)
    {
        if (handlerFuncName is null)
        {
            throw new ArgumentNullException(nameof(handlerFuncName));
        }

        var propertyName = handlerFuncName.Split('.').Last();

        var senderInfo = GetEventSenderInfo(this, propertyName);
        
        Client.DispatchDotNetCustomEvent(senderInfo, parameters);
    }
    
    protected virtual Element render()
    {
        return NoneOfRender.Value;
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
    
    
    protected virtual Task<Element> renderAsync()
    {
        return Task.FromResult(NoneOfRender.Value);
    }
}

public abstract class Component<TState> : ReactComponentBase where TState : class, new()
{
    protected internal TState state { get; set; }

    internal void InitState(TState value) => state = value;

    internal override bool IsStateNull => state == null;

    protected override Task constructor()
    {
        state ??= new TState();

        return Task.CompletedTask;
    }

    
}

public abstract class Component : Component<EmptyState>
{
    protected static Modifier Modify<TComponent>(Action<TComponent> modifyAction)
        where TComponent : Component => CreateComponentModifier(modifyAction);
}

[Serializable]
public sealed class EmptyState;