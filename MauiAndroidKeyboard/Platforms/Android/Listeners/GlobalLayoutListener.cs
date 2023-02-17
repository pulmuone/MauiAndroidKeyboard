using Android.App;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using View = Android.Views.View;
using resource = Android.Resource;
using MauiAndroidKeyboard.Controls;
using MauiAndroidKeyboard.Platforms.Android.Services;

namespace MauiAndroidKeyboard.Platforms.Android.Listeners
{
    internal class GlobalLayoutListener : Java.Lang.Object, ViewTreeObserver.IOnGlobalLayoutListener
    {
        private static InputMethodManager inputManager;
        private static Activity activity;
        private readonly SoftwareKeyboardService softwarekeyboardservice;
        private static View childOfContent;
        private static float displayDensity;
        private static int displayheight;
        private static int extrascreenheight;
        private static int keyboardheight;

        private static void ObtainInputManager()
        {
            inputManager = (InputMethodManager)activity.GetSystemService(Context.InputMethodService);
        }

        public GlobalLayoutListener(SoftwareKeyboardService softwarekeyboardservice)
        {            
            this.softwarekeyboardservice = softwarekeyboardservice;
            GlobalLayoutListener.activity = Platform.CurrentActivity;
            ObtainInputManager();
            GetDisplayData();
        }

        private void GetDisplayData()
        {
            var content = (FrameLayout)activity.FindViewById(resource.Id.Content);
            childOfContent = content.GetChildAt(0);

            var metrics = new DisplayMetrics();
            activity.WindowManager.DefaultDisplay.GetMetrics(metrics);
            displayDensity = metrics.Density;

            CalculateDisplayHeight();
        }

        public void OnGlobalLayout()
        {
            if (inputManager.Handle == IntPtr.Zero)
            {
                ObtainInputManager();
            }

            if (displayheight != childOfContent.RootView.Height)
            {
                CalculateDisplayHeight();
            }

            var keyboardheight = CalculateKeyboardHeight();

            if (keyboardheight != GlobalLayoutListener.keyboardheight)
            {
                GlobalLayoutListener.keyboardheight = keyboardheight;
                this.softwarekeyboardservice.InvokeKeyboardHeightChanged(new SoftwareKeyboardEventArgs(ConvertPixelsToDp((float)keyboardheight)));
            }
        }

        public bool IsKeyboardVisible => keyboardheight != 0;

        private static void CalculateDisplayHeight()
        {
            var r = new Rect();
            childOfContent.GetWindowVisibleDisplayFrame(r);
            var visibleheight = r.Height;
            displayheight = childOfContent.RootView.Height;
            extrascreenheight = displayheight - Convert.ToInt32(visibleheight);
        }

        private static int CalculateKeyboardHeight()
        {
            var r = new Rect();
            childOfContent.GetWindowVisibleDisplayFrame(r);

            var visibleheight = r.Height;
            return Math.Max(displayheight - Convert.ToInt32(visibleheight) - extrascreenheight, 0);
        }

        private static int ConvertPixelsToDp(float pixelValue)
        {
            return (int)((pixelValue) / displayDensity);
        }
    }
}
