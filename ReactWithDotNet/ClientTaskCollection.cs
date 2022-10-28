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
                propertyInfo.SetValue(reactComponent, null);

                reactComponent.ClientTask.taskList.Add(new ClientTask
                {
                    TaskId              = (int)TaskId.InitializeDotnetComponentEventListener,
                    EventName           = GetEventKey(reactComponent, propertyInfo.Name),
                    HandlerComponentUniqueIdentifier = target.ComponentUniqueIdentifier,
                    RouteToMethod       = @delegate.Method.Name
                });
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

public sealed class ClientTaskCollection
{
    #region Fields
    internal readonly List<ClientTask> taskList = new();
    #endregion

    #region Public Methods
    public void CallJsFunction(JsClientFunctionInfo info)
    {
        CallJsFunction(info.Name);
    }

    public void CallJsFunction<P1>(JsClientFunctionInfo<P1> info, P1 parameter)
    {
        CallJsFunction(info.Name, parameter);
    }

    public void CallJsFunction<P1, P2>(JsClientFunctionInfo<P1, P2> info, P1 parameter1, P2 parameter2)
    {
        CallJsFunction(info.Name, parameter1, parameter2);
    }

    public void CallJsFunction<P1, P2, P3>(JsClientFunctionInfo<P1, P2, P3> info, P1 parameter1, P2 parameter2, P3 parameter3)
    {
        CallJsFunction(info.Name, parameter1, parameter2, parameter3);
    }

    public void DispatchEvent<EventArgument1>(JsClientEventInfo<EventArgument1> eventInfo, EventArgument1 argument)
    {
        DispatchEvent(eventInfo.Name, argument);
    }

    public void DispatchEvent(JsClientEventInfo eventInfo)
    {
        DispatchEvent(eventInfo.Name);
    }

    public void GotoMethod(int timeout, Action action)
    {
        GotoMethod(timeout, action.Method.Name);
    }
    public void GotoMethod(Action action)
    {
        GotoMethod(0, action.Method.Name);
    }

    public void GotoMethod<TArgument>(int timeout, Action<TArgument> action, TArgument argument)
    {
        GotoMethod(timeout, action.Method.Name, argument);
    }
    
    public void OnOutsideClicked(string idOfElement, Action action)
    {
        if (action.Target is ReactStatefulComponent target)
        {
            taskList.Add(new ClientTask
            {
                TaskId        = (int)TaskId.OnOutsideClicked,
                IdOfElement   = idOfElement,
                RouteToMethod = action.Method.Name,
                HandlerComponentUniqueIdentifier = target.ComponentUniqueIdentifier
            });
        }
        else
        {
            throw DeveloperException("Action handler method should belong to React component");
        }
            
        
    }

    public void GotoMethod<TArgument1, TArgument2>(int timeout, Action<TArgument1, TArgument2> action, TArgument1 argument1, TArgument2 argument2)
    {
        GotoMethod(timeout, action.Method.Name, argument1, argument2);
    }

    public void ListenEvent<EventArgument1>(JsClientEventInfo<EventArgument1> eventInfo, Action<EventArgument1> routeToMethod)
    {
        ListenEvent(eventInfo.Name, routeToMethod.Method.Name);
    }

    public void ListenEvent(JsClientEventInfo eventInfo, Action routeToMethod)
    {
        ListenEvent(eventInfo.Name, routeToMethod.Method.Name);
    }

    public void ListenEventOnlyOnce(JsClientEventInfo eventInfo, Action routeToMethod)
    {
        taskList.Add(new ClientTask { TaskId = (int)TaskId.ListenEventOnlyOnce, EventName = eventInfo.Name, RouteToMethod = routeToMethod.Method.Name });
    }

    public void NavigateToUrl(string url)
    {
        taskList.Add(new ClientTask { TaskId = (int)TaskId.NavigateToUrl, Url = url });
    }

    public void PushHistory(string title, string url)
    {
        taskList.Add(new ClientTask { TaskId = (int)TaskId.PushHistory, Title = title, Url = url });
    }
    #endregion

    #region Methods
    internal ClientTask[] ToArray() => taskList.ToArray();

    void CallJsFunction(string JsFunctionPath, params object[] JsFunctionArguments)
    {
        taskList.Add(new ClientTask { TaskId = (int)TaskId.CallJsFunction, JsFunctionPath = JsFunctionPath, JsFunctionArguments = JsFunctionArguments });
    }

    internal void DispatchEvent(string eventName, params object[] eventArguments)
    {
        taskList.Add(new ClientTask { TaskId = (int)TaskId.DispatchEvent, EventName = eventName, EventArguments = eventArguments });
    }

    void GotoMethod(int timeout, string methodName, params object[] methodArguments)
    {
        taskList.Add(new ClientTask { TaskId = (int)TaskId.GotoMethod, MethodName = methodName, MethodArguments = methodArguments, Timeout = timeout });
    }

    internal void ListenEvent(string eventName, string routeToMethod)
    {
        taskList.Add(new ClientTask { TaskId = (int)TaskId.ListenEvent, EventName = eventName, RouteToMethod = routeToMethod });
    }
    #endregion

   
}
enum TaskId
{
    CallJsFunction = 1,
    ListenEvent = 2,
    DispatchEvent = 3,
    PushHistory = 4,
    InitializeDotnetComponentEventListener = 5,
    GotoMethod = 6,
    NavigateToUrl = 7,
    OnOutsideClicked =8,
    ListenEventOnlyOnce = 9
}
public class JsClientEventInfo
{
    #region Fields
    public readonly string Name;
    #endregion

    #region Constructors
    public JsClientEventInfo(string name)
    {
        Name = name;
    }
    #endregion
}

public sealed class JsClientEventInfo<EventArgument1> : JsClientEventInfo
{
    #region Constructors
    public JsClientEventInfo(string name) : base(name)
    {
    }
    #endregion
}

#region JsClientFunctionInfo
public class JsClientFunctionInfo
{
    #region Fields
    public readonly string Name;
    #endregion

    #region Constructors
    public JsClientFunctionInfo(string name)
    {
        Name = name;
    }
    #endregion
}

public sealed class JsClientFunctionInfo<EventArgument1> : JsClientFunctionInfo
{
    #region Constructors
    public JsClientFunctionInfo(string name) : base(name)
    {
    }
    #endregion
}

public sealed class JsClientFunctionInfo<EventArgument1, EventArgument2> : JsClientFunctionInfo
{
    #region Constructors
    public JsClientFunctionInfo(string name) : base(name)
    {
    }
    #endregion
}

public sealed class JsClientFunctionInfo<EventArgument1, EventArgument2, EventArgument3> : JsClientFunctionInfo
{
    #region Constructors
    public JsClientFunctionInfo(string name) : base(name)
    {
    }
    #endregion
}
#endregion

public static class JsClient
{
    #region Static Fields
    public static JsClientFunctionInfo<string> CopyToClipboard = new(nameof(CopyToClipboard));

    public static JsClientFunctionInfo<int> ListenWindowResizeEvent = new(nameof(ListenWindowResizeEvent));

    public static JsClientEventInfo WindowResize = new(nameof(WindowResize));
    #endregion
}