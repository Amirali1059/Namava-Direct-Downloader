using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namava_Direct_Downloader
{
    interface IMyValues
    {
        string GetValue(string valueName);
        bool Exists(string valueName);
        void SetValue(string valueName, string value);
    }
}
