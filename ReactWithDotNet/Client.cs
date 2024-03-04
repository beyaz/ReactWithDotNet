namespace ReactWithDotNet;

sealed class EventSenderInfo
{
    public int SenderComponentUniqueIdentifier { get; set; }
    public string SenderPropertyFullName { get; set; }
}

static partial class Mixin
{
    public static void OnThirdPartyComponentPropsCalculatedInClient<T>(this Client client, string javascripCode_fn_takes_props_then_return_props) where T : ThirdPartyReactComponent
    {
        client.RunJavascript($"window.ReactWithDotNet.OnThirdPartyComponentPropsCalculated('{typeof(T).FullName}', {javascripCode_fn_takes_props_then_return_props})");
    }

    internal static EventSenderInfo GetEventSenderInfo(ReactComponentBase reactComponent, string propertyName)
    {
        if (reactComponent.ComponentUniqueIdentifier == 0)
        {
            throw DeveloperException("ComponentUniqueIdentifier cannot be null");
        }

        return new()
        {
            SenderPropertyFullName          = $"{reactComponent.GetType().FullName}::{propertyName}",
            SenderComponentUniqueIdentifier = reactComponent.ComponentUniqueIdentifier
        };
    }
}

public sealed class Client
{
    internal Client Clone()
    {
        if (TaskList.Count == 0)
        {
            return new(_reactContext);
        }

        var cloned = new Client(_reactContext);
        
        cloned.TaskList.AddRange(TaskList);

        return cloned;
    }
    
    
    internal readonly List<ClientTask> TaskList = new();
    readonly ReactContext _reactContext;

    internal Client(ReactContext reactContext)
    {
        _reactContext = reactContext;
    }

    /// <summary>
    ///     Client size information will be available after the first request.
    /// </summary>
    public double? Height => _reactContext.ClientWidth;

    /// <summary>
    ///     Client size information will be available after the first request.
    /// </summary>
    public double? Width => _reactContext.ClientWidth;

    public void CallJsFunction(string jsFunctionPath, params object[] jsFunctionArguments)
    {
        TaskList.Add(new() { JsFunctionPath = jsFunctionPath, JsFunctionArguments = jsFunctionArguments });
    }

    internal sealed class ClientTask
    {
        public object[] JsFunctionArguments { get; set; }
        public string JsFunctionPath { get; set; }
    }
    
    /// <summary>
    ///  Sample usage:
    ///     <code>
    ///     
    ///      if (Client.WidthHasMatch(SM))
    ///      {
    ///          // 
    ///      }
    ///     
    ///     </code>
    /// </summary>
    public bool WidthHasMatch(Func<StyleModifier[],StyleModifier> mediaSizeFunction)
    {
        if (mediaSizeFunction == SM)
        {
            return Width >= 640;
        }
        
        if (mediaSizeFunction == MD)
        {
            return Width >= 768;
        }
        
        if (mediaSizeFunction == LG)
        {
            return Width >= 1024;
        }
        
        if (mediaSizeFunction == XL)
        {
            return Width >= 1280;
        }
        
        if (mediaSizeFunction == XXL)
        {
            return Width >= 1536;
        }

        throw DeveloperException("Use common media size functions: SM, MD, LG, XL, XXL");
    }
}