using System;
using System.IO;

namespace ___Module___
{
    static class FileWatcherHelper
    {
        #region Public Methods
        public static (Action startListen, Action stopListen) CreateFileWatcher(string filePath, Action onChange, Action<string> trace)
        {
            // Create a new FileSystemWatcher and set its properties.
            var watcher = new FileSystemWatcher();

            watcher.Path = Path.GetDirectoryName(filePath);

            /* Watch for changes in LastAccess and LastWrite times, and 
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;

            var lastRead = DateTime.MinValue;

            void onChangeHandler(object s, FileSystemEventArgs e)
            {
                trace("Directory has change.");

                if (e.ChangeType == WatcherChangeTypes.Changed && e.FullPath == filePath)
                {
                    var lastWriteTime = File.GetLastWriteTime(filePath);
                    if (lastWriteTime != lastRead)
                    {
                        trace("Firing change action...");
                        onChange();
                        lastRead = lastWriteTime;
                    }
                }
            }

            watcher.Changed += onChangeHandler;

            void startListen() => watcher.EnableRaisingEvents = true;
            void stopListen() => watcher.EnableRaisingEvents = false;

            return (startListen, stopListen);
        }
        #endregion
    }
}