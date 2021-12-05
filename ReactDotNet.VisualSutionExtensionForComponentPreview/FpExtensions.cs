using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;


namespace ___Module___
{
    static class FpExtensions
    {

        public static void Run(string startMessage, Action action, string finalMessage, Action<string> trace)
        {
            try
            {
                trace(startMessage);
                
                action();

                trace(finalMessage);
            }
            catch (Exception e)
            {
                trace(e.ToString());
            }
        }

        public static void Run(string startMessage, Action action, string finalMessage)
        {
            Run(startMessage,action,finalMessage,FileTracer.Trace);
        }

        public static void Run(Action action)
        {
            Run($"Started. Method: {action.Method.Name}", action, $"Finished. Method: {action.Method.Name}", FileTracer.Trace);
        }

        static readonly object _syncObject = new object();
        public static void Log(string message)
        {
            try
            {
                lock (_syncObject)
                {
                    var fs = new FileStream("d:\\ComponentPreviewExtension.log", FileMode.Append);

                    var sb = new StringBuilder();
                    sb.AppendLine(DateTime.Now.ToString("yyyy.MM.dd hh:mm:ss") + " " + message);
                    var sw = new StreamWriter(fs);
                    sw.Write(sb);
                    sw.Close();
                    fs.Close();
                }
            }
            catch
            {
                // ignored
            }
        }

        public static async Task<T> Execute<T,P0>(Func<P0,Task<T>> func, P0 p0)
        {
            var methodName = func.Method.Name;

            return await Execute(methodName, ()=> func(p0));
        }

        public static async Task<T> Execute<T>(Func< Task<T>> func)
        {
            var methodName = func.Method.Name;

            return await Execute(methodName, func);
        }

        public static void RunSynchronously(Func<Task> func)
        {
            var methodName = func.Method.Name;

             Execute(methodName, func).RunSynchronously();
        }

        static async Task<T> Execute<T>(string methodName , Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception e)
            {
                Log(methodName + " - " + e);
                throw;
            }

        }

        static async Task Execute(string methodName, Func<Task> func)
        {
            try
            {
                await func();
            }
            catch (Exception e)
            {
                Log(methodName + " - " + e);
                throw;
            }

        }
    }

}