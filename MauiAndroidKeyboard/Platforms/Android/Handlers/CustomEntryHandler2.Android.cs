using Android.Text;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using AndroidX.AppCompat.Widget;
using MauiAndroidKeyboard.Controls;
using Microsoft.Maui.Handlers;
using System.Diagnostics;
using content = Android.Content;
using view = Android.Views;

namespace MauiAndroidKeyboard.Platforms.Android.Handlers
{
    public class CustomEntryHandler2 : EntryHandler
    {

        public static PropertyMapper<HandlerEntry2, CustomEntryHandler2> PropertyMapper = new PropertyMapper<HandlerEntry2, CustomEntryHandler2>(ViewHandler.ViewMapper)
        {
            //[nameof(HandlerEntry2.VirtualKeyboardToggle)] = MapVirtualKeyboardToggle
        };

        public static new CommandMapper<HandlerEntry2, CustomEntryHandler2> CommandMapper = new CommandMapper<HandlerEntry2, CustomEntryHandler2>(ViewHandler.ViewCommandMapper)
        {
            [nameof(HandlerEntry2.ShowKeyboardRequested)] = MapShowKeyboardRequested,
            [nameof(HandlerEntry2.HideKeyboardRequested)] = MapHideKeyboardRequested
        };

        protected override AppCompatEditText CreatePlatformView() => new AppCompatEditText(Context);

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

            if (VirtualView.IsReadOnly == true)
            {
                platformView.Enabled = false;
            }
            else
            {
                platformView.Enabled = true;
            }
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

        protected override void DisconnectHandler(AppCompatEditText platformView)
        {
            // Perform any native view cleanup here
            platformView.EditorAction -= PlatformView_EditorAction;

            platformView.Dispose();
            base.DisconnectHandler(platformView);
        }

        public static void MapShowKeyboardRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            inputMethodManager.ShowSoftInput(handler.PlatformView, ShowFlags.Forced);
        }

        public static void MapHideKeyboardRequested(CustomEntryHandler2 handler, HandlerEntry2 entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus?.WindowToken, HideSoftInputFlags.None);
        }


        //자동으로 생성되는 생성자이나 파라미터 없는 생성자 필요
        public CustomEntryHandler2(IPropertyMapper mapper, CommandMapper commandMapper = null) : base(mapper, commandMapper)
        {

        }

        public CustomEntryHandler2(IPropertyMapper mapper) : base(mapper)
        {

        }

        //이렇게 만들어야 함.
        public CustomEntryHandler2() : base(PropertyMapper, CommandMapper)
        {
            Debug.WriteLine("test");
        }
    }
}
