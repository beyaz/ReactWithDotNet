using System;
using System.IO;

namespace ___Module___
{
    static class DirectoryHelper
    {
        public static string GetWorkingDirectory()
        {
            return Path.GetDirectoryName(typeof(DirectoryHelper).Assembly.Location);
        }

        public static string GetFilePath(string fileName) => Path.Combine(DirectoryHelper.GetWorkingDirectory(), fileName);


    }

    static class FileWatcherHelper
    {

        public static (Action startListen, Action stopListen) CreateFileWatcher(string filePath, Action onChange)
        {
            // Create a new FileSystemWatcher and set its properties.
            FileSystemWatcher watcher = new FileSystemWatcher();

            watcher.Path = Path.GetDirectoryName(filePath);

            /* Watch for changes in LastAccess and LastWrite times, and 
               the renaming of files or directories. */
            watcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.Size;


            var lastRead = DateTime.MinValue;


            void onChangeHandler(object s, FileSystemEventArgs e)
            {
                if (e.ChangeType == WatcherChangeTypes.Changed && e.FullPath == filePath)
                {


                    DateTime lastWriteTime = File.GetLastWriteTime(filePath);
                    if (lastWriteTime != lastRead)
                    {
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

    }

}