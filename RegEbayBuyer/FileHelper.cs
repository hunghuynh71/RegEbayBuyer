using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreDogeTool
{
    class FileHelper
    {
        private static object locker = new object();

        public static void WriteAppendToFile(string filepath, string text)
        {
            lock (locker)
            {
                using (FileStream stream = new FileStream(filepath, FileMode.Append, FileAccess.Write, FileShare.Read))
                {
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.Unicode))
                    {
                        writer.Write(text + Environment.NewLine);
                    }
                }
            }
        }
        public static string ReadFile(string filepath)
        {
            lock (locker)
            {
                if (!File.Exists(filepath))
                    return "";
                return File.ReadAllText(filepath).Trim();
            }
        }

        public static void WriteToFile(string filepath, string text)
        {
            lock (locker)
            {
                using (FileStream stream = new FileStream(filepath, FileMode.Create, FileAccess.Write, FileShare.Read))
                {
                    using (StreamWriter writer = new StreamWriter(stream, Encoding.UTF8))
                    {
                        writer.Write(text);
                    }
                }
            }
        }

        
    }
}
