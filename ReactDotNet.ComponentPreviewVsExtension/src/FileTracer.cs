using System;
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
                    var targetFilePath = DirectoryHelper.GetFilePath("ReactDotNet.ComponentPreviewVsExtension.trace.txt");

                    using (var fs = new FileStream(targetFilePath, FileMode.Append))
                    {
                        using (var sw = new StreamWriter(fs))
                        {
                            sw.Write(message + Environment.NewLine);
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