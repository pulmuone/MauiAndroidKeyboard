using MauiAndroidKeyboard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAndroidKeyboard.Controls
{
    public class ExtendedEntry : Entry
    {

        /// <summary>
        /// The ShowVirtualKeyboardOnFocus property
        /// </summary>
        public static readonly BindableProperty ShowVirtualKeyboardOnFocusProperty =
            BindableProperty.Create("ShowVirtualKeyboardOnFocus", typeof(bool), typeof(ExtendedEntry), true);

        public IVirtualKeyboard VirtualKeyboardHandler { get; set; }

        public bool ShowVirtualKeyboardOnFocus
        {
            get => (bool)this.GetValue(ShowVirtualKeyboardOnFocusProperty);
            set => this.SetValue(ShowVirtualKeyboardOnFocusProperty, value);
        }

        public SoftKeyboardViewStatus SoftKeyboardViewStatus;

        public ExtendedEntry()
        {
            this.Focused += OnFocused;
            this.Unfocused -= OnFocused;
        }

        public new bool Focus()
        {
            if (ShowVirtualKeyboardOnFocus)
            {
                ShowKeyboard();
            }
            else
            {
                HideKeyboard();
            }

            return true;
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                if (ShowVirtualKeyboardOnFocus)
                {
                    ShowKeyboard();
                }
                else
                {
                    HideKeyboard();
                }
            }
        }

        public void ShowKeyboard()
        {
            SoftKeyboardViewStatus = SoftKeyboardViewStatus.SHOW;
#if ANDROID
            VirtualKeyboardHandler?.ShowKeyboard();
#endif
        }

        public void HideKeyboard()
        {
            SoftKeyboardViewStatus = SoftKeyboardViewStatus.HIDE;
#if ANDROID
            VirtualKeyboardHandler?.HideKeyboard();
#endif
        }
    }
}
