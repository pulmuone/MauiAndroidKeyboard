using MauiAndroidKeyboard.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAndroidKeyboard.Controls
{
    public class HandlerEntry2 : Entry
    {
        public static readonly BindableProperty ShowVirtualKeyboardOnFocusProperty
            = BindableProperty.Create("ShowVirtualKeyboardOnFocus", typeof(bool), typeof(HandlerEntry2), true);

        public static readonly BindableProperty VirtualKeyboardToggleProperty
            = BindableProperty.Create("VirtualKeyboardToggle", typeof(bool), typeof(HandlerEntry2), true);

        public bool VirtualKeyboardToggle
        {
            get => (bool)this.GetValue(VirtualKeyboardToggleProperty);
            set => this.SetValue(VirtualKeyboardToggleProperty, value);
        }

        public bool ShowVirtualKeyboardOnFocus
        {
            get => (bool)this.GetValue(ShowVirtualKeyboardOnFocusProperty);
            set => this.SetValue(ShowVirtualKeyboardOnFocusProperty, value);
        }

        public SoftKeyboardViewStatus SoftKeyboardViewStatus;

        public IVirtualKeyboard VirtualKeyboardHandler { get; set; }

        #region Events
        public event EventHandler ShowKeyboardRequested;
        public event EventHandler HideKeyboardRequested;
        #endregion


        public HandlerEntry2()
        {
            this.Focused += OnFocused;
            this.Unfocused -= OnFocused;
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

            //VirtualKeyboardToggle = VirtualKeyboardToggle ? false: true;

            VirtualKeyboardHandler?.ShowKeyboard();

            //ShowKeyboardRequested?.Invoke(this, EventArgs.Empty);
            //Handler?.Invoke(nameof(HandlerEntry2.ShowKeyboardRequested));
        }

        public void HideKeyboard()
        {
            SoftKeyboardViewStatus = SoftKeyboardViewStatus.HIDE;

            //VirtualKeyboardToggle = VirtualKeyboardToggle ? false : true;

            VirtualKeyboardHandler?.HideKeyboard();

            //HideKeyboardRequested?.Invoke(this, EventArgs.Empty);
            //Handler?.Invoke(nameof(HandlerEntry2.HideKeyboardRequested));
        }
    }
}
