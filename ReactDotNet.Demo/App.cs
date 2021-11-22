

namespace ReactDotNet.Demo
{
    public class App
    {
        #region Public Methods
        public static void Main()
        {

            // Extensions.RenderReactElementIn<MainWindow>("app");
            ReactHelper.RenderReactElementIn<TodoRecordListView>("app");
        }
        #endregion
    }
}