using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAndroidKeyboard
{
    public class GlobalSetting
    {

        public GlobalSetting()
        {
            ApkUri = "https://github.com/pulmuone/MauiAndroidKeyboard/releases/download/v1/com.gwise.maui.apk";
            ApkVerUri = "https://raw.githubusercontent.com/pulmuone/MauiAndroidKeyboard/master/version.txt";
        }

        public static GlobalSetting Instance { get; } = new GlobalSetting();

        public string ApiUri { get; set; }
        //public string ApiKey { get; set; }

        public string ApkUri { get; set; }
        public string ApkVerUri { get; set; }

    }
}
