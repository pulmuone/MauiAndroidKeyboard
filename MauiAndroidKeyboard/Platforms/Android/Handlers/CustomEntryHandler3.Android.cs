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


namespace MauiAndroidKeyboard.Platforms.Android.Handlers
{
    public class CustomEntryHandler3 : ViewHandler<CustomEntry, AppCompatEditText>
    {
        public static PropertyMapper<CustomEntry, CustomEntryHandler3> PropertyMapper = new PropertyMapper<CustomEntry, CustomEntryHandler3>(ViewHandler.ViewMapper)
        {
            [nameof(CustomEntry.Text)] = MapText,
            [nameof(CustomEntry.TextColor)] = MapTextColor
        };

        public static CommandMapper<CustomEntry, CustomEntryHandler3> CommandMapper = new CommandMapper<CustomEntry, CustomEntryHandler3>(ViewCommandMapper)
        {
            [nameof(CustomEntry.ShowKeyboardRequested)] = MapShowKeyboardRequested,
            [nameof(CustomEntry.HideKeyboardRequested)] = MapHideKeyboardRequested
        };

        protected override AppCompatEditText CreatePlatformView() => new AppCompatEditText(Context);

        protected override void ConnectHandler(AppCompatEditText platformView)
        {
            base.ConnectHandler(platformView);

            // Perform any control setup here
            //platformView.SetPadding(0, 0, 0, 0);
            platformView.SetTextIsSelectable(true);
            platformView.SetSelectAllOnFocus(true);
            //platformView.SetTextSize(ComplexUnitType.Sp, 14);
            platformView.ShowSoftInputOnFocus = false; //true: Show Keyboard, false: Hide Keyboard
            platformView.SetSingleLine(true);
            //platformView.InputType = inputTypes.ClassText;
            //platformView.SetOnKeyListener(new MyOnKeyListener(VirtualView));

            platformView.EditorAction += PlatformView_EditorAction;
        }

        private void PlatformView_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            var actionId = e.ActionId;
            var evt = e.Event;

            if (actionId == ImeAction.Done || (actionId == ImeAction.ImeNull && evt?.KeyCode == Keycode.Enter && evt?.Action == KeyEventActions.Up))
            {
                //VirtualView?.ReturnCommand.Execute(VirtualView?.ReturnCommandParameter);
                (VirtualView as Entry).SendCompleted();
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


        private static void MapText(CustomEntryHandler3 handler, CustomEntry entry)
        {
            handler.PlatformView.Text = entry.Text;
            handler.PlatformView?.SetSelection(handler.PlatformView?.Text?.Length ?? 0);
        }

        private static void MapTextColor(CustomEntryHandler3 handler, CustomEntry entry)
        {
            handler.PlatformView?.SetTextColor(entry.TextColor.ToPlatform());
        }



        public static void MapShowKeyboardRequested(CustomEntryHandler3 handler, CustomEntry entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            inputMethodManager.ShowSoftInput(handler.PlatformView, ShowFlags.Forced);
        }

        public static void MapHideKeyboardRequested(CustomEntryHandler3 handler, CustomEntry entry, object? args)
        {
            handler.PlatformView.RequestFocus();

            var inputMethodManager = (view.InputMethods.InputMethodManager)MauiApplication.Current.GetSystemService(content.Context.InputMethodService);
            var activity = Microsoft.Maui.ApplicationModel.Platform.CurrentActivity;
            inputMethodManager.HideSoftInputFromWindow(activity.CurrentFocus?.WindowToken, HideSoftInputFlags.None);
        }



        //생성자 어디로 진입하는지 체크해야함.
        public CustomEntryHandler3(IPropertyMapper mapper, CommandMapper commandMapper = null) : base(mapper, commandMapper)
        {

        }

        public CustomEntryHandler3(IPropertyMapper mapper) : base(mapper)
        {

        }

        //이렇게 만들어야 함.
        public CustomEntryHandler3() : base(PropertyMapper, CommandMapper)
        {

        }
    }
}
