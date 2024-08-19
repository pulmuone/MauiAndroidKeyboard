using Android.Graphics.Drawables;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.Content;
using MauiAndroidKeyboard.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using content = Android.Content;
using view = Android.Views;

namespace MauiAndroidKeyboard.Platforms.Android.Handlers
{
    public class CustomEntryHandler4 : ViewHandler<HandlerEntry4, AppCompatEditText>
    {
        Drawable? _clearButtonDrawable;

        public static PropertyMapper<HandlerEntry4, CustomEntryHandler4> PropertyMapper = new PropertyMapper<HandlerEntry4, CustomEntryHandler4>(ViewHandler.ViewMapper)
        {
            [nameof(HandlerEntry4.Text)] = MapText
            //[nameof(HandlerEntry4.TextColor)] = MapTextColor
        };

        public static CommandMapper<HandlerEntry4, CustomEntryHandler4> CommandMapper = new CommandMapper<HandlerEntry4, CustomEntryHandler4>(ViewHandler.ViewCommandMapper)
        {
            [nameof(HandlerEntry4.ShowKeyboardRequested)] = MapShowKeyboardRequested,
            [nameof(HandlerEntry4.HideKeyboardRequested)] = MapHideKeyboardRequested,
            [nameof(HandlerEntry4.ClearFocusRequested)] = MapClearFocusRequested
        };

        //기본 생성자, Android에서는 필요 없으나 iOS에서는 이 생성자 없으면 에러 발생.
        public CustomEntryHandler4(IPropertyMapper mapper, CommandMapper commandMapper = null) : base(mapper, commandMapper)
        {
        }

        //이거 없으면 CommandMapper 작동 안됨.
        public CustomEntryHandler4() : base(PropertyMapper, CommandMapper)
        {
        }

        protected override AppCompatEditText CreatePlatformView() => new AppCompatEditText(Context);

        //public override void SetVirtualView(IView view)
        //{
        //    base.SetVirtualView(view);
        //    var entry = (HandlerEntry4)view;
        //}

        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);

            // Perform any control setup here
            //platformView.SetPadding(0, 0, 0, 0); //모양 이상해짐.
            //platformView.SetTextIsSelectable(true);
            //platformView.SetSingleLine(true);

            platformView.SetSelectAllOnFocus(true);
            platformView.ShowSoftInputOnFocus = false; //true: Show Keyboard, false: Hide Keyboard

            if (VirtualView.Keyboard == Keyboard.Numeric)
            {
                platformView.SetRawInputType(InputTypes.ClassNumber);
            }
            else if (VirtualView.Keyboard == Keyboard.Text)
            {
                platformView.SetRawInputType(InputTypes.ClassText);
            }

            platformView.EditorAction += PlatformView_EditorAction;

            //platformView.UpdateClearButtonVisibility(this.VirtualView, GetClearButtonDrawable());
        }

        private void OnFocusedChange(object sender, view.View.FocusChangeEventArgs e)
        {
            if (VirtualView == null)
            {
                return;
            }
        }

        protected virtual Drawable? GetClearButtonDrawable() =>
             _clearButtonDrawable ??= ContextCompat.GetDrawable(Context, Resource.Drawable.abc_ic_clear_material);

        private void PlatformView_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            var returnType = VirtualView?.ReturnType;

            if (returnType != null)
            {
                var actionId = e.ActionId;
                var evt = e.Event;

                if (actionId == ImeAction.Done || (actionId == ImeAction.ImeNull && evt?.KeyCode == Keycode.Enter && evt?.Action == KeyEventActions.Up))
                {
                    (VirtualView as Entry).SendCompleted();
                    //return; //Already handled by base class.
                }

                if (actionId != ImeAction.ImeNull)
                {
                    (VirtualView as Entry).SendCompleted();
                }
            }

            e.Handled = true; //이벤트 취소

            //var actionId = e.ActionId;
            //var evt = e.Event;

            //if (actionId == ImeAction.Done || (actionId == ImeAction.ImeNull && evt?.KeyCode == Keycode.Enter && evt?.Action == KeyEventActions.Up))
            //    return; //Already handled by base class.

            //if (actionId != ImeAction.ImeNull)
            //    (VirtualView as Entry).SendCompleted();

            //e.Handled = true;

            //if (actionId == ImeAction.Done || (actionId == ImeAction.ImeNull && evt?.KeyCode == Keycode.Enter && evt?.Action == KeyEventActions.Up))
            //{
            //    (VirtualView as Entry).SendCompleted();
            //}

            //e.Handled = true;
        }


        protected override void DisconnectHandler(AppCompatEditText platformView)
        {
            base.DisconnectHandler(platformView);

            // Perform any native view cleanup here
            platformView.EditorAction -= PlatformView_EditorAction;

            platformView.Dispose();
        }

        private static void MapText(CustomEntryHandler4 handler, HandlerEntry4 entry)
        {
            handler.PlatformView.Text = entry.Text;
            handler.PlatformView?.SetSelection(handler.PlatformView?.Text?.Length ?? 0);
        }

        //private static void MapTextColor(CustomEntryHandler4 handler, HandlerEntry4 entry)
        //{
        //    handler.PlatformView?.SetTextColor(entry.TextColor.ToPlatform());
        //}

        public static void MapShowKeyboardRequested(CustomEntryHandler4 handler, HandlerEntry4 entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            var inputMethodManager = MauiApplication.Current.GetSystemService(content.Context.InputMethodService) as view.InputMethods.InputMethodManager;
            inputMethodManager.ShowSoftInput(handler.PlatformView, 0);
        }

        public static void MapHideKeyboardRequested(CustomEntryHandler4 handler, HandlerEntry4 entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            var inputMethodManager = MauiApplication.Current.GetSystemService(content.Context.InputMethodService) as view.InputMethods.InputMethodManager;
            inputMethodManager.HideSoftInputFromWindow(handler.PlatformView.WindowToken, HideSoftInputFlags.None);

            //var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            //inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus?.WindowToken, HideSoftInputFlags.None);
        }

        public static void MapClearFocusRequested(CustomEntryHandler4 handler, HandlerEntry4 entry, object? args)
        {
            handler.PlatformView.ClearFocus();

            var inputMethodManager = MauiApplication.Current.GetSystemService(content.Context.InputMethodService) as view.InputMethods.InputMethodManager;
            inputMethodManager.HideSoftInputFromWindow(handler.PlatformView.WindowToken, HideSoftInputFlags.None);
        }
    }
}