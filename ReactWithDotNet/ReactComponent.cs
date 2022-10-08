using System.Collections.Specialized;
using System.Text.Json.Serialization;
using System.Web;

namespace ReactWithDotNet;









public sealed class ReactContext
{
    readonly Dictionary<string, object> map = new();

    public TValue TryGetValue<TValue>(ReactContextKey<TValue> key)
    {
        if (map.TryGetValue(key.Key, out var value))
        {
            return (TValue)value;
        }

        return default;
    }

    public void Set<TValue>(ReactContextKey<TValue> key, TValue value)
    {
        Set(key.Key, value);
    }

    public bool Contains<TValue>(ReactContextKey<TValue> key)
    {
        return map.ContainsKey(key.Key);
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

    public double ClientWidth { get; internal set; }
    public double ClientHeight { get; internal set; }
    public NameValueCollection Query { get; internal set; }
    public string QueryAsString=> string.Join("&", Query.AllKeys.Select(key => $"{HttpUtility.UrlEncode(key)}={HttpUtility.UrlEncode(Query[key])}"));

}




// ReSharper disable once UnusedTypeParameter
public sealed class ReactContextKey<TValue>
{
    public readonly string Key;

    public ReactContextKey(string key)
    {
        Key = key;
    }
}



public abstract class ReactStatefulComponent : Element
{
    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    protected internal ReactContext Context { get; set; }

    [JsonIgnore]
    [Newtonsoft.Json.JsonIgnore]
    public ClientTaskCollection ClientTask { get; internal set; } = new();

    protected abstract Element render();

    protected  abstract void constructor();

    internal void InvokeConstructor() => constructor();
    
    internal Element InvokeRender() => render();

    protected virtual void componentDidMount()
    {
        
    }

    internal object Clone()
    {
        return MemberwiseClone();
    }

    internal List<IModifier> modifiers;
    
    protected internal sealed override void ProcessModifier(IModifier modifier)
    {
        if (modifier is null)
        {
            return;
        }
        
        modifiers ??= new List<IModifier>();

        modifiers.Add(modifier);
    }




    /// <summary>
    ///     Sample event decleration <br/>
    ///     [React] public Action OnUserChanged { get; set; }
    /// <br/>
    /// <br/>
    /// Sample event dispatching <br/>
    /// DispatchEvent(()=> OnUserChanged);
    /// </summary>
    protected void DispatchEvent(Expression<Func<Action>> expressionForAccessingCustomReactEventProperty)
    {
        var expression = (MemberExpression)expressionForAccessingCustomReactEventProperty.Body;

        var propertyNameOfCustomReactEvent = expression.Member.Name;

        ClientTask.DispatchEvent(Mixin.GetEventKey(this, propertyNameOfCustomReactEvent));
    }

    /// <summary>
    ///     Sample event decleration <br/>
    ///     [React] public Action&lt;UserInfo&gt; OnUserChanged { get; set; }
    /// <br/>
    /// <br/>
    /// Sample event dispatching <br/>
    /// DispatchEvent(()=> OnUserChanged, state.SelectedUserInfo);
    /// </summary>
    protected void DispatchEvent<A>(Expression<Func<Action<A>>> expressionForAccessingCustomReactEventProperty, A a)
    {
        var expression = (MemberExpression)expressionForAccessingCustomReactEventProperty.Body;
        
        var propertyNameOfCustomReactEvent = expression.Member.Name;

        ClientTask.DispatchEvent(Mixin.GetEventKey(this, propertyNameOfCustomReactEvent), a);
    }

    /// <summary>
    ///     Sample event decleration <br/>
    ///     [React] public Action&lt;UserInfo,OrderInfo&gt; OnUserChanged { get; set; }
    /// <br/>
    /// <br/>
    /// Sample event dispatching <br/>
    /// DispatchEvent(()=> OnUserChanged, state.SelectedUserInfo, state.SelectedOrderInfo);
    /// </summary>
    protected void DispatchEvent<A,B>(Expression<Func<Action<A,B>>> customReactEvent, A a, B b)
    {
        var expression = (MemberExpression)customReactEvent.Body;

        var propertyNameOfCustomReactEvent = expression.Member.Name;

        ClientTask.DispatchEvent(Mixin.GetEventKey(this, propertyNameOfCustomReactEvent), a,b);
    }

    /// <summary>
    ///     Sample event decleration <br/>
    ///     [React] public Action&lt;UserInfo,OrderInfo,CommissionInfo&gt; OnUserChanged { get; set; }
    /// <br/>
    /// <br/>
    /// Sample event dispatching <br/>
    /// DispatchEvent(()=> OnUserChanged, state.SelectedUserInfo, state.SelectedOrderInfo, state.SelectedCommissionInfo);
    /// </summary>
    protected void DispatchEvent<A, B, C>(Expression<Func<Action<A, B>>> customReactEvent, A a, B b, C c)
    {
        var expression = (MemberExpression)customReactEvent.Body;

        var propertyNameOfCustomReactEvent = expression.Member.Name;

        ClientTask.DispatchEvent(Mixin.GetEventKey(this, propertyNameOfCustomReactEvent), a, b, c);
    }

}

public abstract class ReactComponent : ReactComponent<EmptyState>
{
}