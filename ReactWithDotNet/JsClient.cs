﻿namespace ReactWithDotNet;

partial class Mixin
{
    const string Core = "ReactWithDotNet::Core::";

    public static void CopyToClipboard(this Client client, string text)
    {
        client.CallJsFunction(Core + nameof(CopyToClipboard), text);
    }

    public static void DispatchEvent<TDelegate>(this Client client, object[] eventArguments = null, string senderId = null) where TDelegate : Delegate
    {
        client.DispatchEvent(GetEventName<TDelegate>(senderId), eventArguments);
    }

    public static void DispatchEvent(this Client client, string eventName)
    {
        client.DispatchEvent(eventName, null);
    }

    public static void DispatchEvent(this Client client, string eventName, object[] eventArguments)
    {
        client.DispatchEvent(eventName, eventArguments, 0);
    }

    public static void DispatchEvent(this Client client, string eventName, double delayTimeoutInMilliseconds)
    {
        client.DispatchEvent(eventName, null, delayTimeoutInMilliseconds);
    }

    public static void DispatchEvent(this Client client, string eventName, TimeSpan delayTimeout)
    {
        client.DispatchEvent(eventName, null, delayTimeout.TotalMilliseconds);
    }

    public static void DispatchEvent(this Client client, string eventName, object[] eventArguments, double delayTimeoutInMilliseconds)
    {
        client.CallJsFunction(Core + nameof(DispatchEvent), eventName, eventArguments ?? [], delayTimeoutInMilliseconds);
    }

    public static void DispatchEvent(this Client client, string eventName, object[] eventArguments, TimeSpan delayTimeout)
    {
        client.DispatchEvent(eventName, eventArguments, delayTimeout.TotalMilliseconds);
    }

    public static void DispatchEvent(this Client client, Enum eventName, object[] eventArguments = null)
    {
        DispatchEvent(client, eventName.ToString(), eventArguments);
    }

    public static void HistoryBack(this Client client)
    {
        client.CallJsFunction(Core + nameof(HistoryBack));
    }

    public static void HistoryForward(this Client client)
    {
        client.CallJsFunction(Core + nameof(HistoryForward));
    }

    public static void HistoryGo(this Client client, int delta)
    {
        client.CallJsFunction(Core + nameof(HistoryGo), delta);
    }

    public static void HistoryReplaceState(this Client client, object stateObj, string title, string url)
    {
        // avoid too many push history
        client.TaskList.RemoveAll(x => x.JsFunctionPath == Core + nameof(HistoryReplaceState));
        
        client.CallJsFunction(Core + nameof(HistoryReplaceState), stateObj, title, url);
    }

    public static void ListenEvent<TDelegate>(this Client client, TDelegate handler, string senderId = null) where TDelegate : Delegate
    {
        ListenEvent(client, GetEventName<TDelegate>(senderId), handler.Method.GetAccessKey());
    }
    
    public static void ListenEvent(this Client client, string eventName, Func<Task> handler)
    {
        ListenEvent(client, eventName, handler.Method.GetAccessKey());
    }

    public static void ListenEvent(this Client client, string eventName, Func<string, Task> handler)
    {
        ListenEvent(client, eventName, handler.Method.GetAccessKey());
    }

    public static void ListenEvent(this Client client, string eventName, Func<int, Task> handler)
    {
        ListenEvent(client, eventName, handler.Method.GetAccessKey());
    }

    public static void ListenEvent<TEventArgument1>(this Client client, string eventName, Func<TEventArgument1, Task> handler)
    {
        ListenEvent(client, eventName, handler.Method.GetAccessKey());
    }

    public static void ListenEvent<TEventArgument1>(this Client client, Enum eventName, Func<TEventArgument1, Task> handler)
    {
        ListenEvent(client, eventName.ToString(), handler.Method.GetAccessKey());
    }

    public static void ListenEvent(this Client client, Enum eventName, Func<Task> handler)
    {
        ListenEvent(client, eventName.ToString(), handler.Method.GetAccessKey());
    }

