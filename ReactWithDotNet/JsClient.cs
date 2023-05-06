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

    public static void HistoryBack(this Client client)
    {
        client.CallJsFunction(core + nameof(HistoryBack));
    }

    public static void HistoryForward(this Client client)
    {
        client.CallJsFunction(core + nameof(HistoryForward));
    }

    public static void HistoryGo(this Client client, int delta)
    {
        client.CallJsFunction(core + nameof(HistoryGo), delta);
    }

    public static void HistoryReplaceState(this Client client, object stateObj, string title, string url)
    {
        client.CallJsFunction(core + nameof(HistoryReplaceState), stateObj, title, url);
    }

    public static void ListenEvent(this Client client, Action<Client> triggerMethod, Action handler)
    {
        ListenEvent(client, triggerMethod.Method.Name, handler.Method.Name);
    }

    public static void ListenEvent<EventArgument1>(this Client client, Action<Client, EventArgument1> triggerMethod, Action<EventArgument1> handler)
    {
        ListenEvent(client, triggerMethod.Method.Name, handler.Method.Name);
    }

    public static void ListenEventOnlyOnce(this Client client, Action<Client> triggerMethod, Action handler)
    {
        ListenEventOnlyOnce(client, triggerMethod.Method.Name, handler.Method.Name);
    }

    public static void ListenWindowResizeEvent(this Client client, int resizeTimeout)
    {
        client.CallJsFunction(core + nameof(ListenWindowResizeEvent), resizeTimeout);
    }

    public static void NavigateToUrl(this Client client, string url)
    {
        client.CallJsFunction(core + nameof(NavigateToUrl), url);
    }

    public static void OnOutsideClicked(this Client client, string idOfElement, Action action)
    {
        if (action.Target is ReactComponentBase target)
        {
            if (target.ComponentUniqueIdentifier is null)
            {
                throw DeveloperException("ComponentUniqueIdentifier not initialized yet. @" + target.GetType().FullName);
            }

            client.CallJsFunction(core + nameof(OnOutsideClicked), idOfElement, action.Method.GetNameWithToken(), target.ComponentUniqueIdentifier.Value);
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

    public static void SetCookie(this Client client, string cookieName, string cookieValue, int expiredays)
    {
        client.CallJsFunction(core + nameof(SetCookie), cookieName, cookieValue, expiredays);
    }

    internal static void DispatchDotNetCustomEvent(this Client client, EventSenderInfo eventName, params object[] eventArguments)
    {
        client.CallJsFunction(core + nameof(DispatchDotNetCustomEvent), eventName, eventArguments);
    }

    internal static void InitializeDotnetComponentEventListener(this Client client, EventSenderInfo eventName, string handlerMethodName, int handlerComponentUniqueIdentifier)
    {
        client.CallJsFunction(core + nameof(InitializeDotnetComponentEventListener), eventName, handlerMethodName, handlerComponentUniqueIdentifier);
    }

    static void ListenEvent(this Client client, string eventName, string routeToMethod)
    {
        client.CallJsFunction(core + nameof(ListenEvent), eventName, routeToMethod);
    }

    static void ListenEventOnlyOnce(Client client, string eventName, string handlerMethodName)
    {
        client.CallJsFunction(core + nameof(ListenEventOnlyOnce), eventName, handlerMethodName);
    }

    #region GotoMethod
    public static void GotoMethod(this Client client, int timeout, Action action)
    {
        GotoMethod(client, timeout, action.Method.GetNameWithToken());
    }

    public static void GotoMethod(this Client client, Action action)
    {
        GotoMethod(client, 0, action.Method.GetNameWithToken());
    }

    public static void GotoMethod<TArgument>(this Client client, int timeout, Action<TArgument> action, TArgument argument)
    {
        GotoMethod(client, timeout, action.Method.GetNameWithToken(), argument);
    }

    public static void GotoMethod<TArgument>(this Client client, Action<TArgument> action, TArgument argument)
    {
        GotoMethod(client, 3, action.Method.GetNameWithToken(), argument);
    }

    public static void GotoMethod<TArgument1, TArgument2>(this Client client, int timeout, Action<TArgument1, TArgument2> action, TArgument1 argument1, TArgument2 argument2)
    {
        GotoMethod(client, timeout, action.Method.GetNameWithToken(), argument1, argument2);
    }

    public static void GotoMethod<TArgument1, TArgument2>(this Client client, Action<TArgument1, TArgument2> action, TArgument1 argument1, TArgument2 argument2)
    {
        GotoMethod(client, 3, action.Method.GetNameWithToken(), argument1, argument2);
    }

    static void GotoMethod(Client client, int timeout, string methodName, params object[] methodArguments)
    {
        client.CallJsFunction(core + nameof(GotoMethod), timeout, methodName, methodArguments);
    }
    #endregion
}