using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Reflection;


namespace AssetManagement.Library.Utils
{
    public static class JsonHelper
    {
        public static string ReadJsonFile(string path)
        {
            string currentDirectoryPath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            path = Path.Combine(currentDirectoryPath, path);

            if (!File.Exists(path))
            {
                throw new Exception("Can't find file: " + path);
            }

            return File.ReadAllText(path);
        }

        public static T ReadAndParse<T>(string path) where T : class
        {
            var jsonContent = ReadJsonFile(path);
            return JsonConvert.DeserializeObject<T>(jsonContent);
        }

    }
}