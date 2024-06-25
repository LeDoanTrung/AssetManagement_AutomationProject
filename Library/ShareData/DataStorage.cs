using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagement.Library.ShareData
{
    public class DataStorage
    {
        private static AsyncLocal<Dictionary<string, object>> _data = new AsyncLocal<Dictionary<string, object>>();

        public static void InitData()
        {
            _data.Value = new Dictionary<string, object>();
        }

        public static void SetData(string key, object value)
        {
            _data.Value[key] = value;
        }

        public static object GetData(string key)
        {
            if (_data.Value.ContainsKey(key) is false)
            {
                return null;
            }
            return _data.Value.GetValueOrDefault(key);
        }

        public static void ClearData()
        {
            if(_data.Value is not null)
            {
                _data.Value.Clear();
            }
        }
    }
}
