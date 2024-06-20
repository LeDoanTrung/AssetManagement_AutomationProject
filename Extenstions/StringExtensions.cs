using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Extenstions
{
    public static class StringExtensions
    {
        public static string GetAbsolutePath(this string filePath)
        {
            string directoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            string fullPath = Path.Combine(directoryPath, filePath);
            if (File.Exists(fullPath))
            {
                return fullPath;
            }

            return string.Empty;
        }

        public static string GetTextFromJsonFile(this string filePath)
        {
            string path = filePath.GetAbsolutePath();
            return File.ReadAllText(path);
        }
    }
}
