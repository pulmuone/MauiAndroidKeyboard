using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAndroidKeyboard.Helpers
{
    public static class Settings
    {
        //example : ko, en, ja, es, vi, fr
        public static string Language
        {
            get => Preferences.Get(nameof(Language), String.Empty);
            set => Preferences.Set(nameof(Language), value);
        }
    }
}
