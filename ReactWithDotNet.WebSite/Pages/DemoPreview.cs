using System.Web;
using Microsoft.Net.Http.Headers;

namespace ReactWithDotNet.WebSite.Pages;

sealed class DemoPreview : Component
{
    public const string QueryParameterNameOfFullTypeName = "fullTypeName";

    protected override Element render()
    {
        var fullTypeName = GetQuery(QueryParameterNameOfFullTypeName);
        if (fullTypeName is null)
        {
            return "Element is empty";
        }

        var elementType = Type.GetType(fullTypeName);
        if (elementType is null)
        {
            return $"Element not found @fullTypeName: {fullTypeName}";
        }

        try
        {
            return (Element)Activator.CreateInstance(elementType);
        }
        catch (Exception exception)
        {
            return new pre(Color(Red)) { exception.ToString() };
        }
    }

    string GetQuery(string name)
    {
        var value = Context.HttpContext.Request.Query[name].FirstOrDefault();
        if (value != null)
        {
            return value;
        }

        var referer = Context.HttpContext.Request.Headers[HeaderNames.Referer];
        if (string.IsNullOrWhiteSpace(referer))
        {
            return null;
        }

        var nameValueCollection = HttpUtility.ParseQueryString(new Uri(referer).Query);

        return nameValueCollection[name];
    }
}