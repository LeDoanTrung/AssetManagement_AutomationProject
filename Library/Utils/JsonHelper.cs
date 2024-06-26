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


        public static IEnumerable<TestCaseData> GetDataFromJSONFiles<T1, T2>(params string[] filePaths)
        {
            List<T1> list1 = new List<T1>();
            List<T2> list2 = new List<T2>();

            foreach (var filePath in filePaths)
            {
                string jsonContent = File.ReadAllText(filePath);

                if (filePath.Contains("item1"))
                {
                    List<T1> items = JsonConvert.DeserializeObject<List<T1>>(jsonContent);
                    list1.AddRange(items);
                }
                else if (filePath.Contains("item2"))
                {
                    List<T2> items = JsonConvert.DeserializeObject<List<T2>>(jsonContent);
                    list2.AddRange(items);
                }
            }

            foreach (var item1 in list1)
            {
                foreach (var item2 in list2)
                {
                    yield return new TestCaseData(item1, item2);
                }
            }
        }

    }
}