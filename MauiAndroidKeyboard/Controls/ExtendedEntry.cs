using MauiAndroidKeyboard.Interfaces;

namespace MauiAndroidKeyboard.Controls
{
    public class ExtendedEntry : Entry
    {
        public static readonly BindableProperty ShowVirtualKeyboardOnFocusProperty =
            BindableProperty.Create("ShowVirtualKeyboardOnFocus", typeof(bool), typeof(ExtendedEntry), true);

        public bool ShowVirtualKeyboardOnFocus
        {
            get => (bool)this.GetValue(ShowVirtualKeyboardOnFocusProperty);
            set => this.SetValue(ShowVirtualKeyboardOnFocusProperty, value);
        }

        public IVirtualKeyboard VirtualKeyboardHandler { get; set; }

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
            VirtualKeyboardHandler?.ShowKeyboard();
        }

        public void HideKeyboard()
        {
            VirtualKeyboardHandler?.HideKeyboard();
        }

        public void ClearFocus()
        {
            VirtualKeyboardHandler?.EntryClearFocus();
        }
    }
}
