

using Bridge.Html5;

namespace ReactDotNet.Demo
{
    public class App
    {
        #region Public Methods
        public static void Main()
        {


            ReactDOM.render(React.createElement(typeof(TodoRecordListView)),Window.Document.GetElementById("app"));

        }
        #endregion
    }
}