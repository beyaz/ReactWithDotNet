using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using Newtonsoft.Json;

namespace ReactWithDotNet;

public abstract class ReactComponentBase : Element
{
    internal Func<Element> DesignerCustomizedRender;
    
    internal Style StyleForRootElement;

    internal List<IModifier> Modifiers;

    [System.Text.Json.Serialization.JsonIgnore]
    [JsonIgnore]
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
    [JsonIgnore]
    protected internal Client Client { get; internal set; } = new();

    [System.Text.Json.Serialization.JsonIgnore]
    [JsonIgnore]
    protected internal int? ComponentUniqueIdentifier { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    [JsonIgnore]
    protected internal ReactContext Context { get; internal set; }

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

    internal Element InvokeRender() => DesignerCustomizedRender == null ? render() : DesignerCustomizedRender();

    protected virtual Task componentDidMount()
    {
        return Task.CompletedTask;
    }

    protected abstract Task constructor();

    /// <summary>
    ///     Sample event declaration <br />
    ///     [ReactCustomEvent] public Action OnUserChanged { get; set; }
    ///     <br />
    ///     <br />
    ///     Sample event dispatching <br />
    ///     DispatchEvent(()=> OnUserChanged);
    /// </summary>
    protected void DispatchEvent(Expression<Func<Action>> expressionForAccessingCustomReactEventProperty)
    {
        Client.DispatchDotNetCustomEvent(GetEventSenderInfo(this, GetPropertyNameOfCustomReactEvent((MemberExpression)expressionForAccessingCustomReactEventProperty.Body)));
    }

    /// <summary>
    ///     Sample event decleration <br />
    ///     [ReactCustomEvent] public Action&lt;UserInfo&gt; OnUserChanged { get; set; }
    ///     <br />
    ///     <br />
    ///     Sample event dispatching <br />
    ///     DispatchEvent(()=> OnUserChanged, state.SelectedUserInfo);
    /// </summary>
    protected void DispatchEvent<A>(Expression<Func<Action<A>>> expressionForAccessingCustomReactEventProperty, A a)
    {
        Client.DispatchDotNetCustomEvent(GetEventSenderInfo(this, GetPropertyNameOfCustomReactEvent((MemberExpression)expressionForAccessingCustomReactEventProperty.Body)), a);
    }

    /// <summary>
    ///     Sample event decleration <br />
    ///     [ReactCustomEvent] public Action&lt;UserInfo,OrderInfo&gt; OnUserChanged { get; set; }
    ///     <br />
    ///     <br />
    ///     Sample event dispatching <br />
    ///     DispatchEvent(()=> OnUserChanged, state.SelectedUserInfo, state.SelectedOrderInfo);
    /// </summary>
    protected void DispatchEvent<A, B>(Expression<Func<Action<A, B>>> expressionForAccessingCustomReactEventProperty, A a, B b)
    {
        Client.DispatchDotNetCustomEvent(GetEventSenderInfo(this, GetPropertyNameOfCustomReactEvent((MemberExpression)expressionForAccessingCustomReactEventProperty.Body)), a, b);
    }

    /// <summary>
    ///     Sample event declaration <br />
    ///     [ReactCustomEvent] public Action&lt;UserInfo,OrderInfo,CommissionInfo&gt; OnUserChanged { get; set; }
    ///     <br />
    ///     <br />
    ///     Sample event dispatching <br />
    ///     DispatchEvent(()=> OnUserChanged, state.SelectedUserInfo, state.SelectedOrderInfo, state.SelectedCommissionInfo);
    /// </summary>
    protected void DispatchEvent<A, B, C>(Expression<Func<Action<A, B>>> expressionForAccessingCustomReactEventProperty, A a, B b, C c)
    {
        Client.DispatchDotNetCustomEvent(GetEventSenderInfo(this, GetPropertyNameOfCustomReactEvent((MemberExpression)expressionForAccessingCustomReactEventProperty.Body)), a, b, c);
    }

    protected abstract Element render();

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

public abstract class ReactComponent<TState> : ReactComponentBase where TState : new()
{
    [JsonProperty]
    public TState state { get; protected internal set; }

    internal override bool IsStateNull => state == null;

    protected override Task constructor()
    {
        state = new TState();

        return Task.CompletedTask;
    }
}

public abstract class ReactComponent : ReactComponent<EmptyState>
{
    protected static IModifier Modify<TComponent>(Action<TComponent> modifyAction)
        where TComponent : ReactComponent => CreateComponentModifier(modifyAction);
}

[Serializable]
public sealed class EmptyState;