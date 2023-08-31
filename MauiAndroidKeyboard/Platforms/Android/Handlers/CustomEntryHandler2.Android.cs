using Android.Graphics.Drawables;
using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using AndroidX.Core.Content;
using MauiAndroidKeyboard.Controls;
using Microsoft.Maui.Handlers;
using content = Android.Content;
using view = Android.Views;

namespace MauiAndroidKeyboard.Platforms.Android.Handlers
{
    public class CustomEntryHandler2 : EntryHandler
    {

        public static PropertyMapper<HandlerEntry2, CustomEntryHandler2> PropertyMapper = new PropertyMapper<HandlerEntry2, CustomEntryHandler2>(ViewHandler.ViewMapper)
        {
            [nameof(HandlerEntry2.Text)] = MapText
        };

        //EntryHandler에 CommandMapper 만들어져 있어서 new
        public static new CommandMapper<HandlerEntry2, CustomEntryHandler2> CommandMapper = new CommandMapper<HandlerEntry2, CustomEntryHandler2>(ViewHandler.ViewCommandMapper)
        {
            [nameof(HandlerEntry2.ShowKeyboardRequested)] = MapShowKeyboardRequested,
            [nameof(HandlerEntry2.HideKeyboardRequested)] = MapHideKeyboardRequested,
            [nameof(HandlerEntry2.ClearFocusRequested)] = MapClearFocusRequested
        };

        //자동으로 생성되는 생성자이나 파라미터 없는 생성자 필요
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

            //platformView.SetPadding(0, 0, 0, 0); //적용하면 모양 이상해짐.
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

            platformView.EditorAction += PlatformView_EditorAction;
        }

        private void PlatformView_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            var actionId = e.ActionId;
            var evt = e.Event;

            if (actionId == ImeAction.Done || (actionId == ImeAction.ImeNull && evt?.KeyCode == Keycode.Enter && evt?.Action == KeyEventActions.Up))
            {
                (VirtualView as Entry).SendCompleted();
            }

            e.Handled = true;
        }

        protected override void DisconnectHandler(AppCompatEditText platformView)
        {
            base.DisconnectHandler(platformView);

            // Perform any native view cleanup here
            platformView.EditorAction -= PlatformView_EditorAction;

            platformView.Dispose();
        }

        public static void MapShowKeyboardRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            //var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            //inputMethodManager.ShowSoftInput(handler.PlatformView, ShowFlags.Forced);
            inputMethodManager.ShowSoftInput(handler.PlatformView, 0);
        }

        public static void MapHideKeyboardRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            inputMethodManager.HideSoftInputFromWindow(handler.PlatformView.WindowToken, HideSoftInputFlags.None);

            //var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            //inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus?.WindowToken, HideSoftInputFlags.None);
        }

        //키보드가 열려 있는 상태에서 이전화면으로 넘어 갈 때 SoftKeyboard를 Hidden시켜야 한다.
        //포커스된 상태에서 다른 Entry로 넘어 갈때 이전 Focus가 사라지지 않는 현상 해결.
        public static void MapClearFocusRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {
            handler.PlatformView.ClearFocus();

            var inputMethodManager = MauiApplication.Current.GetSystemService(content.Context.InputMethodService) as view.InputMethods.InputMethodManager;
            inputMethodManager.HideSoftInputFromWindow(handler.PlatformView.WindowToken, HideSoftInputFlags.None);
        }

        protected override Drawable GetClearButtonDrawable()
        {
            return ContextCompat.GetDrawable(Context, Resource.Drawable.abc_ic_clear_material);
        }
    }
}
