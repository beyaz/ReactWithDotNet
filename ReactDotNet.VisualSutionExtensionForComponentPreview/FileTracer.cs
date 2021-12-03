using System.IO;

namespace ___Module___
{
    static class FileTracer
    {
        static readonly object _syncObject = new object();

        public static void Trace(string message)
        {
            

            try
            {
                lock (_syncObject)
                {
                    var targetFilePath = DirectoryHelper.GetFilePath("ReactDotNet.VsComponentPreview.trace.txt");

                    using (var fs = new FileStream(targetFilePath, FileMode.CreateNew))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            sw.Write(message);
                        }
                    }
                }
            }
            catch
            {
                // ignored
            }

        }
    }
}