    public static void ListenEventOnlyOnce(this Client client, Func<Client, Task> triggerMethod, Func<Task> handler)
    {
        ListenEventOnlyOnce(client, triggerMethod.Method.Name, handler.Method.GetAccessKey());
    }

    public static void ListenEventOnlyOnce<TDelegate>(this Client client, TDelegate handler) where TDelegate : Delegate
    {
        ListenEventOnlyOnce(client, GetEventName<TDelegate>(), handler.Method.GetAccessKey());
    }
    
    public static void ListenWindowResizeEvent(this Client client, int resizeTimeout, int thresholdLength = 50)
    {
        client.CallJsFunction(Core + nameof(ListenWindowResizeEvent), resizeTimeout);
    }

    /// <summary>
    ///     Example:
    ///     <br />
    ///     Client.NavigateTo("/") navigates to home page
    ///     <br />
    ///     Client.NavigateTo("/AboutUs") navigates to About Us page
    /// </summary>
    public static void NavigateTo(this Client client, string path)
    {
        client.CallJsFunction(Core + nameof(NavigateTo), path);
    }

    public static void OnWindowResize(this Client client, Func<Task> handler)
    {
        client.ListenEvent(Core + nameof(OnWindowResize), handler.Method.GetAccessKey());
    }

    public static void RunJavascript(this Client client, string javascriptCodeWillBeExecuteInClient)
    {
        client.CallJsFunction(Core + nameof(RunJavascript), javascriptCodeWillBeExecuteInClient);
    }

    public static void SetCookie(this Client client, string cookieName, string cookieValue, int expiredays)
    {
        client.CallJsFunction(Core + nameof(SetCookie), cookieName, cookieValue, expiredays);
    }

    internal static void DispatchDotNetCustomEvent(this Client client, EventSenderInfo eventName, object[] eventArguments = null)
    {
        eventArguments ??= [];

        foreach (var argument in eventArguments)
        {
            if (argument is Element)
            {
                throw DeveloperException($"Invalid arguments for DispatchEvent . Element type('{argument.GetType().FullName}') cannot serialize to client.");
            }
        }

        client.CallJsFunction(Core + nameof(DispatchDotNetCustomEvent), eventName, eventArguments);
    }

    internal static void InitializeDotnetComponentEventListener(this Client client, EventSenderInfo eventName, string handlerMethodName, int handlerComponentUniqueIdentifier)
    {
        client.CallJsFunction(Core + nameof(InitializeDotnetComponentEventListener), eventName, handlerMethodName, handlerComponentUniqueIdentifier);
    }

    static void ListenEvent(this Client client, string eventName, string routeToMethod)
    {
        client.CallJsFunction(Core + nameof(ListenEvent), eventName, routeToMethod);
    }

    static void ListenEventOnlyOnce(Client client, string eventName, string handlerMethodName)
    {
        client.CallJsFunction(Core + nameof(ListenEventOnlyOnce), eventName, handlerMethodName);
    }

    #region GotoMethod

    public static void GotoMethod(this Client client, int timeoutInMilliseconds, Func<Task> func)
    {
        GotoMethod(client, timeoutInMilliseconds, func.Method.GetAccessKey());
    }

    public static void GotoMethod(this Client client, Func<Task> func)
    {
        GotoMethod(client, 0, func.Method.GetAccessKey());
    }
    
    public static void GotoMethod(this Client client, Func<Task> func, TimeSpan delayTimeout)
    {
        GotoMethod(client, delayTimeout.TotalMilliseconds, func.Method.GetAccessKey());
    }

    public static void GotoMethod<TArgument>(this Client client, int timeoutInMilliseconds, Func<TArgument, Task> func, TArgument argument)
    {
        GotoMethod(client, timeoutInMilliseconds, func.Method.GetAccessKey(), argument);
    }

    public static void GotoMethod<TArgument>(this Client client, Func<TArgument, Task> func, TArgument argument)
    {
        GotoMethod(client, 3, func.Method.GetAccessKey(), argument);
    }

