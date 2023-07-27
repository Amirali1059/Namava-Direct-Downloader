using System;
using System.Collections.Specialized;
using System.IO;
using System.Json;
using System.Windows.Forms;

namespace AAM_Products
{
    class AAM_Products
    {
        public string product = "";
        IWin32Window Parent;
        readonly public string Server = "https://aamproducts.pythonanywhere.com";
        public string InputLicense()
        {
            EnterLicenseDialog EnterLicenseDialog_ = new EnterLicenseDialog();
            DialogResult result = EnterLicenseDialog_.ShowDialog(Parent);
            if (result == DialogResult.OK)
            {
                return EnterLicenseDialog_.license;
            }
            else
            {
                return null;
            }
        }
        public AAM_Products_API(string Product, Form parent)
        {
            this.product = Product;
            this.Parent = parent;
        }

        public (bool, string, string) Activate(string license)
        {
            var data = new NameValueCollection
            {
                ["product"] = this.product,
                ["license"] = license
            };
            JsonValue ParsedResponse = Utils.Request(this.Parent, $"{this.Server}/api/Activate/", "POST", data);
            string secret = "";
            if (ParsedResponse.ContainsKey("secret"))
            {
                secret = ParsedResponse["secret"];
            }
            return (ParsedResponse["ok"], ParsedResponse["msg"], secret);
        }
        public (bool, string) Authorize(string license, string secret)
        {
            var data = new NameValueCollection
            {
                ["product"] = this.product,
                ["license"] = license,
                ["secret"] = secret
            };
            JsonValue ParsedResponse = Utils.Request(this.Parent, $"{this.Server}/api/Authorize/", "POST", data);
            return (ParsedResponse["ok"], ParsedResponse["msg"]);
        }
        public bool CrashReport(string error)
        {
            var data = new NameValueCollection
            {
                ["text"] = error
            };
            JsonValue ParsedResponse = Utils.Request(this.Parent, $"{this.Server}/api/CrashReport/", "POST", data);
            return (bool)ParsedResponse["ok"];
        }

        public (bool, JsonValue) GetLicenseInfo(string license, string secret)
        {
            var data = new NameValueCollection
            {
                ["product"] = this.product,
                ["license"] = license,
                ["secret"] = secret
            };
            JsonValue ParsedResponse = Utils.Request(this.Parent, $"{this.Server}/api/GetLicenseInfo/", "POST", data);
            return (ParsedResponse["ok"], ParsedResponse["msg"]);
        }

        public (bool, string) ResetLicense(string license, string secret)
        {
            var data = new NameValueCollection
            {
                ["product"] = this.product,
                ["license"] = license,
                ["secret"] = secret
            };
            JsonValue ParsedResponse = Utils.Request(this.Parent, $"{this.Server}/api/ResetLicense/", "POST", data);
            return (ParsedResponse["ok"], ParsedResponse["msg"]);
        }

        public (bool, JsonValue, string) CheckUpdates()
        {
            var data = new NameValueCollection
            {
                ["product"] = this.product
            };
            JsonValue ParsedResponse = Utils.Request(this.Parent, $"{this.Server}/api/CheckUpdates/", "POST", data);
            if ((bool)ParsedResponse["ok"])
            {
                return (true, ParsedResponse["update_info"], (string)ParsedResponse["release-notes"]);
            }
            else
            {
                return (false, ParsedResponse["msg"], null);
            }
        }

        public bool Log(JsonValue Data)
        {
            var data = new NameValueCollection
            {
                ["product"] = this.product,
                ["data"] = Data,
            };
            JsonValue ParsedResponse = Utils.Request(this.Parent, $"{this.Server}/api/LogData/", "POST", data);
            return (bool)ParsedResponse["ok"];
        }
    }
}
