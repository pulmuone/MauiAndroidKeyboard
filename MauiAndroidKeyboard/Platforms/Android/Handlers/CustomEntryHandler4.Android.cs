using Android.Text;
using Android.Views.InputMethods;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using MauiAndroidKeyboard.Controls;
using MauiAndroidKeyboard.Interfaces;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using inputTypes = Android.Text.InputTypes;
using content = Android.Content;
using view = Android.Views;
using AndroidX.Core.Content;
using Android.Graphics.Drawables;

namespace MauiAndroidKeyboard.Platforms.Android.Handlers
{
    public class CustomEntryHandler4 : ViewHandler<HandlerEntry4, AppCompatEditText>
    {
        public static PropertyMapper<HandlerEntry4, CustomEntryHandler4> PropertyMapper = new PropertyMapper<HandlerEntry4, CustomEntryHandler4>(ViewHandler.ViewMapper)
        {
            //[nameof(HandlerEntry4.Text)] = MapText,
            //[nameof(HandlerEntry4.TextColor)] = MapTextColor
        };

        public static CommandMapper<HandlerEntry4, CustomEntryHandler4> CommandMapper = new CommandMapper<HandlerEntry4, CustomEntryHandler4>(ViewHandler.ViewCommandMapper)
        {
            [nameof(HandlerEntry4.ShowKeyboardRequested)] = MapShowKeyboardRequested,
            [nameof(HandlerEntry4.HideKeyboardRequested)] = MapHideKeyboardRequested
        };

        protected override AppCompatEditText CreatePlatformView() => new AppCompatEditText(Context);

        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);

            // Perform any control setup here
            //platformView.SetPadding(0, 0, 0, 0); //모양 이상해짐.
            platformView.SetTextIsSelectable(true);
            platformView.SetSelectAllOnFocus(true);
            //platformView.SetTextSize(ComplexUnitType.Sp, 14);
            platformView.ShowSoftInputOnFocus = false; //true: Show Keyboard, false: Hide Keyboard
            platformView.SetSingleLine(true);
            if (VirtualView.Keyboard == Keyboard.Numeric)
            {
                platformView.SetRawInputType(InputTypes.ClassNumber);
            }
            else if (VirtualView.Keyboard == Keyboard.Text)
            {
                platformView.SetRawInputType(InputTypes.ClassText);
            }

            if (VirtualView.IsReadOnly == true)
            {
                platformView.Enabled = false;
            }
            else
            {
                platformView.Enabled = true;
            }

            //platformView.SetOnKeyListener(new MyOnKeyListener(VirtualView));
            platformView.EditorAction += PlatformView_EditorAction;
        }

        private void PlatformView_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            var actionId = e.ActionId;
            var evt = e.Event;

            if (actionId == ImeAction.Done || (actionId == ImeAction.ImeNull && evt?.KeyCode == Keycode.Enter && evt?.Action == KeyEventActions.Up))
            {
                //(VirtualView as Entry).SendCompleted();
                (VirtualView as IEntry).Completed();
            }

            e.Handled = true;
        }


        protected override void DisconnectHandler(AppCompatEditText platformView)
        {
            // Perform any native view cleanup here
            platformView.EditorAction -= PlatformView_EditorAction;

            platformView.Dispose();
            base.DisconnectHandler(platformView);
        }

        //private static void MapText(CustomEntryHandler4 handler, HandlerEntry4 entry)
        //{
        //    handler.PlatformView.Text = entry.Text;
        //    handler.PlatformView?.SetSelection(handler.PlatformView?.Text?.Length ?? 0);
        //}

        //private static void MapTextColor(CustomEntryHandler4 handler, HandlerEntry4 entry)
        //{
        //    handler.PlatformView?.SetTextColor(entry.TextColor.ToPlatform());
        //}


        public static void MapShowKeyboardRequested(CustomEntryHandler4 handler, HandlerEntry4 entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            inputMethodManager.ShowSoftInput(handler.PlatformView, ShowFlags.Forced);
        }

        public static void MapHideKeyboardRequested(CustomEntryHandler4 handler, HandlerEntry4 entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus?.WindowToken, HideSoftInputFlags.None);
        }


        //자동으로 생성되는 생성자이나 파라미터 없는 생성자 필요
        //public CustomEntryHandler4(IPropertyMapper mapper, CommandMapper commandMapper = null) : base(mapper, commandMapper)
        //{

        //}

        //public CustomEntryHandler4(IPropertyMapper mapper) : base(mapper)
        //{

        //}

        //이거 없으면 CommandMapper 작동 안됨.
        public CustomEntryHandler4() : base(PropertyMapper, CommandMapper)
        {

        }
    }
}
