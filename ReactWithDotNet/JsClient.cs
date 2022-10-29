namespace ReactWithDotNet;

partial class Mixin
{
    const string core = "ReactWithDotNet::Core::";

    public static void CopyToClipboard(this Client client, string text)
    {
        client.CallJsFunction(core + nameof(CopyToClipboard), text);
    }

    public static void DispatchEvent(this Client client, string eventName, params object[] eventArguments)
    {
        client.CallJsFunction(core + nameof(DispatchEvent), eventName, eventArguments);
    }

    public static void GotoMethod(this Client client, Action action, int timeout)
    {
        GotoMethod(client, timeout, action.Method.Name);
    }

    public static void GotoMethod(this Client client, Action action)
    {
        GotoMethod(client, 0, action.Method.Name);
    }

    public static void GotoMethod<TArgument>(this Client client, Action<TArgument> action, TArgument argument, int timeout)
    {
        GotoMethod(client, timeout, action.Method.Name, argument);
    }

    public static void GotoMethod<TArgument>(this Client client, Action<TArgument> action, TArgument argument)
    {
        GotoMethod(client, 3, action.Method.Name, argument);
    }

    public static void GotoMethod<TArgument1, TArgument2>(this Client client, Action<TArgument1, TArgument2> action, TArgument1 argument1, TArgument2 argument2, int timeout)
    {
        GotoMethod(client, timeout, action.Method.Name, argument1, argument2);
    }

    public static void GotoMethod<TArgument1, TArgument2>(this Client client, Action<TArgument1, TArgument2> action, TArgument1 argument1, TArgument2 argument2)
    {
        GotoMethod(client, 3, action.Method.Name, argument1, argument2);
    }

    public static void ListenEvent(this Client client, Action<Client> attachMethod, Action handler)
    {
        ListenEvent(client, attachMethod.Method.Name, handler.Method.Name);
    }

    public static void ListenEvent<EventArgument1>(this Client client, Action<Client, EventArgument1> attachMethod, Action<EventArgument1> handler)
    {
        ListenEvent(client, attachMethod.Method.Name, handler.Method.Name);
    }

    public static void ListenEventOnlyOnce(Client client, string eventName, Action handler)
    {
        client.CallJsFunction(core + nameof(ListenEventOnlyOnce), eventName, handler.Method.Name);
    }

    public static void ListenWindowResizeEvent(this Client client, int resizeTimeout)
    {
        client.CallJsFunction(core + nameof(ListenWindowResizeEvent), resizeTimeout);
    }

    public static void NavigateToUrl(Client client, string url)
    {
        client.CallJsFunction(core + nameof(NavigateToUrl), url);
    }

    public static void OnOutsideClicked(Client client, string idOfElement, Action action)
    {
        if (action.Target is ReactStatefulComponent target)
        {
            client.CallJsFunction(core + nameof(OnOutsideClicked), idOfElement, action.Method.Name, target.ComponentUniqueIdentifier);
        }
        else
        {
            throw DeveloperException("Action handler method should belong to React component");
        }
    }

    public static void OnWindowResize(this Client client, Action handlerAction)
    {
        client.ListenEvent(core + nameof(OnWindowResize), handlerAction.Method.Name);
    }

    public static void PushHistory(this Client client, string title, string url)
    {
        client.CallJsFunction(core + nameof(PushHistory), title, url);
    }

    public static void SetCookie(this Client client, string cookieName, string cookieValue, int expiredays)
    {
        client.CallJsFunction(core + nameof(SetCookie), cookieName, cookieValue, expiredays);
    }

    static void GotoMethod(Client client, int timeout, string methodName, params object[] methodArguments)
    {
        client.CallJsFunction(core + nameof(GotoMethod), timeout, methodName, methodArguments);
    }

    static void ListenEvent(this Client client, string eventName, string routeToMethod)
    {
        client.CallJsFunction(core + nameof(ListenEvent), eventName, routeToMethod);
    }
}