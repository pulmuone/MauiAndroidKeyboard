using MauiAndroidKeyboard.Controls;
using MauiAndroidKeyboard.Interfaces;
using MauiAndroidKeyboard.Platforms.Android.Listeners;
using app = Android.App;

namespace MauiAndroidKeyboard.Platforms.Android.Services
{
    public class SoftwareKeyboardService : ISoftwareKeyboardService
    {
        public virtual event EventHandler<SoftwareKeyboardEventArgs> KeyboardHeightChanged;

        private readonly app.Activity activity;
        private readonly GlobalLayoutListener globalLayoutListener;

        public bool IsKeyboardVisible => globalLayoutListener.IsKeyboardVisible;
        
        public SoftwareKeyboardService()
        {
            this.activity = Platform.CurrentActivity;
            globalLayoutListener = new GlobalLayoutListener(this);
            this.activity.Window.DecorView.ViewTreeObserver.AddOnGlobalLayoutListener(this.globalLayoutListener);
        }

        internal void InvokeKeyboardHeightChanged(SoftwareKeyboardEventArgs args)
        {
            var handler = KeyboardHeightChanged;
            handler?.Invoke(this, args);
        }
    }
}
