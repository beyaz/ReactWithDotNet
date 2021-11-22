using Bridge.Html5;

namespace ReactDotNet
{
    static class ReactElementPreview
    {
        public static bool IsActive { get; set; }

        public static int RenderPreview(string reactComponentTypeName, string targetElementId)
        {
            IsActive = true;

            var componentType = ReflectionHelper.FindComponentTypeByName(reactComponentTypeName);
            if (componentType == null)
            {
                return 0;
            }

            var reactElement = React.createElement(componentType, null);

            ReactDOM.unmountComponentAtNode(Window.Document.GetElementById(targetElementId));
            ReactDOM.render(reactElement, Window.Document.GetElementById(targetElementId));

            return 1;
        }
    }
}