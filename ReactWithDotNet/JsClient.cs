namespace ReactWithDotNet;

public static class JsClient
{
    const string core = "ReactWithDotNet::Core::";

    public static void CopyToClipboard(ReactStatefulComponent component, string text)
    {
        component.Client.CallJsFunction(core + nameof(CopyToClipboard), text);
    }

    public static void ListenWindowResizeEvent(ReactStatefulComponent component, int resizeTimeout)
    {
        component.Client.CallJsFunction(core + nameof(ListenWindowResizeEvent), resizeTimeout);
    }

    public static void OnWindowResize(ReactStatefulComponent component, Action handlerAction)
    {
        component.Client.ListenEvent(core + nameof(OnWindowResize), handlerAction.Method.Name);
    }

    public static void PushHistory(ReactStatefulComponent component, string title, string url)
    {
        component.Client.CallJsFunction(core + nameof(PushHistory), title, url);
    }

    public static void SetCookie(ReactStatefulComponent component, string cookieName, string cookieValue, int expiredays)
    {
        component.Client.CallJsFunction(core + nameof(SetCookie), cookieName, cookieValue, expiredays);
    }
}