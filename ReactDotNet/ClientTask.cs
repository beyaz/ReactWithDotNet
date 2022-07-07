using System;

namespace ReactDotNet.Html5;


[Serializable]
public abstract class ClientTask
{
    public ClientTask After { get; set; } 
}

[Serializable]
public sealed class ClientTaskCallJsFunction : ClientTask
{
    public int TaskId => 0;
    public string JsFunctionPath{ get; set; }
    public object[] JsFunctionArguments { get; set; }
}

[Serializable]
public sealed class ClientTaskListenEvent : ClientTask
{
    public int TaskId => 1;
    public string EventName { get; set; }
    public string RouteToMethod { get; set; }
}

[Serializable]
public sealed class ClientTaskDispatchEvent: ClientTask
{
    public int TaskId => 2;
    public string EventName { get; set; }
    public object[] EventArguments { get; set; }
}

[Serializable]
public sealed class ClientTaskListenComponentEvent : ClientTask
{
    public int TaskId => 3;
    public string EventName { get; set; }
    public string RouteToMethod { get; set; }
}

[Serializable]
public sealed class ClientTaskPushHistory: ClientTask
{
    public int TaskId => 4;
    public string Title { get; set; }
    public string Url { get; set; }
}

[Serializable]
public sealed class ClientTaskComebackWithLastAction: ClientTask
{
    public int TaskId => 5;
    public int Timeout { get; set; }
}
[Serializable]
public sealed class ClientTaskGotoMethod : ClientTask
{
    public int TaskId => 6;
    public int Timeout { get; set; }
    public string  MethodName { get; set; }
    public object[] MethodArguments { get; set; }
}

[Serializable]
public sealed class ClientTaskNavigateToUrl : ClientTask
{
    public int TaskId => 7;
    public string Url { get; set; }
}