    public static void GotoMethod<TArgument1, TArgument2>(this Client client, int timeoutInMilliseconds, Func<TArgument1, TArgument2, Task> func, TArgument1 argument1, TArgument2 argument2)
    {
        GotoMethod(client, timeoutInMilliseconds, func.Method.GetAccessKey(), argument1, argument2);
    }

    public static void GotoMethod<TArgument1, TArgument2>(this Client client, Func<TArgument1, TArgument2, Task> func, TArgument1 argument1, TArgument2 argument2)
    {
        GotoMethod(client, 3, func.Method.GetAccessKey(), argument1, argument2);
    }

    static void GotoMethod(Client client, double timeoutInMilliseconds, string methodName, params object[] methodArguments)
    {
        client.CallJsFunction(Core + nameof(GotoMethod), timeoutInMilliseconds, methodName, methodArguments);
    }

    #endregion
    
    static string GetEventName<TDelegate>(string senderId = null) where TDelegate : Delegate
    {
        var name =  typeof(TDelegate).FullName;

        if (string.IsNullOrWhiteSpace(senderId))
        {
            return name;
        }

        return $"{name} & {senderId}";
    }
}

// ReSharper disable once UnusedTypeParameter
public sealed class EventName<TArgument>
{
    public string Name { get; init; }
}

static partial class Mixin
{
    public static readonly EventName<MouseEvent> click = new()
    {
        Name = "click"
    };
    
    public static readonly EventName<MouseEvent> OutsideClick = new()
    {
        Name = "OutsideClick"
    };
    
    public static void AddEventListener<TEventArgument>(string idOfElement, EventName<TEventArgument> eventName, Func<TEventArgument,Task> handlerMethod, IFunctionalComponent callerComponent)
    {
        if (callerComponent == null)
        {
            throw new ArgumentNullException(nameof(callerComponent));
        }

        AddEventListener(idOfElement, eventName, handlerMethod, (ReactComponentBase)callerComponent);
    }
    
    public static void AddEventListener<TEventArgument>(string idOfElement, EventName<TEventArgument> eventName, Func<TEventArgument,Task> handlerMethod, ReactComponentBase callerComponent)
    {
        if (callerComponent == null)
        {
            throw new ArgumentNullException(nameof(callerComponent));
        }

        if (string.IsNullOrWhiteSpace(idOfElement))
        {
            throw new ArgumentException(nameof(idOfElement));
        }
        
        if (handlerMethod == null)
        {
            throw new ArgumentNullException(nameof(handlerMethod));
        }
        
        var caller = callerComponent;

        if (caller.ComponentUniqueIdentifier == 0)
        {
            throw DeveloperException("ComponentUniqueIdentifier not initialized yet. @" + caller.GetType().FullName);
        }

        caller.Client.CallJsFunction(Core + nameof(AddEventListener), [idOfElement,eventName.Name, handlerMethod.Method.GetAccessKey(), caller.ComponentUniqueIdentifier]);
    }

    
    public static void RemoveEventListener<TEventArgument>(string idOfElement, EventName<TEventArgument> eventName, Func<TEventArgument,Task> handlerMethod, IFunctionalComponent callerComponent)
    {
        if (callerComponent == null)
        {
            throw new ArgumentNullException(nameof(callerComponent));
        }

        if (string.IsNullOrWhiteSpace(idOfElement))
        {
            throw new ArgumentException(nameof(idOfElement));
        }
        
        if (handlerMethod == null)
        {
            throw new ArgumentNullException(nameof(handlerMethod));
        }
        
        var caller = (FunctionalComponent)callerComponent;

        if (caller.ComponentUniqueIdentifier == 0)
        {
            throw DeveloperException("ComponentUniqueIdentifier not initialized yet. @" + caller.GetType().FullName);
        }

        caller.Client.CallJsFunction(Core + nameof(RemoveEventListener), [idOfElement,eventName.Name, handlerMethod.Method.GetAccessKey(), caller.ComponentUniqueIdentifier]);
    }
}