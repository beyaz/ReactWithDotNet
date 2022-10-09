
namespace ReactWithDotNet;

sealed class ClientTask
{
    public int TaskId { get; set; }
    public string JsFunctionPath { get; set; }
    public object[] JsFunctionArguments { get; set; }

    public string Title { get; set; }
    public string Url { get; set; }

    public string EventName { get; set; }
    public string RouteToMethod { get; set; }

    public object[] EventArguments { get; set; }

    public int Timeout { get; set; }
    public string MethodName { get; set; }
    public object[] MethodArguments { get; set; }
    public string HandlerComponentKey { get; set; }
    public string IdOfElement { get; set; }
}