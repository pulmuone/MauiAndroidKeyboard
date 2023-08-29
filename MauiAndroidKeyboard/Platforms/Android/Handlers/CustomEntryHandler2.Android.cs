using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using CommunityToolkit.Maui.Core.Platform;
using MauiAndroidKeyboard.Controls;
using MauiAndroidKeyboard.Interfaces;
using Microsoft.Maui.Handlers;
using static Android.Views.View;
using content = Android.Content;
using view = Android.Views;
using inputTypes = Android.Text.InputTypes;

namespace MauiAndroidKeyboard.Platforms.Android.Handlers
{
    public class CustomEntryHandler2 : EntryHandler, IVirtualKeyboard
    {

        //ViewHandler로 개발하지 않아도 작동됨.
        public static PropertyMapper<HandlerEntry2, CustomEntryHandler2> PropertyMapper = new PropertyMapper<HandlerEntry2, CustomEntryHandler2>(ViewHandler.ViewMapper)
        {
            //[nameof(HandlerEntry2.VirtualKeyboardToggle)] = MapVirtualKeyboardToggle
        };

        //동작하지 않음.ViewHandler로 개발한 경우만 적용됨
        //public static new CommandMapper<HandlerEntry2, CustomEntryHandler2> CommandMapper = new(ViewCommandMapper)
        //{
        //    [nameof(HandlerEntry2.ShowKeyboardRequested)] = MapShowKeyboardRequested,
        //    [nameof(HandlerEntry2.HideKeyboardRequested)] = MapHideKeyboardRequested
        //};

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
            //platformView.SetTextSize(ComplexUnitType.Sp, 14);
            platformView.ShowSoftInputOnFocus = false; //true: Show Keyboard, false: Hide Keyboard
            //platformView.SetSingleLine(true);
            platformView.InputType = inputTypes.ClassText;
            //platformView.SetOnKeyListener(new MyOnKeyListener(VirtualView));

            platformView.EditorAction += PlatformView_EditorAction;

        }

        private void PlatformView_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            var actionId = e.ActionId;
            var evt = e.Event;

            if (actionId == ImeAction.Done || (actionId == ImeAction.ImeNull && evt?.KeyCode == Keycode.Enter && evt?.Action == KeyEventActions.Up))
            {
                VirtualView?.Completed();
            }

            e.Handled = true;
        }

        //핸들러 기본 실행 #3
        protected override void DisconnectHandler(AppCompatEditText platformView)
        {
            platformView.EditorAction -= PlatformView_EditorAction;
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

        //================사용
        public void ShowKeyboard()
        {
            this.PlatformView.RequestFocus();

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            inputMethodManager.ShowSoftInput(this.PlatformView, ShowFlags.Forced);
        }

        //================사용
        public void HideKeyboard()
        {
            this.PlatformView.RequestFocus();

            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus?.WindowToken, HideSoftInputFlags.None);
        }

        public void StatusKeyboard()
        {
            Console.WriteLine(this.PlatformView.ShowSoftInputOnFocus);

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
                if (e.Action == KeyEventActions.Down && keyCode == Keycode.Enter)
                {
                    (vv as HandlerEntry2).SendCompleted();

                    return true;
                }

                return false;
            }
        }
    }
}
