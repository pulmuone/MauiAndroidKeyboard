using MauiAndroidKeyboard.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAndroidKeyboard.Controls
{
    public enum SoftKeyboardViewStatus
    {
        SHOW,
        HIDE,
    }

    public class HandlerEntry : Entry
    {
        public static readonly BindableProperty ShowVirtualKeyboardOnFocusProperty = BindableProperty.Create("ShowVirtualKeyboardOnFocus", typeof(bool), typeof(ExtendedEntry), true);
        
        public bool ShowVirtualKeyboardOnFocus
        {
            get => (bool)this.GetValue(ShowVirtualKeyboardOnFocusProperty);
            set => this.SetValue(ShowVirtualKeyboardOnFocusProperty, value);
        }

        public SoftKeyboardViewStatus SoftKeyboardViewStatus;

        #region Events
        public event EventHandler ShowKeyboardRequested;
        public event EventHandler HideKeyboardRequested;
        #endregion

        public HandlerEntry()
        {
            this.Focused += OnFocused;
            this.Unfocused -= OnFocused;
        }

        private void OnFocused(object sender, FocusEventArgs e)
        {
            //if (e.IsFocused)
            //{
            //    if (ShowVirtualKeyboardOnFocus)
            //    {
            //        ShowKeyboard();
            //    }
            //    else
            //    {
            //        HideKeyboard();
            //    }
            //}
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

        public void ShowKeyboard()
        {
            SoftKeyboardViewStatus = SoftKeyboardViewStatus.SHOW;

            ShowKeyboardRequested?.Invoke(this, EventArgs.Empty);
            Handler?.Invoke(nameof(HandlerEntry.ShowKeyboardRequested));
        }

        public void HideKeyboard()
        {
            SoftKeyboardViewStatus = SoftKeyboardViewStatus.HIDE;

            HideKeyboardRequested?.Invoke(this, EventArgs.Empty);
            Handler?.Invoke(nameof(HandlerEntry.HideKeyboardRequested));
        }
    }
}
