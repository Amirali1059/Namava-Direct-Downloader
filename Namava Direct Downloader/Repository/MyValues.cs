using Microsoft.Win32;

namespace Namava_Direct_Downloader
{
    public class MyValues : IMyValues
    {
        private RegistryKey _MyRegistryKey;
        public MyValues(RegistryKey myRegistryKey)
        {
            this._MyRegistryKey = myRegistryKey;
        }
        public bool Exists(string valueName)
        {
            bool ValueExists = false;
            string Value = (string)_MyRegistryKey.GetValue(valueName);
            if (Value != null && Value != "" && Value.Length != 0)
            {
                ValueExists = true;
            }
            return ValueExists;
        }
        public string GetValue(string valueName)
        {
            string Value = (string)_MyRegistryKey.GetValue(valueName);
            if (!Exists(valueName))
            {
                return "";
            }
            return Value;
        }
        public void SetValue(string valueName, string value)
        {
            _MyRegistryKey.SetValue(valueName, value);
        }
    }
}
