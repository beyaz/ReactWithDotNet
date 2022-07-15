using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ReactDotNet
{
    public enum ReactComponentEvents
    {
        componentDidMount
    }

    public interface IReactStatelessComponent
    {
        string key { get; }
        Element render();
    }

    public abstract class ReactComponent : Element, IReactStatelessComponent
    {
        public abstract Element render();
    }


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

        public void Insert<TValue>(ReactContextKey<TValue> key, TValue value)
        {
            Insert(key.Key, value);
        }

        public void Insert<TValue>(string key, TValue value)
        {
            if (map.ContainsKey(key))
            {
                map[key] = value;
                return;
            }


            map.Add(key, value);

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
    }



    public static class BrowserInformation
    {
        public static ReactContextKey<double> AvailableWidth = new(nameof(AvailableWidth));
        public static ReactContextKey<double> AvailableHeight = new(nameof(AvailableHeight));
        public static ReactContextKey<IReadOnlyDictionary<string, string>> UrlParameters = new(nameof(UrlParameters));
    }


    public interface IReactStatefulComponent
    {
        Element ___RootNode___ { get; }

        ReactContext Context { get; set; }
        
        string ___Type___ { get; }

        string ___TypeOfState___ { get; }

        bool ___HasComponentDidMountMethod___ { get; }
        
        Element render();
    }

    public abstract class ReactComponent<TState> : Element, IReactStatefulComponent where TState : new()
    {
        [JsonIgnore]
        public ReactContext Context { get; set; }

        public Element ___RootNode___ => render();

        public string ___Type___ => GetType().GetFullName();

        public string ___TypeOfState___ => typeof(TState).GetFullName();

        public bool ___HasComponentDidMountMethod___ => GetType().GetMethod("ComponentDidMount") != null;
        

        public abstract Element render();

        public  TState state { get; protected set; }
    }
}