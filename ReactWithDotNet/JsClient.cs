namespace ReactWithDotNet;

partial class Mixin
{
    const string core = "ReactWithDotNet::Core::";

    public static void CopyToClipboard(this Client client, string text)
    {
        client.CallJsFunction(core + nameof(CopyToClipboard), text);
    }

    public static void ListenWindowResizeEvent(this Client client, int resizeTimeout)
    {
        client.CallJsFunction(core + nameof(ListenWindowResizeEvent), resizeTimeout);
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
}