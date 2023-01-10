using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace MauiAndroidKeyboard.Resources.Localization
{
    public class LocalizationResourceManager : ObservableObject
    {
        private static readonly Lazy<LocalizationResourceManager> currentHolder = new Lazy<LocalizationResourceManager>(() => new LocalizationResourceManager());

        private ResourceManager? resourceManager;

        private CultureInfo currentCulture = Thread.CurrentThread.CurrentUICulture;

        public static LocalizationResourceManager Current => currentHolder.Value;

        public string this[string text] => GetValue(text);

        public CultureInfo CurrentCulture
        {
            get
            {
                return currentCulture;
            }
            set
            {
                SetProperty(ref currentCulture, value, null);
            }
        }

        private LocalizationResourceManager()
        {
        }

        public void Init(ResourceManager resource)
        {
            resourceManager = resource;
        }

        public void Init(ResourceManager resource, CultureInfo initialCulture)
        {
            CurrentCulture = initialCulture;
            Init(resource);
        }

        public string GetValue(string text)
        {
            if (resourceManager == null)
            {
                throw new InvalidOperationException("Must call LocalizationResourceManager.Init first");
            }

            return resourceManager!.GetString(text, CurrentCulture) ?? throw new NullReferenceException("text: " + text + " not found");
        }

        [Obsolete("Please, use CurrentCulture to set culture")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetCulture(CultureInfo language)
        {
            CurrentCulture = language;
        }

        //[Obsolete("This method is no longer needed with new implementation of LocalizationResourceManager. Please, remove all references to it.")]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        //public void Invalidate()
        //{
        //    //OnPropertyChanged(null);
        //}
    }
}
