using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AssetManagement.Library
{
    public static class JsonHelper
    {
        public static IEnumerable<object[]> GetTestData(string jsonFile, string propertyName)
        {
            var path = Path.IsPathRooted(jsonFile)
				? jsonFile
				: Path.GetRelativePath(Directory.GetCurrentDirectory(), jsonFile);

			if (!File.Exists(path))
			{
				throw new ArgumentException($"Could not find file at path: {path}");
			}

			var fileData = File.ReadAllText(jsonFile);

			if (string.IsNullOrEmpty(propertyName))
			{
				return JsonConvert.DeserializeObject<List<object[]>>(fileData);
			}

			var allData = JObject.Parse(fileData);
			var data = allData[propertyName];
			return data.ToObject<List<object[]>>();
        }
    }
}