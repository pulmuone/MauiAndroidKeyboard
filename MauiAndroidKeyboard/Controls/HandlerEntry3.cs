using CommunityToolkit.Maui.Core.Platform;
using System.Diagnostics;

namespace MauiAndroidKeyboard.Controls
{
    public class HandlerEntry3 : Entry
    {
        CancellationToken _token = new CancellationToken();

        // 포커스 됐을때 true: 키보드 보임, false: 키보드 안보임
        public static readonly BindableProperty ShowVirtualKeyboardOnFocusProperty
            = BindableProperty.Create("ShowVirtualKeyboardOnFocus", typeof(bool), typeof(HandlerEntry2), true);

        public bool ShowVirtualKeyboardOnFocus
        {
            get => (bool)this.GetValue(ShowVirtualKeyboardOnFocusProperty);
            set => this.SetValue(ShowVirtualKeyboardOnFocusProperty, value);
        }

        public HandlerEntry3()
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

        public bool OnIsKeyboardShowing()
        {
            if (this.IsSoftKeyboardShowing())
            {
                Debug.WriteLine($"Soft Input Is Currently Showing");
                //OperationResult = $"Soft Input Is Currently Showing";

                return true;
            }
            else
            {
                Debug.WriteLine($"Soft Input Is Currently Hidden");
                //OperationResult = $"Soft Input Is Currently Hidden";
                return false;
            }
        }

        //public void ToggleKeyboard()
        //{
        //    if (this.IsSoftKeyboardShowing())
        //    {
        //        //OperationResult = $"Soft Input Is Currently Showing";
        //        HideKeyboard();
        //    }
        //    else
        //    {
        //        //OperationResult = $"Soft Input Is Currently Hidden";
        //        ShowKeyboard();
        //    }
        //}

        public async void ShowKeyboard()
        {
            try
            {
                bool isSuccessful = await this.ShowKeyboardAsync(CancellationToken.None);

                if (isSuccessful)
                {
                    Debug.WriteLine("Show Succeeded");
                }
                else
                {
                    Debug.WriteLine("Show Failed");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public async void HideKeyboard()
        {
            try
            {
                bool isSuccessful = await this.HideKeyboardAsync(_token);

                if (isSuccessful)
                {
                    Debug.WriteLine("Hide Succeeded");
                }
                else
                {
                    Debug.WriteLine("Hide Failed");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }
    }
}
