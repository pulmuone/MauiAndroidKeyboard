using Android.Widget;
using MauiAndroidKeyboard.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using view = Android.Views;
using content = Android.Content;
using android = Microsoft.Maui.Controls.Compatibility.Platform.Android;
using Android.Views.InputMethods;
using Xamarin.Google.Crypto.Tink.Signature;

namespace MauiAndroidKeyboard.Platforms.Android
{
    public partial class CustomEntryHandler : ViewHandler<CustomEntry, EditText>
    {
        public static PropertyMapper<CustomEntry, CustomEntryHandler> CustomEntryMapper = new PropertyMapper<CustomEntry, CustomEntryHandler>(ViewHandler.ViewMapper)
        {
            [nameof(CustomEntry.Text)] = MapText,
            [nameof(CustomEntry.TextColor)] = MapTextColor,
        };

        public CustomEntryHandler() : base(CustomEntryMapper)
        {
        }

        protected override void ConnectHandler(EditText platformView)
        {
            base.ConnectHandler(platformView);

            // Perform any control setup here
        }

        protected override EditText CreatePlatformView()
        {
            return new EditText(Context);
        }

        static void MapText(CustomEntryHandler handler, CustomEntry entry)
        {
            handler.PlatformView.Text = entry.Text;
            //handler.PlatformView?.Text = entry.Text;
        }

        static void MapTextColor(CustomEntryHandler handler, CustomEntry entry)
        {
            handler.PlatformView.UpdateTextColor(entry.TextColor);
            //handler.PlatformView?.TextColor = entry.TextColor;
        }

        public void ShowKeyboard()
        {
            try
            {
                this.PlatformView.RequestFocus();
                var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
                var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
                inputMethodManager.ShowSoftInput(this.PlatformView, ShowFlags.Forced);
            }
            catch (Exception ex)
            {

            }
        }

        public void HideKeyboard()
        {
            try
            {
                this.PlatformView.RequestFocus();
                var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
                var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
                inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus?.WindowToken, 0);
            }
            catch (Exception ex)
            {

            }
        }
    }
}
