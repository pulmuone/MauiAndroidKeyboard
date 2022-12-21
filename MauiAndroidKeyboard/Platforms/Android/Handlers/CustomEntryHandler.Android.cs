using Android.Runtime;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using MauiAndroidKeyboard.Controls;
using Microsoft.Maui.Handlers;
using static Android.Views.View;
using content = Android.Content;
using view = Android.Views;

namespace MauiAndroidKeyboard.Platforms.Android.Handlers
{
    public partial class CustomEntryHandler : ViewHandler<HandlerEntry, EditText>
    {
        public static PropertyMapper<HandlerEntry, CustomEntryHandler> PropertyMapper = new PropertyMapper<HandlerEntry, CustomEntryHandler>(ViewHandler.ViewMapper)
        {
            //[nameof(ICustomEntry.ShowVirtualKeyboardOnFocus)] = MapShowVirtualKeyboardOnFocus
        };

        public static CommandMapper<HandlerEntry, CustomEntryHandler> CommandMapper = new(ViewCommandMapper)
        {
            [nameof(HandlerEntry.ShowKeyboardRequested)] = MapShowKeyboardRequested,
            [nameof(HandlerEntry.HideKeyboardRequested)] = MapHideKeyboardRequested
        };

        public CustomEntryHandler() : base(PropertyMapper, CommandMapper)
        {
        }

        protected override EditText CreatePlatformView()
        {
            return new EditText(Context);
        }

        protected override void ConnectHandler(EditText platformView)
        {
            base.ConnectHandler(platformView);

            //VirtualView : Cross-platform Control 접근

            //Perform any control setup here
            platformView.SetPadding(0, 0, 0, 0);
            //platformView.SetHeight(40);
            //platformView.SetMaxHeight(40);
            platformView.SetSingleLine(true);
            if(VirtualView.Keyboard == Keyboard.Numeric)
            {
                platformView.SetRawInputType(InputTypes.ClassNumber);
            }
            else if (VirtualView.Keyboard == Keyboard.Text)
            {
                platformView.SetRawInputType(InputTypes.ClassText);
            }

            platformView.SetTextIsSelectable(true);
            platformView.SetSelectAllOnFocus(true);
            platformView.ShowSoftInputOnFocus = false; //true: Show Keyboard, false: Hide Keyboard

            platformView.SetOnKeyListener(new MyOnKeyListener(VirtualView));
        }

        protected override void DisconnectHandler(EditText platformView)
        {
            platformView.Dispose();

            base.DisconnectHandler(platformView);
        }

        //static void MapShowVirtualKeyboardOnFocus(CustomEntryHandler handler, ICustomEntry entry)
        //{
        //    //handler.PlatformView.UpdateTextColor(entry.TextColor);
        //}

        public static void MapShowKeyboardRequested(CustomEntryHandler handler, HandlerEntry entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            inputMethodManager.ShowSoftInput(handler.PlatformView, ShowFlags.Forced);
        }

        public static void MapHideKeyboardRequested(CustomEntryHandler handler, HandlerEntry entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus?.WindowToken, HideSoftInputFlags.None);
        }

        class MyOnKeyListener : Java.Lang.Object, IOnKeyListener
        {
            IView vv;
            public MyOnKeyListener(IView view)
            {
                vv = view;
            }

            ///handler.PlatformView.RequestFocus(); 하면 한번 더 진입한다.
            public bool OnKey(view.View v, [GeneratedEnum] Keycode keyCode, KeyEvent e)
            {
                if(e.Action == KeyEventActions.Down && keyCode== Keycode.Enter) 
                {
                    (vv as HandlerEntry).SendCompleted();

                    return true;
                }

                return false;
            }
        }
    }
}
