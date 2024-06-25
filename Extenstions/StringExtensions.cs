using System;
using System.Collections.Generic;
using System.Globalization;
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

        public static string ConvertDateFormat(string date, string fromFormat, string toFormat)
        {
            DateTime parsedDate = DateTime.ParseExact(date, fromFormat, CultureInfo.InvariantCulture);
            return parsedDate.ToString(toFormat);
        }
    }
}
