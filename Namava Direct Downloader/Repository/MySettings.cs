
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Namava_Direct_Downloader
{
    public class MySettings
    {
        MyValues myValues;
        public Dictionary<string, bool> boolSettings = new Dictionary<string, bool>();
        public Dictionary<string, string> strSettings = new Dictionary<string, string>();
        private readonly (string, bool)[] boolvals = {
                ("NumsWithEnglish",true),
                ("RemoveNamavaIntro",false),
                ("AutoUpdate",false),
                ("DeleteAudios",false),
                ("DeleteSubtitles",false),
                ("DeleteVideo",false),
                ("DeleteVideoFolder",false),
                ("AllwaysMix",true),
            };
        private readonly (string, string)[] strvals = {
                ("SavePath", $"{Utils.getUserProfile()}\\Downloads\\NDD\\"),
                ("SaveFormat", "$N-S$SE$E-($Qp)"),
                ("Language", "en"),
            };
        public MySettings(MyValues myValues)
        {
            this.myValues = myValues;
        }

        // load the settings from registary
        public void LoadSettings()
        {
           
            // Load boolean typed settins
            foreach ((string valname, bool defult) in boolvals)
            {
                if (myValues.Exists(valname))
                {
                    boolSettings.Add(valname, myValues.GetValue(valname) == "true");
                }
                else
                {
                    myValues.SetValue(valname, defult ? "true" : "false");
                    boolSettings.Add(valname, defult);
                }
            }

            // Load string typed settins
            foreach ((string valname, string defult) in strvals)
            {
                if (myValues.Exists(valname))
                {
                    strSettings.Add(valname, myValues.GetValue(valname));
                }
                else
                {
                    myValues.SetValue(valname, defult);
                    strSettings.Add(valname, defult);
                }
            }
        }

        // save the settings to registary
        public void SaveSettings()
        {
            /* This function  */

            // Save boolean typed settins
            foreach ((string valname, bool defult) in boolvals)
            {
                if (boolSettings.ContainsKey(valname))
                {
                    myValues.SetValue(valname, boolSettings[valname] ? "true" : "false");
                }
                else
                {
                    myValues.SetValue(valname, defult ? "true" : "false");
                }
            }

            // Save string typed settins
            foreach ((string valname, string defult) in strvals)
            {
                if (strSettings.ContainsKey(valname))
                {
                    myValues.SetValue(valname, strSettings[valname]);
                }
                else
                {
                    myValues.SetValue(valname, defult);
                }
            }
        }
    }
}
