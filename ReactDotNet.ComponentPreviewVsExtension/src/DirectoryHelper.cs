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
}