namespace ReactWithDotNet;

sealed class EventSenderInfo
{
    public int SenderComponentUniqueIdentifier { get; set; }
    public string SenderPropertyFullName { get; set; }
}

static partial class Mixin
{
    internal static EventSenderInfo GetEventSenderInfo(ReactComponentBase reactComponent, string propertyName)
    {
        if (reactComponent.ComponentUniqueIdentifier == 0)
        {
            throw DeveloperException("ComponentUniqueIdentifier cannot be null");
        }

        return new EventSenderInfo
        {
            SenderPropertyFullName          = $"{reactComponent.GetType().FullName}::{propertyName}",
            SenderComponentUniqueIdentifier = reactComponent.ComponentUniqueIdentifier
        };
    }
    
    public static void OnThirdPartyComponentPropsCalculatedInClient<T>(this Client client, string javascripCode_fn_takes_props_then_return_props) where T: ThirdPartyReactComponent
    {
        client.RunJavascript($"window.ReactWithDotNet.OnThirdPartyComponentPropsCalculated('{typeof(T).FullName}', {javascripCode_fn_takes_props_then_return_props})");
    }
}

public sealed class Client
{
    internal readonly List<ClientTask> TaskList = new();

    public void CallJsFunction(string jsFunctionPath, params object[] jsFunctionArguments)
    {
        TaskList.Add(new ClientTask { JsFunctionPath = jsFunctionPath, JsFunctionArguments = jsFunctionArguments });
    }

    internal sealed class ClientTask
    {
        public object[] JsFunctionArguments { get; set; }
        public string JsFunctionPath { get; set; }
    }
}