namespace ReactWithDotNet;

public abstract class ReactComponent<TState> : ReactStatefulComponent where TState : new()
{
    #region Public Properties
    public TState state { get; protected set; }
    #endregion

    #region Methods
    protected override void constructor()
    {
        state = new TState();
    }
    #endregion
}

[Serializable]
public class EmptyState
{
}

public sealed class ClientTaskCollection
{
    #region Fields
    internal readonly List<ClientTask> taskList = new();
    #endregion

    #region Public Methods
    public void CallJsFunction(string JsFunctionPath, params object[] JsFunctionArguments)
    {
        taskList.Add(new ClientTask { TaskId = 0, JsFunctionPath = JsFunctionPath, JsFunctionArguments = JsFunctionArguments });
    }

    public void DispatchEvent<EventArgument1>(ClientEventInfo<EventArgument1> eventInfo, EventArgument1 argument)
    {
        DispatchEvent(eventInfo.Name, argument);
    }

    public void DispatchEvent(ClientEventInfo eventInfo)
    {
        DispatchEvent(eventInfo.Name);
    }

    public void GotoMethod(int timeout, Action action)
    {
        GotoMethod(timeout, action.Method.Name);
    }

    public void GotoMethod<TArgument>(int timeout, Action<TArgument> action, TArgument argument)
    {
        GotoMethod(timeout, action.Method.Name, argument);
    }

    public void GotoMethod<TArgument1, TArgument2>(int timeout, Action<TArgument1, TArgument2> action, TArgument1 argument1, TArgument2 argument2)
    {
        GotoMethod(timeout, action.Method.Name, argument1, argument2);
    }

    public void ListenEvent<EventArgument1>(ClientEventInfo<EventArgument1> eventInfo, Action<EventArgument1> routeToMethod)
    {
        ListenEvent(eventInfo.Name, routeToMethod.Method.Name);
    }

    public void ListenEvent(ClientEventInfo eventInfo, Action routeToMethod)
    {
        ListenEvent(eventInfo.Name, routeToMethod.Method.Name);
    }

    public void NavigateToUrl(string url)
    {
        taskList.Add(new ClientTask { TaskId = 7, Url = url });
    }

    public void PushHistory(string title, string url)
    {
        taskList.Add(new ClientTask { TaskId = 4, Title = title, Url = url });
    }
    #endregion

    #region Methods
    internal ClientTask[] ToArray() => taskList.ToArray();

    void DispatchEvent(string eventName, params object[] eventArguments)
    {
        taskList.Add(new ClientTask { TaskId = 2, EventName = eventName, EventArguments = eventArguments });
    }

    void GotoMethod(int timeout, string methodName, params object[] methodArguments)
    {
        taskList.Add(new ClientTask { TaskId = 6, MethodName = methodName, MethodArguments = methodArguments, Timeout = timeout });
    }

    void ListenEvent(string eventName, string routeToMethod)
    {
        taskList.Add(new ClientTask { TaskId = 1, EventName = eventName, RouteToMethod = routeToMethod });
    }
    #endregion
}