using System;

namespace ReactWithDotNet;

[Serializable]
public sealed class ClientTaskCallJsFunction
{
    public int TaskId => 0;
    public string JsFunctionPath { get; set; }
    public object[] JsFunctionArguments { get; set; }
}

[Serializable]
public sealed class ClientTaskListenEvent
{
    public int TaskId => 1;
    public string EventName { get; set; }
    public string RouteToMethod { get; set; }
}

[Serializable]
public sealed class ClientTaskDispatchEvent
{
    public int TaskId => 2;
    public string EventName { get; set; }
    public object[] EventArguments { get; set; }
}

[Serializable]
public sealed class ClientTaskListenComponentEvent
{
    public int TaskId => 3;
    public string EventName { get; set; }
    public string RouteToMethod { get; set; }
}

[Serializable]
public sealed class ClientTaskPushHistory
{
    public int TaskId => 4;
    public string Title { get; set; }
    public string Url { get; set; }
}

[Serializable]
public sealed class ClientTaskComebackWithLastAction
{
    public int TaskId => 5;
    public int Timeout { get; set; }
}

[Serializable]
public sealed class ClientTaskGotoMethod
{
    public int TaskId => 6;
    public int Timeout { get; set; }
    public string MethodName { get; set; }
    public object[] MethodArguments { get; set; }
}

[Serializable]
public sealed class ClientTaskNavigateToUrl
{
    public int TaskId => 7;
    public string Url { get; set; }
}