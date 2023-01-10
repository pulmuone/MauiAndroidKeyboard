using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAndroidKeyboard.Resources.Localization
{
    [ContentProperty("Text")]
    public class TranslateExtension : IMarkupExtension<BindingBase>, IMarkupExtension
    {
        public string Text { get; set; } = string.Empty;


        public string? StringFormat { get; set; }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return ProvideValue(serviceProvider);
        }

        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            if (DateTime.Now.Ticks < 0)
            {
                _ = LocalizationResourceManager.Current[Text];
            }

            return new Binding
            {
                Mode = BindingMode.OneWay,
                Path = "[" + Text + "]",
                Source = LocalizationResourceManager.Current,
                StringFormat = StringFormat
            };
        }
    }
}
