using CommunityToolkit.Maui.Core.Platform;

namespace MauiAndroidKeyboard.Controls
{
    public class KeyboardEntry : Entry
    {
        private CancellationTokenSource _source = new();

        public static readonly BindableProperty ShowVirtualKeyboardOnFocusProperty = BindableProperty.Create("ShowVirtualKeyboardOnFocus", typeof(bool), typeof(ExtendedEntry), true);

        public bool ShowVirtualKeyboardOnFocus
        {
            get => (bool)this.GetValue(ShowVirtualKeyboardOnFocusProperty);
            set => this.SetValue(ShowVirtualKeyboardOnFocusProperty, value);
        }

        public KeyboardEntry()
        {
            this.Focused += OnFocused;
            this.Unfocused -= OnFocused;
        }

        private async void OnFocused(object sender, FocusEventArgs e)
        {
            if (e.IsFocused)
            {
                if (ShowVirtualKeyboardOnFocus)
                {
                    await this.ShowKeyboardAsync(_source.Token);
                }
                else
                {
                    await this.HideKeyboardAsync(_source.Token);
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
        public async void ShowKeyboard()
        {
            await this.ShowKeyboardAsync(_source.Token);
        }

        public async void HideKeyboard()
        {
            await this.HideKeyboardAsync(_source.Token);
        }

    }
}
