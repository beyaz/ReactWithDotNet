using Bridge;

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

            // ReSharper disable once UnusedVariable
            var reactElement = ReactHelper.CreateReactElement(componentType, null);

            Script.Write("ReactDOM.unmountComponentAtNode( document.getElementById(targetElementId) )");
            Script.Write("ReactDOM.render(reactElement, document.getElementById(targetElementId))");

            return 1;
        }
    }
}