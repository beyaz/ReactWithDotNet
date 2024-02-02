using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace ReactWithDotNet;

public abstract class ReactComponentBase : Element
{
    internal Func<Task<Element>> DesignerCustomizedRender;
    
    internal Style StyleForRootElement;
    
    internal List<Style> classNameList;

    internal List<IModifier> Modifiers;

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
            return _client ??= new ();
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
    ///     Sample event declaration
    ///     <br />
    ///     [ReactCustomEvent] public Func&lt;UserInfo,Task&gt; OnUserChanged { get; set; }
    ///     <br />
    ///     <br />
    ///     Sample event dispatching <br />
    ///     DispatchEvent(()=> OnUserChanged, new UserInfo { Name = '..'});
    /// </summary>
    protected void DispatchEvent(Expression<Func<Delegate>> expressionForAccessingCustomReactEventProperty)
    {
        Client.DispatchDotNetCustomEvent(GetEventSenderInfo(this, GetPropertyNameOfCustomReactEvent((MemberExpression)expressionForAccessingCustomReactEventProperty.Body)));
    }

    
    
    /// <summary>
    ///     Sample event declaration
    ///     <br />
    ///     [ReactCustomEvent] public Func&lt;UserInfo,Task&gt; OnUserChanged { get; set; }
    ///     <br />
    ///     <br />
    ///     Sample event dispatching <br />
    ///     DispatchEvent(()=> OnUserChanged, new UserInfo { Name = '..'});
    /// </summary>
    protected void DispatchEvent<A>(Expression<Func<Delegate>> expressionForAccessingCustomReactEventProperty, A a)
    {
        Client.DispatchDotNetCustomEvent(GetEventSenderInfo(this, GetPropertyNameOfCustomReactEvent((MemberExpression)expressionForAccessingCustomReactEventProperty.Body)), a);
    }
    
    /// <summary>
    ///     Sample event declaration
    ///     <br />
    ///     [ReactCustomEvent] public Func&lt;UserInfo,Task&gt; OnUserChanged { get; set; }
    ///     <br />
    ///     <br />
    ///     Sample event dispatching <br />
    ///     DispatchEvent(()=> OnUserChanged, new UserInfo { Name = '..'});
    /// </summary>
    protected void DispatchEvent<A, B>(Expression<Func<Delegate>> expressionForAccessingCustomReactEventProperty, A a, B b)
    {
        Client.DispatchDotNetCustomEvent(GetEventSenderInfo(this, GetPropertyNameOfCustomReactEvent((MemberExpression)expressionForAccessingCustomReactEventProperty.Body)), a, b);
    }

    /// <summary>
    ///     Sample event declaration
    ///     <br />
    ///     [ReactCustomEvent] public Func&lt;UserInfo,Task&gt; OnUserChanged { get; set; }
    ///     <br />
    ///     <br />
    ///     Sample event dispatching <br />
    ///     DispatchEvent(()=> OnUserChanged, new UserInfo { Name = '..'});
    /// </summary>
    protected void DispatchEvent<A, B, C>(Expression<Func<Delegate>> expressionForAccessingCustomReactEventProperty, A a, B b, C c)
    {
        Client.DispatchDotNetCustomEvent(GetEventSenderInfo(this, GetPropertyNameOfCustomReactEvent((MemberExpression)expressionForAccessingCustomReactEventProperty.Body)), a, b, c);
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

    string GetPropertyNameOfCustomReactEvent(MemberExpression expression)
    {
        var propertyNameOfCustomReactEvent = expression.Member.Name;
        
        if (GetType().GetProperty(propertyNameOfCustomReactEvent)?.GetCustomAttribute<ReactCustomEventAttribute>() is null)
        {
            throw DeveloperException($"{GetType().FullName}::{propertyNameOfCustomReactEvent} should contains 'ReactCustomEvent' attribute.");
        }

        return propertyNameOfCustomReactEvent;
    }
}

public abstract class Component<TState> : ReactComponentBase where TState : class, new()
{
    protected TState state { get; set; }

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
    protected static IModifier Modify<TComponent>(Action<TComponent> modifyAction)
        where TComponent : Component => CreateComponentModifier(modifyAction);
}

[Serializable]
public sealed class EmptyState;