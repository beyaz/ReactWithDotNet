namespace ReactWithDotNet;

public static class JsClient
{
    const string core = "ReactWithDotNet::Core::";

    public static void CopyToClipboard(ReactStatefulComponent component, string text)
    {
        component.ClientTask.CallJsFunction(core + nameof(CopyToClipboard), text);
    }

    public static void ListenWindowResizeEvent(ReactStatefulComponent component, int resizeTimeout)
    {
        component.ClientTask.CallJsFunction(core + nameof(ListenWindowResizeEvent), resizeTimeout);
    }

    public static void OnWindowResize(ReactStatefulComponent component, Action handlerAction)
    {
        component.ClientTask.ListenEvent(core + nameof(OnWindowResize), handlerAction.Method.Name);
    }

    public static void PushHistory(ReactStatefulComponent component, string title, string url)
    {
        component.ClientTask.CallJsFunction(core + nameof(PushHistory), title, url);
    }

    public static void SetCookie(ReactStatefulComponent component, string cookieName, string cookieValue, int expiredays)
    {
        component.ClientTask.CallJsFunction(core + nameof(SetCookie), cookieName, cookieValue, expiredays);
    }
}