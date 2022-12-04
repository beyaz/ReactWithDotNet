using Newtonsoft.Json;

namespace ReactWithDotNet;

static partial class Mixin
{
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
    [JsonProperty]
    public TState state { get; protected internal set; }
    
    protected override void constructor()
    {
        state = new TState();
    }
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