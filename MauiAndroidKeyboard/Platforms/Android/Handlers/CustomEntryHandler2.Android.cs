using Android.Content;
using Android.Util;
using Android.Views.InputMethods;
using AndroidX.AppCompat.Widget;
using MauiAndroidKeyboard.Controls;
using MauiAndroidKeyboard.Interfaces;
using Microsoft.Maui;
using Microsoft.Maui.Handlers;
using System.Runtime.CompilerServices;
using content = Android.Content;
using view = Android.Views;

namespace MauiAndroidKeyboard.Platforms.Android.Handlers
{
    public class CustomEntryHandler2 : EntryHandler, IVirtualKeyboard
    {
        public static PropertyMapper<HandlerEntry2, CustomEntryHandler2> PropertyMapper = new PropertyMapper<HandlerEntry2, CustomEntryHandler2>(ViewHandler.ViewMapper)
        {
            [nameof(HandlerEntry2.VirtualKeyboardToggle)] = MapVirtualKeyboardToggle
        };

        //동작하지 않음.
        public static new CommandMapper<HandlerEntry2, CustomEntryHandler2> CommandMapper = new(ViewCommandMapper)
        {
            [nameof(HandlerEntry2.ShowKeyboardRequested)] = MapShowKeyboardRequested,
            [nameof(HandlerEntry2.HideKeyboardRequested)] = MapHideKeyboardRequested
        };

        public CustomEntryHandler2() : base(PropertyMapper)
        {
            
        }

        //핸들러 기본 실행 #1
        protected override AppCompatEditText CreatePlatformView()
        {
            return new AppCompatEditText(base.Context);
        }

        public override void SetVirtualView(IView view)
        {
            base.SetVirtualView(view);

            var entry = (HandlerEntry2)view;

            entry.VirtualKeyboardHandler = this;
        }

        //핸들러 기본 실행 #2
        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            base.ConnectHandler(PlatformView);

            platformView.SetPadding(0, 0, 0, 0);
            platformView.SetTextIsSelectable(true);
            platformView.SetSelectAllOnFocus(true);
            platformView.SetTextSize(ComplexUnitType.Sp, 14);
            platformView.ShowSoftInputOnFocus = false; //true: Show Keyboard, false: Hide Keyboard
        }

        //핸들러 기본 실행 #3
        protected override void DisconnectHandler(AppCompatEditText platformView)
        {
            platformView.Dispose();

            base.DisconnectHandler(platformView);
        }

        static void MapVirtualKeyboardToggle(CustomEntryHandler2 handler, HandlerEntry2 entry)
        {
            if (entry.SoftKeyboardViewStatus == SoftKeyboardViewStatus.SHOW)
            {
                MapShowVirtualKeyboard(handler, entry);
            }
            else
            {
                MapHideVirtualKeyboard(handler, entry);
            }
        }

        static void MapShowVirtualKeyboard(CustomEntryHandler2 handler, HandlerEntry2 entry)
        {
            handler.PlatformView.RequestFocus();

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            inputMethodManager.ShowSoftInput(handler.PlatformView, ShowFlags.Forced);
        }


        static void MapHideVirtualKeyboard(CustomEntryHandler2 handler, HandlerEntry2 entry)
        {
            handler.PlatformView.RequestFocus();

            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus?.WindowToken, HideSoftInputFlags.None);
        }

        static void MapShowKeyboardRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? obj)
        {
            handler.PlatformView.RequestFocus();

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            inputMethodManager.ShowSoftInput(handler.PlatformView, ShowFlags.Forced);
        }

        static void MapHideKeyboardRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? obj)
        {
            handler.PlatformView.RequestFocus();

            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus?.WindowToken, HideSoftInputFlags.None);
        }

        public void ShowKeyboard()
        {
            this.PlatformView.RequestFocus();

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            inputMethodManager.ShowSoftInput(this.PlatformView, ShowFlags.Forced);
        }

        public void HideKeyboard()
        {
            this.PlatformView.RequestFocus();

            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus?.WindowToken, HideSoftInputFlags.None);
        }
    }
}
