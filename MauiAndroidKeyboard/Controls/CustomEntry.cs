﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiAndroidKeyboard.Controls
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty ShowVirtualKeyboardOnFocusProperty = BindableProperty.Create("ShowVirtualKeyboardOnFocus", typeof(bool), typeof(ExtendedEntry), true);

        public static readonly BindableProperty TextProperty = BindableProperty.Create(nameof(Text), typeof(string), typeof(CustomEntry), null);

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(CustomEntry), null);

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        public Color TextColor
        {
            get { return (Color)GetValue(TextColorProperty); }
            set { SetValue(TextColorProperty, value); }
        }

        public bool ShowVirtualKeyboardOnFocus
        {
            get => (bool)this.GetValue(ShowVirtualKeyboardOnFocusProperty);
            set => this.SetValue(ShowVirtualKeyboardOnFocusProperty, value);
        }


        #region Events
        public event EventHandler ShowKeyboardRequested;
        public event EventHandler HideKeyboardRequested;
        #endregion


        public CustomEntry()
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
            ShowKeyboardRequested?.Invoke(this, EventArgs.Empty);
            Handler?.Invoke(nameof(CustomEntry.ShowKeyboardRequested));
        }

        public void HideKeyboard()
        {
            HideKeyboardRequested?.Invoke(this, EventArgs.Empty);
            Handler?.Invoke(nameof(CustomEntry.HideKeyboardRequested));
        }
    }
}
