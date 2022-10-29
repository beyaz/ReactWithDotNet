using System.Collections.Specialized;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Web;

namespace ReactWithDotNet;

public sealed class ReactContext
{
    readonly Dictionary<string, object> map = new();
    public double ClientHeight { get; internal set; }

    public double ClientWidth { get; internal set; }
    public NameValueCollection Query { get; internal set; }
    public string QueryAsString => string.Join("&", Query.AllKeys.Select(key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(Query[key])}"));

    public bool Contains<TValue>(ReactContextKey<TValue> key)
    {
        return map.ContainsKey(key.Key);
    }

    public void Set<TValue>(ReactContextKey<TValue> key, TValue value)
    {
        Set(key.Key, value);
    }

    public void Set<TValue>(string key, TValue value)
    {
        if (map.ContainsKey(key))
        {
            map[key] = value;
            return;
        }

        map.Add(key, value);
    }

    public TValue TryGetValue<TValue>(ReactContextKey<TValue> key)
    {
        if (map.TryGetValue(key.Key, out var value))
        {
            return (TValue)value;
        }

        return default;
    }

    public TValue TryGetValue<TValue>(string key)
    {
        if (map.TryGetValue(key, out var value))
        {
            return (TValue)value;
        }

        return default;
    }
}

// ReSharper disable once UnusedTypeParameter
public sealed class ReactContextKey<TValue>
{
    public readonly string Key;

    public ReactContextKey(string key)
    {
        Key = key;
    }

    public TValue this[ReactContext reactContext] => reactContext.TryGetValue(this);
}

public abstract class ReactStatefulComponent : Element
{
    internal List<IModifier> modifiers;

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public ClientTaskCollection Client { get; internal set; } = new();

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    protected internal ReactContext Context { get; set; }

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    protected internal int? ComponentUniqueIdentifier { get; set; }

    internal object Clone()
    {
        return MemberwiseClone();
    }

    internal void InvokeConstructor() => constructor();

    internal Element InvokeRender() => render();

    protected internal sealed override void ProcessModifier(IModifier modifier)
    {
        if (modifier is null)
        {
            return;
        }

        modifiers ??= new List<IModifier>();

        modifiers.Add(modifier);
    }

    protected virtual void componentDidMount()
    {
    }

    protected abstract void constructor();

    /// <summary>
    ///     Sample event decleration <br />
    ///     [ReactCustomEvent] public Action OnUserChanged { get; set; }
    ///     <br />
    ///     <br />
    ///     Sample event dispatching <br />
    ///     DispatchEvent(()=> OnUserChanged);
    /// </summary>
    protected void DispatchEvent(Expression<Func<Action>> expressionForAccessingCustomReactEventProperty)
    {
        Client.DispatchEvent(Mixin.GetEventKey(this, GetPropertyNameOfCustomReactEvent((MemberExpression)expressionForAccessingCustomReactEventProperty.Body)));
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
        Client.DispatchEvent(Mixin.GetEventKey(this, GetPropertyNameOfCustomReactEvent((MemberExpression)expressionForAccessingCustomReactEventProperty.Body)), a);
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
        Client.DispatchEvent(Mixin.GetEventKey(this, GetPropertyNameOfCustomReactEvent((MemberExpression)expressionForAccessingCustomReactEventProperty.Body)), a, b);
    }

    /// <summary>
    ///     Sample event decleration <br />
    ///     [ReactCustomEvent] public Action&lt;UserInfo,OrderInfo,CommissionInfo&gt; OnUserChanged { get; set; }
    ///     <br />
    ///     <br />
    ///     Sample event dispatching <br />
    ///     DispatchEvent(()=> OnUserChanged, state.SelectedUserInfo, state.SelectedOrderInfo, state.SelectedCommissionInfo);
    /// </summary>
    protected void DispatchEvent<A, B, C>(Expression<Func<Action<A, B>>> expressionForAccessingCustomReactEventProperty, A a, B b, C c)
    {
        Client.DispatchEvent(Mixin.GetEventKey(this, GetPropertyNameOfCustomReactEvent((MemberExpression)expressionForAccessingCustomReactEventProperty.Body)), a, b, c);
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

public abstract class ReactComponent : ReactComponent<EmptyState>
{
}