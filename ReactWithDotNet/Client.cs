namespace ReactWithDotNet;

sealed class EventSenderInfo
{
    public string SenderPropertyFullName { get; set; }
    
    public int SenderComponentUniqueIdentifier { get; set; }
}
static partial class Mixin
{
    internal static EventSenderInfo GetEventSenderInfo(ReactStatefulComponent reactComponent, string propertyName)
    {
        if (reactComponent.ComponentUniqueIdentifier is null)
        {
            throw DeveloperException("ComponentUniqueIdentifier cannot be null");
        }

        return new EventSenderInfo
        {
            SenderPropertyFullName          = $"{reactComponent.GetType().FullName}::{propertyName}",
            SenderComponentUniqueIdentifier = reactComponent.ComponentUniqueIdentifier.Value
        };
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