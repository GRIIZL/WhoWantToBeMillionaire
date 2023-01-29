using System;
using System.IO;

namespace Кто_хочет_стать_миллионером_НАСЛЕДОВВАНИЕ
{
    
        public class FileHelper
        {
            public static string GetDirectoryName()
            {
                string runningExecutable = System.Diagnostics.Process.GetCurrentProcess().MainModule.FileName;
                FileInfo runningExecutableFileInfo = new FileInfo(runningExecutable);
                return runningExecutableFileInfo.DirectoryName;
            }
            public static string ReadTextFromFile(string filePath)
            {
                using StreamReader sr = new StreamReader(filePath);
                String line = sr.ReadToEnd();
                sr.Close();
                return line;
            }
        }
}
