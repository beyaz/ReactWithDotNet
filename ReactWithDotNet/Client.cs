using System.Reflection;

namespace ReactWithDotNet;

static partial class Mixin
{
    internal static void ConvertReactEventsToTaskForEventBus(this ReactStatefulComponent reactComponent)
    {
        foreach (var propertyInfo in reactComponent.GetType().GetProperties().Where(x => x.GetCustomAttribute<ReactCustomEventAttribute>() is not null))
        {
            var isAction        = propertyInfo.PropertyType.FullName == typeof(Action).FullName;
            var isGenericAction = propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.IsGenericAction1or2or3();

            if (isAction || isGenericAction)
            {
                convertToTask(propertyInfo);
                continue;
            }

            throw DeveloperException("ReactCustomEventAttribute can only use with Action or Action<..>");
        }

        void convertToTask(PropertyInfo propertyInfo)
        {
            var @delegate = (Delegate)propertyInfo.GetValue(reactComponent);
            if (@delegate is null)
            {
                return;
            }

            if (@delegate.Target is ReactStatefulComponent target)
            {
                if (target.ComponentUniqueIdentifier is null)
                {
                    throw DeveloperException("ComponentUniqueIdentifier not initialized yet");
                }

                propertyInfo.SetValue(reactComponent, null);

                reactComponent.Client.InitializeDotnetComponentEventListener(GetEventKey(reactComponent, propertyInfo.Name), @delegate.Method.Name, target.ComponentUniqueIdentifier.GetValueOrDefault());
            }
            else
            {
                throw DeveloperException("Action handler method should belong to React component");
            }
        }
    }

    internal static string GetEventKey(ReactStatefulComponent reactComponent, string propertyName)
    {
        if (reactComponent.ComponentUniqueIdentifier is null)
        {
            throw DeveloperException("ComponentUniqueIdentifier cannot be null");
        }

        return $"{{Property: '{reactComponent.GetType().FullName}::{propertyName}', ComponentUniqueIdentifier: {reactComponent.ComponentUniqueIdentifier}}}";
    }
}

public abstract class ReactComponent<TState> : ReactStatefulComponent where TState : new()
{
    #region Public Properties
    public TState state { get; protected internal set; }
    #endregion

    #region Methods
    protected override void constructor()
    {
        state = new TState();
    }
    #endregion
}

[Serializable]
public sealed class EmptyState
{
}

public sealed class Client
{
    internal readonly List<ClientTask> taskList = new();

    public void CallJsFunction(string jsFunctionPath, params object[] jsFunctionArguments)
    {
        taskList.Add(new ClientTask { JsFunctionPath = jsFunctionPath, JsFunctionArguments = jsFunctionArguments });
    }

    internal sealed class ClientTask
    {
        public object[] JsFunctionArguments { get; set; }
        public string JsFunctionPath { get; set; }
    }
}