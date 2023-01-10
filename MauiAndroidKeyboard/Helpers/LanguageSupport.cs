using MauiAndroidKeyboard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAndroidKeyboard.Helpers
{
    public class LanguageSupport
    {
        private static readonly LanguageSupport _instance = new LanguageSupport();
        private List<LanguageModel> _languageList = new List<LanguageModel>();
        private List<string> _languageListShort = new List<string>();

        private LanguageSupport()
        {
            _languageList = new List<LanguageModel>()
            {
                new LanguageModel { DisplayName =  "한국어-Korean", ShortName = "ko" },
                new LanguageModel { DisplayName =  "English", ShortName = "en" },
                new LanguageModel { DisplayName =  "中文-Chinese (simplified)", ShortName = "zh-Hans" },
                new LanguageModel { DisplayName =  "中文-Chinese(Traditional)", ShortName = "zh-Hant" },
                new LanguageModel { DisplayName =  "日本語-Japanese", ShortName = "ja" },
                new LanguageModel { DisplayName =  "español-Spanish", ShortName = "es" },
                new LanguageModel { DisplayName =  "TiếngViệt-Vietnam", ShortName = "vi" }
                //new LanguageModel { DisplayName =  "Français-French", ShortName = "fr" },

                //new LanguageModel { DisplayName =  "Русский-Russian", ShortName = "ru" },
                //new LanguageModel { DisplayName =  "Deutsche-German", ShortName = "de" },
                //new LanguageModel { DisplayName =  "हिन्दी-India", ShortName = "hi" },
                //new LanguageModel { DisplayName =  "Português-Portugal", ShortName = "pt" }
            };

            foreach (var item in _languageList)
            {
                _languageListShort.Add(item.ShortName);
            }
        }

        public static LanguageSupport Instance
        {
            get
            {
                return _instance;
            }
        }
        public List<LanguageModel> LanguageList
        {
            get { return this._languageList; }
        }

        public List<string> LanguageListShort
        {
            get { return this._languageListShort; }
        }
    }
}
