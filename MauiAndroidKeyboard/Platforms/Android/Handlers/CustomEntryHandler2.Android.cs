using Android.Content;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.View;
using MauiAndroidKeyboard.Controls;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using AView = Android.Views.View;

namespace MauiAndroidKeyboard.Platforms.Android.Handlers
{
    public class CustomEntryHandler2 : EntryHandler
    {
        public static PropertyMapper<HandlerEntry2, CustomEntryHandler2> PropertyMapper = new PropertyMapper<HandlerEntry2, CustomEntryHandler2>(ViewHandler.ViewMapper)
        {
            [nameof(HandlerEntry2.Background)] = MapBackground,
            [nameof(HandlerEntry2.IsReadOnly)] = MapIsReadOnly,
            [nameof(HandlerEntry2.Keyboard)] = MapKeyboard,
            [nameof(HandlerEntry2.ClearButtonVisibility)] = MapClearButtonVisibility,
            [nameof(HandlerEntry2.CursorPosition)] = MapCursorPosition,
            [nameof(HandlerEntry2.HorizontalTextAlignment)] = MapHorizontalTextAlignment,
            [nameof(HandlerEntry2.IsPassword)] = MapIsPassword,
            [nameof(HandlerEntry2.IsTextPredictionEnabled)] = MapIsTextPredictionEnabled,
            [nameof(HandlerEntry2.MaxLength)] = MapMaxLength,
            [nameof(HandlerEntry2.Placeholder)] = MapPlaceholder,
            [nameof(HandlerEntry2.ReturnType)] = MapReturnType,
            [nameof(HandlerEntry2.SelectionLength)] = MapSelectionLength,
            [nameof(HandlerEntry2.Text)] = MapText,
            [nameof(HandlerEntry2.VerticalTextAlignment)] = MapVerticalTextAlignment
        };

        //EntryHandler에 CommandMapper 만들어져 있어서 new
        public static new CommandMapper<HandlerEntry2, CustomEntryHandler2> CommandMapper = new CommandMapper<HandlerEntry2, CustomEntryHandler2>(ViewHandler.ViewCommandMapper)
        {
            [nameof(HandlerEntry2.ShowKeyboardRequested)] = MapShowKeyboardRequested,
            [nameof(HandlerEntry2.HideKeyboardRequested)] = MapHideKeyboardRequested,
            [nameof(HandlerEntry2.ClearFocusRequested)] = MapClearFocusRequested
        };

        //자동으로 생성되는 생성자이나 파라미터 없는 생성자 필요
        //iOS에서 발생하면 적용
        public CustomEntryHandler2(IPropertyMapper mapper, CommandMapper commandMapper = null) : base(mapper, commandMapper)
        {
        }

        //이거 없으면 PropertyMapper, CommandMapper 작동 안함.
        public CustomEntryHandler2() : base(PropertyMapper, CommandMapper)
        {
        }

        //#2
        protected override AppCompatEditText CreatePlatformView() => new AppCompatEditText(Context);

        //#3
        //public override void SetVirtualView(IView view)
        //{
        //    base.SetVirtualView(view);
        //    var entry = (HandlerEntry2)view;
        //}

        //#4
        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            base.ConnectHandler(PlatformView);

            platformView.SetTextIsSelectable(true);
            platformView.SetSelectAllOnFocus(true);
            platformView.ShowSoftInputOnFocus = false; //true: Show Keyboard, false: Hide Keyboard
            platformView.SetSingleLine(true);

            platformView.EditorAction += PlatformView_EditorAction;
        }

        private void PlatformView_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            var returnType = VirtualView?.ReturnType;

            var actionId = e.ActionId;
            var evt = e.Event;

            if (returnType != null)
            {
                var currentInputImeFlag = returnType.Value.ToPlatform();

                if (actionId == ImeAction.Done ||
                    actionId == currentInputImeFlag ||
                    (actionId == ImeAction.ImeNull && evt?.KeyCode == Keycode.Enter && evt?.Action == KeyEventActions.Up))
                {
                    VirtualView?.Completed();
                    //(VirtualView as Entry).SendCompleted();
                }
            }

            e.Handled = true;
        }

        protected override void DisconnectHandler(AppCompatEditText platformView)
        {
            base.DisconnectHandler(platformView);
            platformView.EditorAction -= PlatformView_EditorAction;
            platformView.Dispose();
        }

        public static void MapShowKeyboardRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            var inputMethodManager = (InputMethodManager?)handler.Context?.GetSystemService(Context.InputMethodService);
            inputMethodManager.ShowSoftInput(handler.PlatformView, 0);

            //var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            //inputMethodManager.ShowSoftInput(handler.PlatformView, 0);
        }

        public static void MapHideKeyboardRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            var focusedView = handler.Context?.GetActivity()?.Window?.CurrentFocus;
            AView tokenView = focusedView ?? handler.PlatformView;

            using var inputMethodManager = (InputMethodManager?)tokenView.Context?.GetSystemService(Context.InputMethodService);
            var windowToken = tokenView.WindowToken;

            if (windowToken is not null && inputMethodManager is not null)
            {
                inputMethodManager.HideSoftInputFromWindow(windowToken, HideSoftInputFlags.None);
            }

            //var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(Context.InputMethodService);
            //inputMethodManager.HideSoftInputFromWindow(handler.PlatformView.WindowToken, HideSoftInputFlags.None);
        }

        //키보드가 열려 있는 상태에서 이전화면으로 넘어 갈 때 SoftKeyboard를 Hidden시켜야 한다.
        //포커스된 상태에서 다른 Entry로 넘어 갈때 이전 Focus가 사라지지 않는 현상 해결.
        public static void MapClearFocusRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {
            handler.PlatformView.ClearFocus();

            var focusedView = handler.Context?.GetActivity()?.Window?.CurrentFocus;
            AView tokenView = focusedView ?? handler.PlatformView;

            using var inputMethodManager = (InputMethodManager?)tokenView.Context?.GetSystemService(Context.InputMethodService);
            var windowToken = tokenView.WindowToken;

            if (windowToken is not null && inputMethodManager is not null)
            {
                inputMethodManager.HideSoftInputFromWindow(windowToken, HideSoftInputFlags.None);
            }

            //var inputMethodManager = (InputMethodManager?)MauiApplication.Current.GetSystemService(Context.InputMethodService);
            //inputMethodManager.HideSoftInputFromWindow(handler.PlatformView.WindowToken, HideSoftInputFlags.None);
        }
    }
}
